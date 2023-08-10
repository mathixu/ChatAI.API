using AutoMapper;
using ChatAI.Infrastructure;
using System.Reflection;

namespace ChatAI.API.Extensions;

public static class BuilderExtensions
{
    public static void Configure(this WebApplicationBuilder builder)
    {
        builder.Services.ConfigureOptions();
        builder.Services.ConfigureDependancies();
        builder.Services.ConfigureAPI();
        builder.Services.ConfigureSwagger();
        builder.Services.ConfigureCors();
        builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));

        //builder.Services.AddApplicationServices();
        builder.Services.AddInfrastructureServices(builder.Configuration);
        //builder.Services.ConfigureBearer();

    }

    private static void ConfigureAPI(this IServiceCollection services)
    {
        services.AddControllers();
    }

    /*private static void ConfigureBearer(this IServiceCollection services)
    {
        var jwtOptions = services.BuildServiceProvider().GetRequiredService<IOptions<JwtOptions>>().Value ?? throw new ArgumentNullException("JwtOptions");

        services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        }).AddJwtBearer(options =>
        {
            options.RequireHttpsMetadata = false;
            options.SaveToken = true;
            options.TokenValidationParameters = new()
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = jwtOptions.Issuer,
                ValidAudience = jwtOptions.Audience,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtOptions.SecretKey)),
                RequireExpirationTime = true,
                ClockSkew = TimeSpan.Zero,
            };
        });
    }*/

    private static void ConfigureSwagger(this IServiceCollection services)
    {
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();

        /*services.AddSwaggerGen(setup =>
        {
            var jwtSecurityScheme = new OpenApiSecurityScheme
            {
                BearerFormat = "JWT",
                Name = "JWT Authentication",
                In = ParameterLocation.Header,
                Type = SecuritySchemeType.Http,
                Scheme = JwtBearerDefaults.AuthenticationScheme,
                Description = "Put **_ONLY_** your JWT Bearer token on textbox below!",

                Reference = new OpenApiReference
                {
                    Id = JwtBearerDefaults.AuthenticationScheme,
                    Type = ReferenceType.SecurityScheme,
                },
            };

            setup.AddSecurityDefinition(jwtSecurityScheme.Reference.Id, jwtSecurityScheme);

            setup.AddSecurityRequirement(new OpenApiSecurityRequirement { { jwtSecurityScheme, Array.Empty<string>() } });
        });*/
    }

    private static void ConfigureDependancies(this IServiceCollection services)
    {
    }

    private static void ConfigureOptions(this IServiceCollection services)
    {
        /*services.ConfigureOptions<JwtOptionsSetup>();
        services.ConfigureOptions<RefreshTokenOptionsSetup>();*/
    }

    private static void ConfigureCors(this IServiceCollection services)
    {
        services.AddCors(options =>
        {
            options.AddPolicy(
                "AllowAll",
                builder =>
                {
                    builder.AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader();
                });
        });
    }
}
