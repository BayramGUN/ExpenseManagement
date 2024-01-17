
using ExpenseManagement.Base.Constants.Cors;
using ExpenseManagement.Base.JsonFiles;
using ExpenseManagement.Base.LoggingInformation;
using ExpenseManagement.Business.Authentication.Commands.SignUp;
using FluentValidation.AspNetCore;
using Serilog;

namespace ExpenseManagement.Api;
public static class DependencyInjection
{

    public static IServiceCollection AddPresentation(
        this IServiceCollection services)
    {
        services.AddControllers().AddFluentValidation(x => {
            x.RegisterValidatorsFromAssemblyContaining<SignUpValidator>();
        });
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();


        var config = new ConfigurationBuilder().AddJsonFile(JsonFilesKeys.AppSettingsJson).Build();
        Log.Logger = new LoggerConfiguration().ReadFrom.Configuration(config).CreateLogger();
        Log.Information(LogInformation.StartMessage);


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