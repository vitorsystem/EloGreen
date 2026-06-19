using EloGreen.API.Controllers;
using EloGreen.Application.Services.Interfaces;
using EloGreen.Application.ViewModels.Request;
using EloGreen.Application.ViewModels.Response;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace EloGreen.Tests.UnitTests.Controllers;

public class CarbonTrackingsControllerTests
{
    private readonly Mock<ICarbonTrackingService> _mockService;
    private readonly Mock<IValidator<CreateCarbonTrackingRequest>> _mockValidator;
    private readonly CarbonTrackingsController _controller;

    public CarbonTrackingsControllerTests()
    {
        _mockService = new Mock<ICarbonTrackingService>();
        _mockValidator = new Mock<IValidator<CreateCarbonTrackingRequest>>();
        _controller = new CarbonTrackingsController(_mockService.Object, _mockValidator.Object);
    }

    [Fact]
    public async Task Create_ShouldReturn201_WhenValid()
    {
        var request = new CreateCarbonTrackingRequest
        {
            ActivityDescription = "Queima de combustível",
            CarbonEmitted = 10.5m,
            TrackingDate = DateTime.UtcNow,
            ProductId = Guid.NewGuid()
        };

        var response = new CarbonTrackingResponse { Id = Guid.NewGuid() };

        _mockValidator.Setup(v => v.ValidateAsync(request, It.IsAny<CancellationToken>()))
                      .ReturnsAsync(new ValidationResult());

        _mockService.Setup(s => s.CreateAsync(request))
                    .ReturnsAsync(response);

        var result = await _controller.Create(request);

        var objectResult = Assert.IsType<ObjectResult>(result);
        Assert.Equal(201, objectResult.StatusCode);
    }

    [Fact]
    public async Task GetTotalCarbon_ShouldReturnOkWithCalculation_WhenProductExists()
    {
        var productId = Guid.NewGuid();
        var expectedTotal = 150.5m;

        _mockService.Setup(s => s.GetTotalCarbonFootprintAsync(productId))
                    .ReturnsAsync(expectedTotal);

        var result = await _controller.GetTotalCarbon(productId);

        var okResult = Assert.IsType<OkObjectResult>(result);
        Assert.NotNull(okResult.Value);
    }
}