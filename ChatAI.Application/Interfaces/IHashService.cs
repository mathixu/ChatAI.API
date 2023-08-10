namespace ChatAI.Application.Interfaces;

public interface IHashService
{
    string Hash(string password);
    bool Verify(string password, string hash);
}
