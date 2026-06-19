using EloGreen.Application.ViewModels.Request;
using EloGreen.Application.ViewModels.Response;

namespace EloGreen.Application.Services.Interfaces;

public interface ISupplierService
{
    Task<SupplierResponse> CreateAsync(CreateSupplierRequest request);
    Task<SupplierResponse?> GetByIdAsync(Guid id);
    Task<PaginatedResult<SupplierResponse>> GetAllAsync(int page, int size, string? name);
    Task<bool> UpdateAsync(Guid id, UpdateSupplierRequest request);
    Task<(bool NotFound, string? ErrorMessage)> DeleteAsync(Guid id);
}