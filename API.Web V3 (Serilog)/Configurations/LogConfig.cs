using Serilog;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Hosting;

namespace API.Configurations;

public static class LoggingConfig
{
    public static void AddSerilogLogging(this WebApplicationBuilder builder)
    {
        builder.Logging.ClearProviders();

        Log.Logger = new LoggerConfiguration()
            .ReadFrom.Configuration(builder.Configuration)
            .Enrich.FromLogContext() 
            .CreateLogger();
        
        builder.Logging.AddSerilog(Log.Logger, dispose: true);    
        builder.Host.UseSerilog();
    }
}
