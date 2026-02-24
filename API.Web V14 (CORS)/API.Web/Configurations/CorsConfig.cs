namespace API.Configurations;

public static class AddCorsConfig
{
    private static string[] GetAllowedOrigins(IConfiguration configuration)
    {
        return configuration.GetSection("Cors:Origins").Get<string[]>() ?? Array.Empty<string>();
    }

    public static void AddCorsConfiguration(this WebApplicationBuilder builder, IConfiguration configuration)
    {
        string[] origins = GetAllowedOrigins(configuration);

        builder.Services.AddCors(options =>
        {
            options.AddPolicy("LocalPolicy", policy =>
            {
                policy
                        .WithOrigins("http://localhost:3000")
                        .AllowAnyMethod()
                        .AllowAnyHeader()
                        .AllowCredentials();
            });

            options.AddPolicy("MultipleOriginsPolicy", policy =>
            {
                policy
                        .WithOrigins(origins)
                        .AllowAnyMethod()
                        .AllowAnyHeader()
                        .AllowCredentials();
            });

            options.AddPolicy("DefaultPolicy", policy =>
            {
                policy
                        .WithOrigins(origins)
                        .AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader();
            });
        });
    }

    public static WebApplication UseCorsConfiguration(this WebApplication app, IConfiguration configuration)
    {
        string[] origins = GetAllowedOrigins(configuration);

        app.Use(async (context, next) =>
        {
            string? origin = context.Request.Headers["Origin"];

            if (!string.IsNullOrEmpty(origin) && !origins.Contains(origin, StringComparer.OrdinalIgnoreCase))
            {
                context.Response.StatusCode = StatusCodes.Status403Forbidden;
                await context.Response.WriteAsync("CORS origin not allowed.");
                return;
            }

            await next();
        });

        app.UseCors("DefaultPolicy");
        return app;
    }
}
