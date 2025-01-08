using CleanArchitecture.Infrastructure;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.EntityFrameworkCore;

namespace CleanArchitecture.Api.Extensions;

public static class ApplicationBuilder
{
  public static async void ApplyMigration(this IApplicationBuilder app)
  {
    using (var scope = app.ApplicationServices.CreateScope())
    {
      var service = scope.ServiceProvider;
      var loggerFactory = service.GetRequiredService<ILoggerFactory>();

      try
      {
        var context = service.GetRequiredService<ApplicationDbContext>();
        await context.Database.MigrateAsync();
        var logger = loggerFactory.CreateLogger<Program>();
        logger.LogInformation("Test ApplicationBuilder");
      }
      catch (Exception ex)
      {
        var logger = loggerFactory.CreateLogger<Program>();
        logger.LogError(ex, "Error en migracion");
      }
    }
  }

  public static void UseCustomExceptionHandler(this IApplicationBuilder app)
  {
    app.UseMiddleware<ExceptionHandlerMiddleware>();
  }
}