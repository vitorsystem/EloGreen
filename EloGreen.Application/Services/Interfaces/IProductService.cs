using EloGreen.Application.ViewModels.Request;
using EloGreen.Application.ViewModels.Response;

namespace EloGreen.Application.Services.Interfaces;

public interface IProductService
{
    Task<ProductResponse?> CreateProductAsync(CreateProductRequest request);
    Task<ProductResponse?> GetProductByIdAsync(Guid id);
    Task<PaginatedResult<ProductResponse>> GetAllAsync(int page, int size, string? name);
    Task<bool> UpdateAsync(Guid id, UpdateProductRequest request);
    Task<(bool NotFound, string? ErrorMessage)> DeleteAsync(Guid id);
}