using EloGreen.Application.Services.Interfaces;
using EloGreen.Application.ViewModels.Request;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EloGreen.API.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class CarbonTrackingsController : ControllerBase
{
    private readonly ICarbonTrackingService _trackingService;
    private readonly IValidator<CreateCarbonTrackingRequest> _validator;

    public CarbonTrackingsController(ICarbonTrackingService trackingService, IValidator<CreateCarbonTrackingRequest> validator)
    {
        _trackingService = trackingService;
        _validator = validator;
    }

    [Authorize]
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateCarbonTrackingRequest request)
    {
        var validationResult = await _validator.ValidateAsync(request);
        if (!validationResult.IsValid) return BadRequest(validationResult.Errors);

        var response = await _trackingService.CreateAsync(request);

        if (response == null)
            return NotFound(new { Message = "O Produto informado não existe na base de dados." });

        return StatusCode(StatusCodes.Status201Created, response);
    }

    [HttpGet("product/{productId}")]
    public async Task<IActionResult> GetByProductId(Guid productId)
    {
        var trackings = await _trackingService.GetByProductIdAsync(productId);
        return Ok(trackings);
    }

    [HttpGet("product/{productId}/total-carbon")]
    public async Task<IActionResult> GetTotalCarbon(Guid productId)
    {
        var totalCarbon = await _trackingService.GetTotalCarbonFootprintAsync(productId);

        if (totalCarbon == null)
            return NotFound(new { Message = "Produto não encontrado." });

        return Ok(new
        {
            ProductId = productId,
            TotalCarbonFootprint = totalCarbon
        });
    }

    [Authorize]
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(Guid id, [FromBody] UpdateCarbonTrackingRequest request, [FromServices] IValidator<UpdateCarbonTrackingRequest> updateValidator)
    {
        var validationResult = await updateValidator.ValidateAsync(request);
        if (!validationResult.IsValid) return BadRequest(validationResult.Errors);

        var (notFound, errorMessage) = await _trackingService.UpdateAsync(id, request);

        if (notFound) return NotFound();
        if (errorMessage != null) return BadRequest(new { Message = errorMessage });

        return NoContent();
    }

    [Authorize]
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var success = await _trackingService.DeleteAsync(id);
        if (!success) return NotFound();

        return NoContent();
    }
}