using System.Net;
using System.Net.Http.Json;
using System.Text.Json;
using API.Data.DTOs.V1;
using API.Tests.Integration.Tools;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using Xunit.Abstractions;
using static API.Tests.Integration.Tools.PriorityOrdener;

namespace API.Tests.Integration;

[TestCaseOrderer("API.Tests.Integration.Tools.PriorityOrdener", "API.Tests")]
public class CorsIntegrationsTests : IClassFixture<PostgreSQLFixture>
{
    private readonly HttpClient _httpClient;
    private readonly ITestOutputHelper _output;

    public CorsIntegrationsTests(PostgreSQLFixture fixture, ITestOutputHelper output)
    {
        var factory = new CustomWebApplicationFactory<Program>(fixture.ConnectionString);
        _httpClient = factory.CreateClient(
            new WebApplicationFactoryClientOptions
            {
                BaseAddress = new Uri("http://localhost")
            }
        );

        _output = output;
    }

    PersonDTO dtoTest = new() { Id = 11, FirstName = "John", LastName = "Doe", Gender = "M", Address = "123 Main St" };

    private void AddOriginHeader(string origin)
    {
        _httpClient.DefaultRequestHeaders.Remove("Origin");
        _httpClient.DefaultRequestHeaders.Add("Origin", origin);
    }

    [Fact(DisplayName = "01 - Create person with allowed origin")]
    [TestPriority(1)]
    public async Task CreatePerson_WithAllowedOrigin_ShouldReturnSuccess()
    {
        // Arrange --> Preparação de request
        AddOriginHeader("http://youtube.com.br");
        
        // Act --> Faz execução do request
        var response = await _httpClient.PostAsJsonAsync("/api/person/v1", dtoTest);
        PersonDTO responseContent = await response.Content.ReadFromJsonAsync<PersonDTO>()!;

        // Assert
        response.EnsureSuccessStatusCode();
        responseContent.Should().NotBeNull();
        responseContent.Should().BeEquivalentTo(dtoTest, options => options.Excluding(p => p.Id));

    }

    [Fact(DisplayName = "02 - Create person with disallowed origin")]
    [TestPriority(2)]
    public async Task CreatePerson_WithDisallowedOrigin_ShouldReturnForbidden()
    {
        // Arrange --> Preparação de request
        AddOriginHeader("http://disallowed-origin.com");
        
        // Act --> Faz execução do request
        var response = await _httpClient.PostAsJsonAsync("/api/person/v1", dtoTest);
        var responseContent = await response.Content.ReadAsStringAsync();

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.Forbidden);
        responseContent.Should().NotBeNull();
        responseContent.Should().Be("CORS origin not allowed.");

    }

    [Fact(DisplayName = "03 - FindById person with disallowed origin")]
    [TestPriority(3)]
    public async Task FindById_WithDisallowedOrigin_ShouldReturnForbidden()
    {
        // Arrange --> Preparação de request
        AddOriginHeader("http://disallowed-origin.com");
        
        // Act --> Faz execução do request
        var response = await _httpClient.GetAsync("/api/person/v1/1");
        var responseContent = await response.Content.ReadAsStringAsync();

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.Forbidden);
        responseContent.Should().NotBeNull();
        responseContent.Should().Be("CORS origin not allowed.");

    }
    [Fact(DisplayName = "05 - FindById person with allowed origin")]
    [TestPriority(5)]
    public async Task FindById_WithAllowedOrigin_ShouldReturnSuccess()
    {
        // Arrange --> Preparação de request
        AddOriginHeader("http://youtube.com.br");
        
        // Act --> Faz execução do request
        var response = await _httpClient.GetAsync("/api/person/v1/1");
        var responseContent = await response.Content.ReadAsStringAsync();

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);
        responseContent.Should().NotBeNull();
    }
}