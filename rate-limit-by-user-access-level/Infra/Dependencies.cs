using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using rate_limit_by_user_access_level.Abstractions;
using rate_limit_by_user_access_level.Implementations;
using System.Text;

namespace rate_limit_by_user_access_level.Infra;

public static class Dependencies
{
    public static IServiceCollection AddDependencies(
    this IServiceCollection services,
    IConfiguration configuration)
    {
        AddImplementations(services);
        AddCaching(services, configuration);
        AddJwtBearer(services, configuration);
        AddSwagger(services);

        return services;
    }

    private static void AddImplementations(IServiceCollection services)
    {
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<ITokenGenerator, TokenGenerator>();
        services.AddScoped<IRateLimitService, RateLimitService>();
        services.AddSingleton<ICountRequests, CountRequests>();
    }

    private static void AddCaching(IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("Cache") ??
            throw new ArgumentNullException(nameof(configuration));

        services.AddStackExchangeRedisCache(options => options.Configuration = connectionString);

        services.AddSingleton<ICacheService, CacheService>();
    }

    private static void AddJwtBearer(IServiceCollection services, IConfiguration configuration)
    {
        services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        }).AddJwtBearer(options =>
        {
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = false,
                ValidateAudience = false,
                ValidateIssuerSigningKey = true,
                ValidIssuer = configuration["JwtSettings:Issuer"],
                ValidAudience = configuration["JwtSettings:Audience"],
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(configuration["JwtSettings:SecretKey"]))
            };
        });
    }

    private static void AddSwagger(IServiceCollection services)
    {
        services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo
            {
                Title = "rate_limit_by_user_access_level",
                Version = "v1",
                Description = "",
                Contact = new OpenApiContact
                {
                    Name = "gabriel",
                    Email = "ga.jaimealves@gmail.com",
                    Url = new Uri("https://github.com/gajalves")
                },
            });

            c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                In = ParameterLocation.Header,
                Description = "Bearer <TOKEN>",
                Name = "Authorization",
                Type = SecuritySchemeType.ApiKey
            });
            c.AddSecurityRequirement(new OpenApiSecurityRequirement {
            {
                new OpenApiSecurityScheme
                {
                    Reference = new OpenApiReference
                    {
                        Type = ReferenceType.SecurityScheme,
                        Id = "Bearer"
                    }
                },
                new string[] { }
            }
            });
        });
    }
}
