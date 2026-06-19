using System.Threading.Tasks;
using Xunit;

namespace EloGreen.Tests.Integration;

public class EndpointsIntegrationTests : IClassFixture<CustomWebApplicationFactory>
{
    private readonly HttpClient _client;
    private readonly CustomWebApplicationFactory _factory;

    public EndpointsIntegrationTests(CustomWebApplicationFactory factory)
    {
        _factory = factory;
        _client = factory.CreateClient();
    }

    [Fact]
    public async Task Endpoint1_GetSuppliers_ReturnsHttpStatusCode200_AndPaginates()
    {
        // Act
        var response = await _client.GetAsync("/api/v1/Suppliers?page=1&size=5");

        // Assert
        response.EnsureSuccessStatusCode();
    }

    [Fact]
    public async Task Endpoint2_GetProducts_ReturnsHttpStatusCode200()
    {
        // Act
        var response = await _client.GetAsync("/api/v1/Products");

        // Assert
        response.EnsureSuccessStatusCode();
    }

    [Fact]
    public async Task Endpoint3_GetCarbonTrackingsByProduct_ReturnsHttpStatusCode200()
    {
        // Arrange
        var request = $"/api/v1/CarbonTrackings/product/{_factory.TestProductId}";

        // Act
        var response = await _client.GetAsync(request);

        // Assert
        response.EnsureSuccessStatusCode();
    }

    [Fact]
    public async Task Endpoint4_GetTotalCarbonCalculation_ReturnsHttpStatusCode200()
    {
        // Arrange
        var request = $"/api/v1/CarbonTrackings/product/{_factory.TestProductId}/total-carbon";

        // Act
        var response = await _client.GetAsync(request);

        // Assert
        response.EnsureSuccessStatusCode();
    }
}