using ChatAI.Application.Interfaces;
using ChatAI.Infrastructure.Options;
using Microsoft.Extensions.Options;
using System.Security.Cryptography;
using System.Text;

namespace ChatAI.Infrastructure.Services;

public class EncryptionService : IEncryptionService
{
    private readonly EncryptionOptions _encryptionOptions;

    public EncryptionService(IOptions<EncryptionOptions> encryptionOptions)
    {
        _encryptionOptions = encryptionOptions.Value;

        var keySizeInBits = Encoding.UTF8.GetBytes(_encryptionOptions.Key).Length * 8; 
        if (keySizeInBits != 128 && keySizeInBits != 192 && keySizeInBits != 256)
        {
            throw new ArgumentException($"La taille de la clé est de {keySizeInBits} bits, mais AES requiert une clé de 128, 192 ou 256 bits.");
        }
    }

    public string Encrypt(string plainText)
    {
        using var aesAlg = Aes.Create();
        aesAlg.Key = Encoding.UTF8.GetBytes(_encryptionOptions.Key);
        aesAlg.GenerateIV();

        var encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);

        using var msEncrypt = new MemoryStream();
        using var csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write);
        using var swEncrypt = new StreamWriter(csEncrypt);
        swEncrypt.Write(plainText);
        swEncrypt.Flush();
        csEncrypt.FlushFinalBlock();

        var result = new byte[aesAlg.IV.Length + msEncrypt.Length];
        Buffer.BlockCopy(aesAlg.IV, 0, result, 0, aesAlg.IV.Length);
        Buffer.BlockCopy(msEncrypt.ToArray(), 0, result, aesAlg.IV.Length, (int)msEncrypt.Length);

        return Convert.ToBase64String(result);
    }

    public string Decrypt(string cipherText)
    {
        var fullCipher = Convert.FromBase64String(cipherText);

        var iv = new byte[16];
        var actualCipherText = new byte[fullCipher.Length - 16];
        Buffer.BlockCopy(fullCipher, 0, iv, 0, iv.Length);
        Buffer.BlockCopy(fullCipher, iv.Length, actualCipherText, 0, fullCipher.Length - iv.Length);

        using var aesAlg = Aes.Create();
        aesAlg.Key = Encoding.UTF8.GetBytes(_encryptionOptions.Key);
        aesAlg.IV = iv;

        var decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);

        using var msDecrypt = new MemoryStream(actualCipherText);
        using var csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read);
        using var srDecrypt = new StreamReader(csDecrypt);
        return srDecrypt.ReadToEnd();
    }

}
