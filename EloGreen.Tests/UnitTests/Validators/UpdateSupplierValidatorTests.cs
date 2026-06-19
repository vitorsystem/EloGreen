using EloGreen.Application.Validators;
using EloGreen.Application.ViewModels.Request;
using Xunit;

namespace EloGreen.Tests.UnitTests.Validators;

public class UpdateSupplierValidatorTests
{
    private readonly UpdateSupplierValidator _validator;

    public UpdateSupplierValidatorTests()
    {
        _validator = new UpdateSupplierValidator();
    }

    [Fact]
    public void Validate_ShouldPass_WhenUpdateDataIsValid()
    {
        // Arrange
        var request = new UpdateSupplierRequest
        {
            Name = "EcoTech Sustentável S/A",
            Document = "ABCDEFGHIJ1234",
            IsEsgCertified = false
        };

        // Act
        var result = _validator.Validate(request);

        // Assert
        Assert.True(result.IsValid);
    }

    [Fact]
    public void Validate_ShouldFail_WhenNameIsEmpty()
    {
        // Arrange
        var request = new UpdateSupplierRequest
        {
            Name = "",
            Document = "ABCDEFGHIJ1234",
            IsEsgCertified = true
        };

        // Act
        var result = _validator.Validate(request);

        // Assert
        Assert.False(result.IsValid);
        Assert.Contains(result.Errors, e => e.PropertyName == nameof(request.Name));
    }

    [Fact]
    public void Validate_ShouldFail_WhenCnpjIsInvalid()
    {
        // Arrange
        var request = new UpdateSupplierRequest
        {
            Name = "EcoTech Sustentável S/A",
            Document = "123",
            IsEsgCertified = true
        };

        // Act
        var result = _validator.Validate(request);

        // Assert
        Assert.False(result.IsValid);
        Assert.Contains(result.Errors, e => e.PropertyName == nameof(request.Document));
    }
}