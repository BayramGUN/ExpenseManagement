using System.Reflection;
using System.Text;
using AutoMapper;
using ExpenseManagement.Base.Token;
using ExpenseManagement.Business.Authentication;
using ExpenseManagement.Business.Common;
using ExpenseManagement.Business.Configurations.Mapper;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace ExpenseManagement.Business;

/// <summary>
/// Extension methods for configuring dependency injection in the business layer.
/// </summary>
public static class DependencyInjection
{
    /// <summary>
    /// Adds MediatR, AutoMapper, FluentValidation, and authentication-related services to the IServiceCollection.
    /// </summary>
    /// <param name="services">The IServiceCollection to which services are added.</param>
    /// <param name="configuration">The configuration manager for accessing configuration settings.</param>
    /// <returns>The updated IServiceCollection.</returns>
    public static IServiceCollection AddBusiness(
        this IServiceCollection services, 
        ConfigurationManager configuration)
    {
        // Add MediatR for handling commands and queries
        services.AddMediatR(typeof(DependencyInjection).Assembly);

        // Configure AutoMapper with the specified profile 
        var mapperConfig = new MapperConfiguration(cfg => cfg.AddProfile(new MapperConfig()));
        services.AddSingleton(mapperConfig.CreateMapper());

        /* // Add validators from the current assembly
        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly()); */

        // Add authentication-related services
        services.AddAuthentication(configuration);

        return services;
    }

    /// <summary>
    /// Adds authentication-related services to the IServiceCollection.
    /// </summary>
    /// <param name="services">The IServiceCollection to which services are added.</param>
    /// <param name="configuration">The configuration manager for accessing configuration settings.</param>
    /// <returns>The updated IServiceCollection.</returns>
    public static IServiceCollection AddAuthentication(
        this IServiceCollection services, 
        ConfigurationManager configuration)
    {
        var jwtSettings = new JwtSettings();
        configuration.Bind(JwtSettings.SectionName, jwtSettings);

        services.AddSingleton(Options.Create(jwtSettings));
        services.AddSingleton<IJwtTokenGenerator, JwtTokenGenerator>();

        services.AddAuthentication(defaultScheme: JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options => options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = jwtSettings.Issuer,
                    ValidAudience = jwtSettings.Audience,
                    IssuerSigningKey = new SymmetricSecurityKey(
                        Encoding.UTF8.GetBytes(jwtSettings.Secret)
                    )
                });
        
        return services;
    }
}