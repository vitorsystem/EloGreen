using EloGreen.Application.Services.Interfaces;
using EloGreen.Application.ViewModels.Request;
using EloGreen.Application.ViewModels.Response;
using EloGreen.Domain.Entities;
using EloGreen.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace EloGreen.Application.Services;

public class ProductService : IProductService
{
    private readonly DatabaseContext _context;

    public ProductService(DatabaseContext context)
    {
        _context = context;
    }

    public async Task<ProductResponse?> CreateProductAsync(CreateProductRequest request)
    {
        var supplierExists = await _context.Suppliers.AnyAsync(s => s.Id == request.SupplierId);
        if (!supplierExists)
        {
            return null;
        }

        var product = new Product
        {
            Name = request.Name,
            Description = request.Description,
            BaseCarbonFootprint = request.BaseCarbonFootprint,
            SupplierId = request.SupplierId,
            CreatedAt = DateTime.UtcNow
        };

        _context.Products.Add(product);
        await _context.SaveChangesAsync();

        return new ProductResponse
        {
            Id = product.Id,
            Name = product.Name,
            Description = product.Description,
            BaseCarbonFootprint = product.BaseCarbonFootprint,
            SupplierId = product.SupplierId,
            CreatedAt = product.CreatedAt
        };
    }

    public async Task<ProductResponse?> GetProductByIdAsync(Guid id)
    {
        var product = await _context.Products.FindAsync(id);
        if (product == null) return null;

        return new ProductResponse
        {
            Id = product.Id,
            Name = product.Name,
            Description = product.Description,
            BaseCarbonFootprint = product.BaseCarbonFootprint,
            SupplierId = product.SupplierId,
            CreatedAt = product.CreatedAt
        };
    }

    public async Task<PaginatedResult<ProductResponse>> GetAllAsync(int page, int size, string? name)
    {
        var query = _context.Products.AsQueryable();

        if (!string.IsNullOrEmpty(name))
        {
            query = query.Where(s => s.Name.Contains(name));
        }

        var totalItems = await query.CountAsync();

        var products = await query
            .OrderBy(s => s.Name)
            .Skip((page - 1) * size)
            .Take(size)
            .Select(p => new ProductResponse
            {
                Id = p.Id,
                Name = p.Name,
                Description = p.Description,
                CreatedAt = p.CreatedAt,
                BaseCarbonFootprint = p.BaseCarbonFootprint,
                SupplierId = p.SupplierId
            })
            .ToListAsync();

        return new PaginatedResult<ProductResponse>
        {
            CurrentPage = page,
            PageSize = size,
            TotalItems = totalItems,
            Items = products
        };
    }

    public async Task<bool> UpdateAsync(Guid id, UpdateProductRequest request)
    {
        var product = await _context.Products.FindAsync(id);
        if (product == null) return false;

        product.Name = request.Name;
        product.Description = request.Description;
        product.BaseCarbonFootprint = request.BaseCarbonFootprint;
        product.SupplierId = request.SupplierId;
        product.CreatedAt = DateTime.UtcNow;

        _context.Products.Update(product);
        await _context.SaveChangesAsync();

        return true;
    }

    public async Task<(bool NotFound, string? ErrorMessage)> DeleteAsync(Guid id)
    {
        var product = await _context.Products.FindAsync(id);
        if (product == null) return (true, null);

        _context.Products.Remove(product);
        await _context.SaveChangesAsync();

        return (false, null);
    }
}