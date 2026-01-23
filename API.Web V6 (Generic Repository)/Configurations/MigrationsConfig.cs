using EvolveDb;
using Microsoft.EntityFrameworkCore;
using Npgsql;
using Serilog;

namespace API.Configurations;

public static class MigrationsConfig
{
    public static IServiceCollection AddMigrationsConfig(
        this IServiceCollection services,
        IConfiguration configuration,
        IWebHostEnvironment environment)
    {
        if (!environment.IsDevelopment())
        {
            return services;
        }

        var connectionString = configuration.GetConnectionString("DefaultConnection");

        if (string.IsNullOrEmpty(connectionString))
        {
            throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
        }

        try
        {
            using var evolveConnection =  new NpgsqlConnection(connectionString);
            var evolve = new Evolve(evolveConnection, msg => Log.Information(msg))
            {
                Locations = new[] { "db/migrations", "db/dataset" },
                IsEraseDisabled = true
            };

            evolve.Migrate();
        }
        catch (Exception ex)
        {
            Log.Fatal("Database migration failed: ", ex);
            throw;
        }

        return services;
    }
}