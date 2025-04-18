using Domain.Contracts;
using Ecommerce_Store.API.Middlewares;
using Microsoft.AspNetCore.Mvc;
using Persistence;
using Services;
using Shared.ErrorModels;



namespace Ecommerce_Store.API.Extension
{
    public static class Extension
    {
        public static IServiceCollection RegisterAllServices(this IServiceCollection services, IConfiguration configuration)
        {
            {
                services.AddBuiltinFunctions();
                services.AddSwaggerServices();
                services.AddInfrastructureServices(configuration);
                services.AddApplicationServices();

                return services;
            }

        }



        private static IServiceCollection AddBuiltinFunctions(this IServiceCollection services)
        {
            services.AddControllers();
            return services;
        }


        private static IServiceCollection AddSwaggerServices(this IServiceCollection services)
        {
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();
            return services;
        }



 


        public static async Task<WebApplication> ConfigureMiddleWareAsync(this WebApplication app)
        {
            await app.IntailizeDataBaseAsync();

            app.UseGlobalErrorHandling(); // Register the global error handling middleware
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
            return app;
        }



        private static async Task<WebApplication> IntailizeDataBaseAsync(this WebApplication app)
        {
            #region Seeding
            // Create a scope and run database initializer
            using var scope = app.Services.CreateScope();
            var dbInitializer = scope.ServiceProvider.GetRequiredService<IDbInitializer>();
            await dbInitializer.IntializeAsync(); // Apply migrations and seed data
            #endregion
            return app;
        }

        private static WebApplication UseGlobalErrorHandling(this WebApplication app)
        {
            app.UseMiddleware<GlobalErrorHandlingMiddleware>(); // Register the global error handling middleware

            return app;
        }


    }
}
