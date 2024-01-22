
using ExpenseManagement.Api.Caching;
using ExpenseManagement.Base.Constants.Cors;
using ExpenseManagement.Base.JsonFiles;
using ExpenseManagement.Base.LoggingInformation;
using ExpenseManagement.Base.Swagger;
using ExpenseManagement.Business.Authentication.Commands.SignUp;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.OpenApi.Models;
using Serilog;

namespace ExpenseManagement.Api;

    /// <summary>
    /// Static class for configuring dependency injection in the presentation layer.
    /// </summary>
public static class DependencyInjection
{
        /// <summary>
        /// Configures services related to the presentation layer, including controllers, validation, Swagger, logging, and CORS.
        /// </summary>
        /// <param name="services">The IServiceCollection to which services are added.</param>
        /// <returns>The updated IServiceCollection.</returns>
    public static IServiceCollection AddPresentation(
        this IServiceCollection services)
    {
        services.AddScoped<ICacheService, CacheService>();
        // Add controllers with FluentValidation support
        services.AddControllers().AddFluentValidation(x => {
            x.RegisterValidatorsFromAssemblyContaining<SignUpValidator>();
        });

        // Add API Explorer for endpoint discovery
        services.AddEndpointsApiExplorer();

        // Add Swagger documentation generation
        services.AddSwaggerGen(c =>
        {
            // Configure Swagger document with version information
            c.SwaggerDoc(SwaggerConfigurationKeys.VersionId, 
                new OpenApiInfo { 
                    Title = SwaggerConfigurationKeys.Title, 
                    Version = SwaggerConfigurationKeys.Version 
                    }
                );

            // Configure Swagger security scheme for JWT authentication
            var securityScheme = new OpenApiSecurityScheme
            {
                Name = SwaggerSecuritySchemeKeys.Name,
                Description = SwaggerSecuritySchemeKeys.Description,
                In = ParameterLocation.Header,
                Type = SecuritySchemeType.Http,
                Scheme = SwaggerSecuritySchemeKeys.Scheme,
                BearerFormat = SwaggerSecuritySchemeKeys.Format,
                Reference = new OpenApiReference
                {
                    Id = JwtBearerDefaults.AuthenticationScheme,
                    Type = ReferenceType.SecurityScheme
                }
            };

            c.AddSecurityDefinition(securityScheme.Reference.Id, securityScheme);
            c.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                { securityScheme, new string[] { } }
            });
        });

        // Read and configure Serilog logging from AppSettings.json
        var config = new ConfigurationBuilder().AddJsonFile(JsonFilesKeys.AppSettingsJson).Build();
        Log.Logger = new LoggerConfiguration().ReadFrom.Configuration(config).CreateLogger();
        Log.Information(LogInformation.StartMessage);

        // Configure CORS policy
        services.AddCors(options =>
        {
        options.AddPolicy(AllowedOrigins.SpecificOrigin,
                              policy =>
                              {
                                  policy.AllowAnyOrigin()
                                        .AllowAnyHeader()
                                        .AllowAnyMethod();
                              });
        });
        return services;
    }
}