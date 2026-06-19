using EloGreen.API.Controllers;
using EloGreen.Application.Services.Interfaces;
using EloGreen.Application.ViewModels.Request;
using EloGreen.Application.ViewModels.Response;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace EloGreen.Tests.Controllers;

public class SuppliersControllerTests
{
    private readonly Mock<ISupplierService> _mockSupplierService;
    private readonly Mock<IValidator<CreateSupplierRequest>> _mockValidator;
    private readonly SuppliersController _controller;

    public SuppliersControllerTests()
    {
        _mockSupplierService = new Mock<ISupplierService>();
        _mockValidator = new Mock<IValidator<CreateSupplierRequest>>();

        _controller = new SuppliersController(_mockSupplierService.Object, _mockValidator.Object);
    }

    [Fact]
    public async Task Create_ShouldReturn201Created_WhenRequestIsValid()
    {
        // Arrange
        var request = new CreateSupplierRequest
        {
            Name = "Fornecedor Verde",
            Document = "11222333000199",
            IsEsgCertified = true
        };

        var response = new SupplierResponse
        {
            Id = Guid.NewGuid(),
            Name = request.Name,
            Document = request.Document,
            IsEsgCertified = request.IsEsgCertified,
            CreatedAt = DateTime.UtcNow
        };

        _mockValidator
            .Setup(v => v.ValidateAsync(request, It.IsAny<CancellationToken>()))
            .ReturnsAsync(new ValidationResult());

        _mockSupplierService
            .Setup(s => s.CreateAsync(request))
            .ReturnsAsync(response);

        // Act
        var result = await _controller.Create(request);

        // Assert
        var createdResult = Assert.IsType<CreatedAtActionResult>(result);
        var model = Assert.IsType<SupplierResponse>(createdResult.Value);
        Assert.Equal(request.Name, model.Name);
        Assert.Equal(response.Id, model.Id);
    }
}