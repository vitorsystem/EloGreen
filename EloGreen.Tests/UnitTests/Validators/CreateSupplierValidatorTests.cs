using EloGreen.Application.Validators;
using EloGreen.Application.ViewModels.Request;

namespace EloGreen.Tests.UnitTests.Validators;
public class CreateSupplierValidatorTests
{
    private readonly CreateSupplierValidator _validator;

    public CreateSupplierValidatorTests()
    {
        _validator = new CreateSupplierValidator();
    }

    [Fact]
    public void Validate_ShouldPass_WhenCnpjIsAlphanumericAndVAlid()
    {
        // Arrange
        var request = new CreateSupplierRequest
        {
            Name = "EcoTehc LTDA",
            Document = "ABCDEFGHIJ1234",
            IsEsgCertified = true
        };

        // Act
        var result = _validator.Validate(request);

        // Assert 
        Assert.True(result.IsValid);
    }

    [Fact]
    public void Validate_ShouldFail_WhenCnpjEndsWithLetters()
    {
        // Arrange
        var request = new CreateSupplierRequest
        {
            Name = "EcoTech LTDA",
            Document = "123456789012AB",
            IsEsgCertified = true
        };

        // Act
        var result = _validator.Validate(request);

        // Assert
        Assert.False(result.IsValid);
        Assert.Contains(result.Errors, e => e.ErrorMessage.Contains("2 dígitos verificadores numéricos"));
    }
}
