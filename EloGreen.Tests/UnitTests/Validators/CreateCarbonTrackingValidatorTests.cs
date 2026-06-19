using EloGreen.Application.Validators;
using EloGreen.Application.ViewModels.Request;
using Xunit;

namespace EloGreen.Tests.UnitTests.Validators;

public class CreateCarbonTrackingValidatorTests
{
    private readonly CreateCarbonTrackingValidator _validator;

    public CreateCarbonTrackingValidatorTests()
    {
        _validator = new CreateCarbonTrackingValidator();
    }

    [Fact]
    public void Validate_ShouldFail_WhenCarbonEmittedIsNegative()
    {
        var request = new CreateCarbonTrackingRequest
        {
            ActivityDescription = "Transporte de carga",
            CarbonEmitted = -5m,
            TrackingDate = DateTime.UtcNow,
            ProductId = Guid.NewGuid()
        };

        var result = _validator.Validate(request);

        Assert.False(result.IsValid);
        Assert.Contains(result.Errors, e => e.PropertyName == nameof(request.CarbonEmitted));
    }

    [Fact]
    public void Validate_ShouldFail_WhenDateIsInTheFuture()
    {
        var request = new CreateCarbonTrackingRequest
        {
            ActivityDescription = "Transporte de carga",
            CarbonEmitted = 50m,
            TrackingDate = DateTime.UtcNow.AddDays(1),
            ProductId = Guid.NewGuid()
        };

        var result = _validator.Validate(request);

        Assert.False(result.IsValid);
        Assert.Contains(result.Errors, e => e.PropertyName == nameof(request.TrackingDate));
    }
}