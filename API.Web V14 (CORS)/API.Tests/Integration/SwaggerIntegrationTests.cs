using API.Tests.Integration.Tools;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;

namespace API.Tests.Integration;

public class SwaggerIntegrationTest : IClassFixture<PostgreSQLFixture>
{
    private readonly HttpClient _httpClient;
    public SwaggerIntegrationTest(PostgreSQLFixture fixture)
    {
        var factory = new CustomWebApplicationFactory<Program>(fixture.ConnectionString);
        _httpClient = factory.CreateClient(
            new WebApplicationFactoryClientOptions
            {
                BaseAddress = new Uri("http://localhost")
            }
        );
    }

    [Fact]
    public async Task SwaggerJson_ShouldReturnSwaggerJson()
    {
        // Arrange & Act
        var response = await _httpClient.GetAsync("/swagger/v1/swagger.json");

        // Assert
        response.EnsureSuccessStatusCode();

        var content = await response.Content.ReadAsStringAsync();
        content.Should().NotBeNullOrEmpty();
        content.Should().Contain("/api/person/v1");
    }

    [Fact]
    public async Task SwaggerUI_ShouldReturnSwaggerUI()
    {
        // Arrange & Act
        var response = await _httpClient.GetAsync("/swagger-ui/index.html");

        // Assert
        response.EnsureSuccessStatusCode();

        var content = await response.Content.ReadAsStringAsync();
        content.Should().NotBeNullOrEmpty();
        content.Should().Contain("<div id=\"swagger-ui\">");
    }
}