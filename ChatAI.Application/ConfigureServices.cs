using AutoMapper;
using ChatAI.Application.Interfaces;
using ChatAI.Application.Mappings;
using ChatAI.Application.Services;
using ChatAI.Application.Validators;
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
