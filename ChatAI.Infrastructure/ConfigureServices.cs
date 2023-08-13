using ChatAI.Application.Common.Interfaces;
using ChatAI.Infrastructure.Authentication;
using ChatAI.Infrastructure.Persistence;
using ChatAI.Infrastructure.Persistence.Interceptors;
using ChatAI.Infrastructure.Persistence.Repositories;
using ChatAI.Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ChatAI.Infrastructure;

public static class ConfigureServices
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped(typeof(IBaseRepository<>), typeof(BaseRepository<>));
        services.AddScoped<IChatSessionRepository, ChatSessionRepository>();

        services.AddScoped<AuditableEntitySaveChangesInterceptor>();

        var connectionString = configuration.GetConnectionString("DefaultConnection");

        services.AddDbContext<ChatAIDbContext>(options =>
            options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString),
                builder => builder.MigrationsAssembly(typeof(ChatAIDbContext).Assembly.FullName)));

        services.AddScoped<ChatAIDbContextInitializer>();

        services.AddTransient<IDateTime, DateTimeService>(); 
        services.AddTransient<ICurrentUserService, CurrentUserService>();
        services.AddTransient<IEmailService, EmailService>();
        services.AddTransient<IEncryptionService, EncryptionService>();

        services.AddScoped<IHashService, HashService>();
        services.AddScoped<IJwtProvider, JwtProvider>();
        services.AddScoped<IRefreshTokenProvider, RefreshTokenProvider>();
        
        return services;
    }
}
