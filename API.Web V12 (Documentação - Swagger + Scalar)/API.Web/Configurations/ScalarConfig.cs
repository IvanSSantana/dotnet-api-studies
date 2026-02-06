using Scalar.AspNetCore;

namespace API.Configurations;

public static class AddScalarConfig
{
    public static WebApplication UseScalarConfig(this WebApplication app)
    {
        app.MapScalarApiReference("/scalar", options =>
        {
            options.WithTitle("Scalar API");
            options.WithOpenApiRoutePattern("swagger/v1/swagger.json");
        });

        return app;
    }
}