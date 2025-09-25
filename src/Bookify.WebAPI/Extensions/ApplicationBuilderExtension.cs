using Bookify.Infrastructure;
using Bookify.WebAPI.Middleware;

using Microsoft.EntityFrameworkCore;

namespace Bookify.WebAPI.Extensions;

public static class ApplicationBuilderExtension
{
    public static void ApplyMigrations(this IApplicationBuilder app) 
    {
        using var scope = app.ApplicationServices.CreateScope();
        using var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
        
        dbContext.Database.Migrate();
    }
    
    public static void UseCustomExceptionHandler(this IApplicationBuilder app)
    {
        app.UseMiddleware<ExceptionHandlerMiddleware>();
    }
}