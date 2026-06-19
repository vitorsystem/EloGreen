using EloGreen.Application.Services.Interfaces;
using EloGreen.Application.ViewModels.Request;
using EloGreen.Application.ViewModels.Response;
using EloGreen.Domain.Entities;
using EloGreen.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace EloGreen.Application.Services;

public class SupplierService : ISupplierService
{
    private readonly DatabaseContext _context;

    public SupplierService(DatabaseContext context)
    {
        _context = context;
    }

    public async Task<SupplierResponse> CreateAsync(CreateSupplierRequest request)
    {
        var supplier = new Supplier
        {
            Name = request.Name,
            Document = request.Document,
            IsEsgCertified = (bool)request.IsEsgCertified,
            CreatedAt = DateTime.UtcNow
        };

        _context.Suppliers.Add(supplier);
        await _context.SaveChangesAsync();

        return new SupplierResponse
        {
            Id = supplier.Id,
            Name = supplier.Name,
            Document = supplier.Document,
            IsEsgCertified = supplier.IsEsgCertified,
            CreatedAt = supplier.CreatedAt
        };
    }

    public async Task<SupplierResponse?> GetByIdAsync(Guid id)
    {
        var supplier = await _context.Suppliers.FindAsync(id);
        if (supplier == null) return null;

        return new SupplierResponse
        {
            Id = supplier.Id,
            Name = supplier.Name,
            Document = supplier.Document,
            IsEsgCertified = supplier.IsEsgCertified,
            CreatedAt = supplier.CreatedAt
        };
    }

    public async Task<PaginatedResult<SupplierResponse>> GetAllAsync(int page, int size, string? name)
    {
        var query = _context.Suppliers.AsQueryable();

        if (!string.IsNullOrEmpty(name))
        {
            query = query.Where(s => s.Name.Contains(name));
        }

        var totalItems = await query.CountAsync();

        var suppliers = await query
            .OrderBy(s => s.Name)
            .Skip((page - 1) * size)
            .Take(size)
            .Select(s => new SupplierResponse
            {
                Id = s.Id,
                Name = s.Name,
                Document = s.Document,
                IsEsgCertified = s.IsEsgCertified,
                CreatedAt = s.CreatedAt
            })
            .ToListAsync();

        return new PaginatedResult<SupplierResponse>
        {
            CurrentPage = page,
            PageSize = size,
            TotalItems = totalItems,
            Items = suppliers
        };
    }

    public async Task<bool> UpdateAsync(Guid id, UpdateSupplierRequest request)
    {
        var supplier = await _context.Suppliers.FindAsync(id);
        if (supplier == null) return false;

        supplier.Name = request.Name;
        supplier.Document = request.Document;
        supplier.IsEsgCertified = request.IsEsgCertified;

        _context.Suppliers.Update(supplier);
        await _context.SaveChangesAsync();

        return true;
    }

    public async Task<(bool NotFound, string? ErrorMessage)> DeleteAsync(Guid id)
    {
        var supplier = await _context.Suppliers.FindAsync(id);
        if (supplier == null) return (true, null);

        var hasProducts = await _context.Products.AnyAsync(p => p.SupplierId == id);
        if (hasProducts)
        {
            return (false, "Não é possível excluir o fornecedor, pois existem produtos atrelados a ele.");
        }

        _context.Suppliers.Remove(supplier);
        await _context.SaveChangesAsync();

        return (false, null);
    }
}