using ChatAI.API.OptionsSetup;
using ChatAI.Infrastructure;
using ChatAI.Application;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;
using ChatAI.Infrastructure.Options;
using ChatAI.Domain.Enums;
using Microsoft.AspNetCore.Authorization;

namespace ChatAI.API.Extensions;

public static class BuilderExtensions
{
    public static void Configure(this WebApplicationBuilder builder)
    {
        builder.Services.ConfigureOptions();
        builder.Services.ConfigureAPI();
        builder.Services.ConfigureSwagger();
        builder.Services.ConfigureCors();
        builder.Services.ConfigureAuthorization();

        builder.Services.AddHttpContextAccessor();
        builder.Services.AddApplicationServices();
        builder.Services.AddInfrastructureServices(builder.Configuration);
        builder.Services.ConfigureBearer();
    }

    private static void ConfigureAPI(this IServiceCollection services)
    {
        services.AddControllers();
    }

    private static void ConfigureBearer(this IServiceCollection services)
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
    }

    private static void ConfigureSwagger(this IServiceCollection services)
    {
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();

        services.AddSwaggerGen(setup =>
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
        });
    }

    private static void ConfigureOptions(this IServiceCollection services)
    {
        services.ConfigureOptions<JwtOptionsSetup>();
        services.ConfigureOptions<RefreshTokenOptionsSetup>();
        services.ConfigureOptions<SmtpOptionsSetup>();
        services.ConfigureOptions<EncryptionOptionsSetup>();
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

    private static void ConfigureAuthorization(this IServiceCollection services)
    {
        services.AddAuthorization(options =>
        {
            options.AddPolicy("ResetPassword", pol =>
                pol.RequireClaim("purpose", JwtType.PasswordResetToken.ToString()));

            options.DefaultPolicy = new AuthorizationPolicyBuilder()
                .RequireAuthenticatedUser()
                .RequireClaim("purpose", JwtType.AccessToken.ToString())
                .Build();
        });
    }
}
