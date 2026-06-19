using EloGreen.Application.Validators;
using EloGreen.Application.ViewModels.Request;
using Xunit;

namespace EloGreen.Tests.Validators;

public class CreateProductValidatorTests
{
    private readonly CreateProductValidator _validator;

    public CreateProductValidatorTests()
    {
        _validator = new CreateProductValidator();
    }

    [Fact]
    public void Validate_ShouldPass_WhenProductDataIsValid()
    {
        // Arrange
        var request = new CreateProductRequest
        {
            Name = "Placa Solar Biogás",
            Description = "Eficiência energética de alta performance",
            BaseCarbonFootprint = 45.50m,
            SupplierId = Guid.NewGuid()
        };

        // Act
        var result = _validator.Validate(request);

        // Assert
        Assert.True(result.IsValid);
    }

    [Fact]
    public void Validate_ShouldFail_WhenCarbonFootprintIsNegative()
    {
        // Arrange
        var request = new CreateProductRequest
        {
            Name = "Placa Solar Biogás",
            BaseCarbonFootprint = -1.25m,
            SupplierId = Guid.NewGuid()
        };

        // Act
        var result = _validator.Validate(request);

        // Assert
        Assert.False(result.IsValid);
        Assert.Contains(result.Errors, e => e.PropertyName == nameof(request.BaseCarbonFootprint));
    }

    [Fact]
    public void Validate_ShouldFail_WhenSupplierIdIsEmpty()
    {
        // Arrange
        var request = new CreateProductRequest
        {
            Name = "Placa Solar Biogás",
            BaseCarbonFootprint = 10m,
            SupplierId = Guid.Empty
        };

        // Act
        var result = _validator.Validate(request);

        // Assert
        Assert.False(result.IsValid);
        Assert.Contains(result.Errors, e => e.ErrorMessage.Contains("vinculado a um fornecedor válido"));
    }
}