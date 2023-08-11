using AutoMapper;
using ChatAI.Application.Account.Commands.AddOpenAIToken;
using ChatAI.Application.Account.Commands.DeleteMyAccount;
using ChatAI.Application.Authentication.Commands.Login;
using ChatAI.Application.Authentication.Commands.Refresh;
using ChatAI.Application.Authentication.Commands.RequestPasswordReset;
using ChatAI.Application.Authentication.Commands.ResetPassword;
using ChatAI.Application.Authentication.Commands.SignUp;
using ChatAI.Application.Interfaces;
using ChatAI.Application.Mappings;
using ChatAI.Application.Services;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace ChatAI.Application;

public static class ConfigureServices
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.ConfigureDependancies();
        services.ConfigureMapper();

        services.AddMediatR(cfg =>
        {
            cfg.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly());
        });

        return services;
    }

    private static void ConfigureDependancies(this IServiceCollection services)
    {
        services.AddScoped<IAuthenticationService, AuthenticationService>();
        services.AddTransient<LoginCommandValidator>();
        services.AddTransient<SignUpCommandValidator>();
        services.AddTransient<AddOpenAITokenCommandValidator>();
        services.AddTransient<RefreshCommandValidator>();
        services.AddTransient<RequestPasswordResetCommandValidator>();
        services.AddTransient<ResetPasswordCommandValidator>();
        services.AddTransient<DeleteMyAccountCommandValidator>();
    }

    private static void ConfigureMapper(this IServiceCollection services)
    {
        var mapperConfig = new MapperConfiguration(mc =>
        {
            mc.AddProfile(new GlobalProfile());
            mc.AddProfile(new CommandsProfile());
            mc.AddProfile(new ResponsesProfile());
        });

        var mapper = mapperConfig.CreateMapper();
        services.AddSingleton(mapper);
    }
}
