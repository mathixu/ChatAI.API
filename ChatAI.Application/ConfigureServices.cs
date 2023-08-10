using AutoMapper;
using ChatAI.Application.Mappings;
using ChatAI.Application.Validators;
using Microsoft.Extensions.DependencyInjection;

namespace ChatAI.Application;

public static class ConfigureServices
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.ConfigureDependancies();
        services.ConfigureMapper();

        return services;
    }

    private static void ConfigureDependancies(this IServiceCollection services)
    {
        services.AddTransient<LoginCommandValidator>();
        services.AddTransient<SignUpCommandValidator>();
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
