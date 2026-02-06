using Microsoft.OpenApi;

namespace API.Configurations;

public static class OpenAPIConfig
{
    private static readonly string appName = "API.Web";
    private static readonly string appDescription = "RESTful API devoloped with .NET 10 for study purposes.";

    public static IServiceCollection AddOpenAPIConfig(this IServiceCollection services)
    {
        services.AddSingleton(new OpenApiInfo
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

        return services;
    }
}