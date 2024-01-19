using System.Net.Http.Headers;
using ExpenseManagement.Api;
using ExpenseManagement.Api.Middlewares;
using ExpenseManagement.Base.JsonFiles;
using ExpenseManagement.Business;
using ExpenseManagement.Data;
using ExpenseManagement.Data.DbContexts;
using ExpenseManagement.Data.DbSeed;
using Serilog;


var builder = WebApplication.CreateBuilder(args);
{
    ///Summary:
    /// All dependency injections come from their own project folder.
    builder.Services
        .AddPresentation()
        .AddBusiness(builder.Configuration)
        .AddDataInjection(builder.Configuration);
}

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}



using var scope = app.Services.CreateScope();
var services = scope.ServiceProvider;
var context = services.GetRequiredService<EfExpenseManagementDbContext>();
context.Database.EnsureCreated();

DbSeedOperations.SeedDatabase(context, builder.Configuration);

app.UseMiddleware<HeartBeatMiddleware>();
app.UseMiddleware<ErrorHandlerMiddleware>();

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
