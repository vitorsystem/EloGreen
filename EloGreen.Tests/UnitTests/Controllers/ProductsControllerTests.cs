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

public class ProductsControllerTests
{
    private readonly Mock<IProductService> _mockProductService;
    private readonly Mock<IValidator<CreateProductRequest>> _mockValidator;
    private readonly ProductsController _controller;

    public ProductsControllerTests()
    {
        _mockProductService = new Mock<IProductService>();
        _mockValidator = new Mock<IValidator<CreateProductRequest>>();
        _controller = new ProductsController(_mockProductService.Object, _mockValidator.Object);
    }

    [Fact]
    public async Task Create_ShouldReturn201Created_WhenProductIsSuccessfullyCreated()
    {
        // Arrange
        var request = new CreateProductRequest
        {
            Name = "Turbina Eólica Residencial",
            BaseCarbonFootprint = 150.00m,
            SupplierId = Guid.NewGuid()
        };

        var response = new ProductResponse
        {
            Id = Guid.NewGuid(),
            Name = request.Name,
            Description = request.Description,
            BaseCarbonFootprint = request.BaseCarbonFootprint,
            SupplierId = request.SupplierId,
            CreatedAt = DateTime.UtcNow
        };

        _mockValidator
            .Setup(v => v.ValidateAsync(request, It.IsAny<CancellationToken>()))
            .ReturnsAsync(new ValidationResult());

        _mockProductService
            .Setup(s => s.CreateProductAsync(request))
            .ReturnsAsync(response);

        // Act
        var result = await _controller.Create(request);

        // Assert
        var createdResult = Assert.IsType<CreatedAtActionResult>(result);
        var model = Assert.IsType<ProductResponse>(createdResult.Value);
        Assert.Equal(response.Id, model.Id);
        Assert.Equal(request.Name, model.Name);
    }

    [Fact]
    public async Task Create_ShouldReturn404NotFound_WhenServiceReturnsNullBecauseSupplierDoesNotExist()
    {
        // Arrange
        var request = new CreateProductRequest
        {
            Name = "Turbina Eólica Residencial",
            SupplierId = Guid.NewGuid()
        };

        _mockValidator
            .Setup(v => v.ValidateAsync(request, It.IsAny<CancellationToken>()))
            .ReturnsAsync(new ValidationResult());

           _mockProductService
            .Setup(s => s.CreateProductAsync(request))
            .ReturnsAsync((ProductResponse?)null);

        // Act
        var result = await _controller.Create(request);

        // Assert
        var notFoundResult = Assert.IsType<NotFoundObjectResult>(result);
        Assert.NotNull(notFoundResult.Value);
    }
}