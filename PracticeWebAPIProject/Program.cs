using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using PracticeWebAPIProject.Data;
using PracticeWebAPIProject.Middlewares;
using PracticeWebAPIProject.Services;

var builder = WebApplication.CreateBuilder(args);

builder.WebHost.UseUrls("http://localhost:5000", "https://localhost:7000");

builder.Services.AddOpenApi();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IUserService, UserService>();

builder.Services.AddDbContext<UserDbContext>(options => 
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseExceptionHandler("/error");

using var scope = app.Services.CreateScope();
await using var dbContext = scope.ServiceProvider.GetRequiredService<UserDbContext>();
await dbContext.Database.GetInfrastructure().GetService<IMigrator>()!.MigrateAsync();

// app.UseAuthorization();
app.MapControllers();

app.UseMiddleware<RequestLoggingMiddleware>();

app.Use(async (context, next) =>
{
    try
    {
        await next(); // run rest of pipeline
    }
    catch (Exception)
    {
        context.Response.StatusCode = 500;
        await context.Response.WriteAsync("Something went wrong");
    }
});
// app.Use(async (context, next) =>
// {
//     Console.WriteLine("-----------------------------------------------------------------------------------------------------Use: before");
//     await next(); // go to next middleware
//     Console.WriteLine("Use: after-----------------------------------------------------------------------------------------------------");
// });
//
//
// app.MapGet("/api/users", () => "Hello");

app.Run();
