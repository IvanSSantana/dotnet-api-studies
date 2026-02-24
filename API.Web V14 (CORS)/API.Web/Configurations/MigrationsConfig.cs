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
            ExecuteMigrations(connectionString);
        }
        catch (Exception ex)
        {
            Log.Fatal("Database migration failed: ", ex);
            throw;
        }

        return services;
    }

    public static void ExecuteMigrations(string connectionString)
    {
        using var evolveConnection = new NpgsqlConnection(connectionString);

        var evolve = new Evolve(
            evolveConnection,
            msg => Log.Information(msg))
        {
            Locations = ["db/migrations", "db/dataset"],
                IsEraseDisabled = false
        };
        
        evolve.Erase();
        evolve.Migrate();
    }
}