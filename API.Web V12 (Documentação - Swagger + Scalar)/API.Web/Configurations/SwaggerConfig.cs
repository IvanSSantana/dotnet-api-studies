using Microsoft.OpenApi;

namespace API.Configurations;

public static class SwaggerConfig
{
    private static readonly string appName = "API.Web";
    private static readonly string appDescription = "RESTful API devoloped with .NET 10 for study purposes.";

     public static IServiceCollection AddSwaggerConfig(this IServiceCollection services)
    {
        services.AddSwaggerGen(options =>
        {
            options.SwaggerDoc("v1", new OpenApiInfo
            {
                Title = appName,
                Version = "v1",
                Description = appDescription,
                Contact = new OpenApiContact
                {
                    Name = "Ivan Santos",
                    Email = "ivansantospriv@gmail.com"
                },
                License = new OpenApiLicense
                {
                    Name = "MIT License",
                    Url = new Uri("https://github.com/IvanSSantana/dotnet-api-studies/blob/main/LICENSE")
                }
            });

            options.CustomSchemaIds(x => x.FullName);
        });

        return services;
    }

    public static IApplicationBuilder UseSwaggerConfig(this IApplicationBuilder app)
    {
        app.UseSwagger();
        app.UseSwaggerUI(options =>
        {
            options.SwaggerEndpoint("/swagger/v1/swagger.json", "API.Web v1");
            options.RoutePrefix = "swagger-ui";
            options.DocumentTitle = appName;
        });

        return app;
    }
}
