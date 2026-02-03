using System.Data;
using API.Context;
using Microsoft.EntityFrameworkCore;
using Npgsql;

namespace API.Configurations;

public static class DatabaseConfig
{
    public static IServiceCollection AddDatabaseConfiguration(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("DefaultConnection");

        if (string.IsNullOrEmpty(connectionString))
        {
            throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
        }

        services.AddDbContext<AppDbContext>(options => 
            options.UseNpgsql(connectionString));

        services.AddScoped<IDbConnection>(serviceProvider =>
            new NpgsqlConnection(connectionString));

        return services;
    }
}