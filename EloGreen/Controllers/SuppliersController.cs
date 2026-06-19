using Microsoft.AspNetCore.Mvc;
using FluentValidation;
using EloGreen.Application.ViewModels.Request;
using EloGreen.Application.Services.Interfaces;

namespace EloGreen.API.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class SuppliersController(ISupplierService supplierService, IValidator<CreateSupplierRequest> createValidator) : ControllerBase
{
    private readonly ISupplierService _supplierService = supplierService;
    private readonly IValidator<CreateSupplierRequest> _createValidator = createValidator;

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateSupplierRequest request)
    {
        var validationResult = await _createValidator.ValidateAsync(request);
        if (!validationResult.IsValid) return BadRequest(validationResult.Errors);

        var response = await _supplierService.CreateAsync(request);

        return CreatedAtAction(nameof(GetById), new { id = response.Id }, response);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var response = await _supplierService.GetByIdAsync(id);
        if (response == null) return NotFound();

        return Ok(response);
    }

    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] int page = 1, [FromQuery] int size = 10, [FromQuery] string? name = null)
    {
        var result = await _supplierService.GetAllAsync(page, size, name);
        return Ok(result);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(Guid id, [FromBody] UpdateSupplierRequest request, [FromServices] IValidator<UpdateSupplierRequest> updateValidator)
    {
        var validationResult = await updateValidator.ValidateAsync(request);
        if (!validationResult.IsValid) return BadRequest(validationResult.Errors);

        var updated = await _supplierService.UpdateAsync(id, request);
        if (!updated) return NotFound();

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var (notFound, errorMessage) = await _supplierService.DeleteAsync(id);

        if (notFound) return NotFound();
        if (errorMessage != null) return BadRequest(new { Message = errorMessage });

        return NoContent();
    }
}