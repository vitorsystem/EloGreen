using EloGreen.Application.Validators;
using EloGreen.Application.ViewModels.Request;
using Xunit;

namespace EloGreen.Tests.UnitTests.Validators;

public class UpdateProductValidatorTests
{
    private readonly UpdateProductValidator _validator;

    public UpdateProductValidatorTests()
    {
        _validator = new UpdateProductValidator();
    }

    [Fact]
    public void Validate_ShouldPass_WhenUpdateDataIsValid()
    {
        //Arrange
        var request = new UpdateProductRequest
        {
            Name = "Produto Atualizado",
            Description = "Nova descrição do produto",
            BaseCarbonFootprint = 10m,
            SupplierId = Guid.NewGuid()
        };

        //Act
        var result = _validator.Validate(request);

        //Assert
        Assert.True(result.IsValid);
    }

    [Fact]
    public void Validate_ShouldFail_WhenNameIsEmpty()
    {
        //Arrange
        var request = new UpdateProductRequest
        {
            Name = "",
            BaseCarbonFootprint = 10m,
            SupplierId = Guid.NewGuid()
        };

        //Act
        var result = _validator.Validate(request);

        //Assert
        Assert.False(result.IsValid);
        Assert.Contains(result.Errors, e => e.PropertyName == nameof(request.Name));
    }

    [Fact]
    public void Validate_ShouldFail_WhenCarbonFootprintIsNegative()
    {
        //Arrange
        var request = new UpdateProductRequest
        {
            Name = "Produto Atualizado",
            BaseCarbonFootprint = -5m,
            SupplierId = Guid.NewGuid()
        };

        //Act
        var result = _validator.Validate(request);

        //Assert
        Assert.False(result.IsValid);
        Assert.Contains(result.Errors, e => e.PropertyName == nameof(request.BaseCarbonFootprint));
    }
}