using API.Configurations;
using API.Repositories.Contracts;
using API.Repositories.Implementations;
using API.Services.Contracts.V1;
using API.Services.Contracts.V2;
using API.Services.Implementations.V1;
using API.Services.Implementations.V2;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.AddSerilogLogging();

builder.Services
    .AddControllers()
    .AddContentNegotiation();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddOpenAPIConfig();
builder.Services.AddSwaggerConfig();
builder.Services.AddRouteConfig();

builder.Services.AddDatabaseConfiguration(builder.Configuration);
builder.Services.AddMigrationsConfig(builder.Configuration, builder.Environment);

builder.Services.AddScoped<IPersonService, PersonService>();
builder.Services.AddScoped<IPersonServiceV2, PersonServiceV2>();
builder.Services.AddScoped<IBooksService, BooksService>();

builder.Services.AddScoped<IEntityAttributesProvider, EntityAttributesProvider>();

builder.Services.AddScoped(typeof(IRepository<>), typeof(GenericRepository<>));

// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

var lifetime = app.Services.GetRequiredService<IHostApplicationLifetime>();

lifetime.ApplicationStarted.Register(() =>
{
    Log.Information("API is now running at {HostAdress}/api/person/v1", app.Urls.First());
    Log.Information("API is now running at {HostAdress}/api/person/v2", app.Urls.First());
    Log.Information("API is now running at {HostAdress}/api/books/v1", app.Urls.First());
    Log.Information("Swagger UI is now running at {HostAdress}/swagger-ui/index.html", app.Urls.First());
    Log.Information("Scalar UI is now running at {HostAdress}/scalar", app.Urls.First());
});

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.UseSwaggerConfig();
app.UseScalarConfig();

app.Run();