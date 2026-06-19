using EloGreen.Application.ViewModels.Request;
using EloGreen.Application.ViewModels.Response;

namespace EloGreen.Application.Services.Interfaces;

public interface ICarbonTrackingService
{
    Task<CarbonTrackingResponse?> CreateAsync(CreateCarbonTrackingRequest request);
    Task<IEnumerable<CarbonTrackingResponse>> GetByProductIdAsync(Guid productId);
    Task<decimal?> GetTotalCarbonFootprintAsync(Guid productId);
    Task<(bool NotFound, string? ErrorMessage)> UpdateAsync(Guid id, UpdateCarbonTrackingRequest request);
    Task<bool> DeleteAsync(Guid id);
}