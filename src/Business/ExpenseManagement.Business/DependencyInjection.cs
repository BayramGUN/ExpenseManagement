using System.Text;
using AutoMapper;
using ExpenseManagement.Base.Constants.Queue;
using ExpenseManagement.Base.MessageBroker;
using ExpenseManagement.Base.Token;
using ExpenseManagement.Business.Common.Implementation.EventBus;
using ExpenseManagement.Business.Common.Implementation.Token;
using ExpenseManagement.Business.Common.Interfaces.EventBus;
using ExpenseManagement.Business.Common.Interfaces.Token;
using ExpenseManagement.Business.Configurations.Mapper;
using ExpenseManagement.Business.PaymentCqrs.Events;
using MassTransit;
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
        
        // Add messageBroker-related services
        services.AddAuthentication(configuration)
                .AddMessageBroker(configuration);

        // Add EventBus dependency as transient.
        services.AddTransient<IEventBus, EventBus>();

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

        services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options => {
                    options.RequireHttpsMetadata = true;
                    options.SaveToken = true;

                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = jwtSettings.Issuer,
                        ValidAudience = jwtSettings.Audience,
                        IssuerSigningKey = new SymmetricSecurityKey(
                            Encoding.UTF8.GetBytes(jwtSettings.Secret)
                        ),
                        ClockSkew = TimeSpan.FromMinutes(2)
                    };
                });
    
        return services;
    }

    /// <summary>
    /// Adds authentication-related services to the IServiceCollection.
    /// </summary>
    /// <param name="services">The IServiceCollection to which services are added.</param>
    /// <param name="configuration">The configuration manager for accessing configuration settings.</param>
    /// <returns>The updated IServiceCollection.</returns>
    public static IServiceCollection AddMessageBroker(
        this IServiceCollection services, 
        ConfigurationManager configuration)
    {
        services.Configure<MessageBrokerSettings>(
            configuration.GetSection(MessageBrokerSettings.SectionName));

        services.AddSingleton(serviceProvider => 
            serviceProvider.GetRequiredService<IOptions<MessageBrokerSettings>>().Value);
        
        services.AddMassTransit(busConfigurator => 
        {
            busConfigurator.SetKebabCaseEndpointNameFormatter();
            busConfigurator.UsingRabbitMq((context, configurator) => 
            {
                MessageBrokerSettings settings = context.GetRequiredService<MessageBrokerSettings>();

                configurator.Host(settings.Host, settings.VirtualHost, h => 
                {
                    h.Username(settings.UserName);
                    h.Password(settings.Password);
                });
                configurator.ReceiveEndpoint(QueueNames.PaymentQueue, endpointConfigurator => {
                    endpointConfigurator.UseMessageRetry(retryConfigurator => {
                        retryConfigurator.Intervals(100, 500, 1000);
                    });
                    endpointConfigurator.Consumer<PaymentEventConsumer>();
                });
            });
        });
        return services;
    }
}