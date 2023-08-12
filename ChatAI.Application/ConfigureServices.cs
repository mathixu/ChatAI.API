using AutoMapper;
using ChatAI.Application.Accounts.Commands.AddOpenAIToken;
using ChatAI.Application.Accounts.Commands.DeleteMyAccount;
using ChatAI.Application.Authentication.Commands.Login;
using ChatAI.Application.Authentication.Commands.Refresh;
using ChatAI.Application.Authentication.Commands.RequestPasswordReset;
using ChatAI.Application.Authentication.Commands.ResetPassword;
using ChatAI.Application.Authentication.Commands.SignUp;
using ChatAI.Application.Common.Interfaces;
using ChatAI.Application.Common.Services;
using ChatAI.Application.Common.Mappings;
using ChatAI.Application.SystemPrompts.Commands.AddSystemPrompt;
using ChatAI.Application.SystemPrompts.Commands.EditSystemPrompt;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using ChatAI.Application.Chats.Commands.AddMessage;
using ChatAI.Application.Chats.Commands.AddChatSession;
using ChatAI.Application.Chats.Commands.EditChatSessionTitle;

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
        services.AddTransient<AddSystemPromptCommandValidator>();
        services.AddTransient<EditSystemPromptCommandValidator>();
        services.AddTransient<AddMessageCommandValidator>();
        services.AddTransient<AddChatSessionCommandValidator>();
        services.AddTransient<EditChatSessionTitleCommandValidator>();
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
