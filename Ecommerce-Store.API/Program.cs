using System.Reflection.Metadata;
using Domain.Contracts;
using Ecommerce_Store.API.Middlewares;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Persistence;
using Services;
using Services.Abstractions;
using Shared.ErrorModels;
using AssemblyMapping = Services.AssemblyReference;
public class Program
{
    public static async Task Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container
        builder.Services.AddControllers();
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        // Configure DbContext with SQL Server connection string
        builder.Services.AddDbContext<StoreDbContext>(options =>
        {
            options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
        });

        // Register DbInitializer for dependency injection
        builder.Services.AddScoped<IDbInitializer, DbInitializer>();
        builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
        builder.Services.AddAutoMapper(typeof(AssemblyMapping).Assembly);
        builder.Services.AddScoped<IServiceManager, ServiceManager>();

        builder.Services.Configure<ApiBehaviorOptions>(config =>
        {
            config.InvalidModelStateResponseFactory = actionContext =>
            {
                var errors = actionContext.ModelState
                    .Where(m => m.Value.Errors.Any())
                    .Select(m => new ValidationError
                    {
                        Field = m.Key,
                        Errors = m.Value.Errors.Select(e => e.ErrorMessage).ToList()
                    });

                var response = new ValidationErrorReponse()
                {
                    Errors = errors
                };

                return new BadRequestObjectResult(response);
            };
        });


        var app = builder.Build();
        app.UseMiddleware<GlobalErrorHandlingMiddleware>(); // Register the global error handling middleware
        #region Seeding
        // Create a scope and run database initializer
        using var scope = app.Services.CreateScope();
        var dbInitializer = scope.ServiceProvider.GetRequiredService<IDbInitializer>();
        await dbInitializer.IntializeAsync(); // Apply migrations and seed data
        #endregion

        // Configure the HTTP request pipeline
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }
        app.UseStaticFiles();
        app.UseHttpsRedirection();
        app.UseAuthorization();
        app.MapControllers();

        await app.RunAsync(); // Use RunAsync for async Main
    }
}
