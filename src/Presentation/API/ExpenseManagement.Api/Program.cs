using ExpenseManagement.Data;
using ExpenseManagement.Data.DbContexts;
using ExpenseManagement.Data.DbSeed;

var builder = WebApplication.CreateBuilder(args);
{
    ///Summary:
    /// all dependency injections come from their own project.
    builder.Services.AddDataInjection(builder.Configuration);
    builder.Services.AddControllers();
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();
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
var context = services.GetRequiredService<ExpenseManagementDbContext>();
context.Database.EnsureCreated();

//DbSeedOperations.SeedDatabase(context, builder.Configuration);

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
