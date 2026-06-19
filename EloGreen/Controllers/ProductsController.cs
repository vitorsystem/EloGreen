using EloGreen.Application.Services;
using EloGreen.Application.Services.Interfaces;
using EloGreen.Application.ViewModels.Request;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EloGreen.API.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class ProductsController : ControllerBase
{
    private readonly IProductService _productService;
    private readonly IValidator<CreateProductRequest> _validator;

    public ProductsController(IProductService productService, IValidator<CreateProductRequest> validator)
    {
        _productService = productService;
        _validator = validator;
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateProductRequest request)
    {
        var validationResult = await _validator.ValidateAsync(request);
        if (!validationResult.IsValid) return BadRequest(validationResult.Errors);

        var response = await _productService.CreateProductAsync(request);

        if (response == null)
        {
            return NotFound(new { Message = "O Fornecedor informado não existe na base de dados." });
        }

        return CreatedAtAction(nameof(GetById), new { id = response.Id }, response);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var response = await _productService.GetProductByIdAsync(id);
        if (response == null) return NotFound();

        return Ok(response);
    }

    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] int page = 1, [FromQuery] int size = 10, [FromQuery] string? name = null)
    {
        var result = await _productService.GetAllAsync(page, size, name);
        return Ok(result);
    }

    [Authorize]
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var (notFound, errorMessage) = await _productService.DeleteAsync(id);

        if (notFound) return NotFound();
        if (errorMessage != null) return BadRequest(new { Message = errorMessage });

        return NoContent();
    }

    [Authorize]
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(Guid id, [FromBody] UpdateProductRequest request, [FromServices] IValidator<UpdateProductRequest> updateValidator)
    {
        var validationResult = await updateValidator.ValidateAsync(request);
        if (!validationResult.IsValid) return BadRequest(validationResult.Errors);

        var updated = await _productService.UpdateAsync(id, request);
        if (!updated) return NotFound();

        return NoContent();
    }

}