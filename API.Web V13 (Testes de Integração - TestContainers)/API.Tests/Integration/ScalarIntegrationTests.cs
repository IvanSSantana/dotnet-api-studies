using API.Tests.Integration.Tools;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;

namespace API.Tests.Integration;

public class ScalarIntegrationTests : IClassFixture<PostgreSQLFixture>
{
    private readonly HttpClient _httpClient;
    public ScalarIntegrationTests(PostgreSQLFixture fixture)
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
    public async Task ScalarUI_ShouldReturnScalarUI()
    {
        // Arrange & Act
        var response = await _httpClient.GetAsync("/scalar");

        // Assert
        response.EnsureSuccessStatusCode();

        var content = await response.Content.ReadAsStringAsync();
        content.Should().NotBeNullOrEmpty();
        content.Should().Contain("<title>API.Web</title>");
        content.Should().Contain("<script src=\"scalar.js\"></script>");
    }
}