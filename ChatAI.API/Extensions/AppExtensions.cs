using ChatAI.Infrastructure.Persistence;
using Serilog;

namespace ChatAI.API.Extensions;

public static class AppExtensions
{
    public static async Task Configure(this WebApplication app)
    {
        app.UseSerilogRequestLogging();

        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseCors("AllowAll");

        app.UseHttpsRedirection();

        app.UseRouting();

        app.UseAuthentication();
        app.UseAuthorization();

        app.MapControllers();

        app.SetAddress();

        await app.MigrateDatabase();

    }

    private static void SetAddress(this WebApplication app)
    {
        if(app.Environment.IsProduction())
        {
            var address = app.Configuration["address"] ?? throw new ArgumentNullException("address not found in appsettings.json");
            app.Urls.Add(address);
        }
    }
    
    private static async Task MigrateDatabase(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();
        var initialiser = scope.ServiceProvider.GetRequiredService<ChatAIDbContextInitializer>();
        await initialiser.InitializeAsync();
    }
}
