using EloGreen.Application.Services.Interfaces;
using EloGreen.Application.ViewModels.Request;
using EloGreen.Application.ViewModels.Response;
using EloGreen.Domain.Entities;
using EloGreen.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace EloGreen.Application.Services;

public class CarbonTrackingService : ICarbonTrackingService
{
    private readonly DatabaseContext _context;

    public CarbonTrackingService(DatabaseContext context)
    {
        _context = context;
    }

    public async Task<CarbonTrackingResponse?> CreateAsync(CreateCarbonTrackingRequest request)
    {
        var productExists = await _context.Products.AnyAsync(p => p.Id == request.ProductId);
        if (!productExists) return null;

        var tracking = new CarbonTracking
        {
            ActivityDescription = request.ActivityDescription,
            CarbonEmitted = request.CarbonEmitted,
            TrackingDate = request.TrackingDate,
            ProductId = request.ProductId
        };

        _context.CarbonTrackings.Add(tracking);
        await _context.SaveChangesAsync();

        return new CarbonTrackingResponse
        {
            Id = tracking.Id,
            ActivityDescription = tracking.ActivityDescription,
            CarbonEmitted = tracking.CarbonEmitted,
            TrackingDate = tracking.TrackingDate,
            ProductId = tracking.ProductId
        };
    }

    public async Task<IEnumerable<CarbonTrackingResponse>> GetByProductIdAsync(Guid productId)
    {
        return await _context.CarbonTrackings
            .Where(ct => ct.ProductId == productId)
            .OrderByDescending(ct => ct.TrackingDate)
            .Select(ct => new CarbonTrackingResponse
            {
                Id = ct.Id,
                ActivityDescription = ct.ActivityDescription,
                CarbonEmitted = ct.CarbonEmitted,
                TrackingDate = ct.TrackingDate,
                ProductId = ct.ProductId
            })
            .ToListAsync();
    }

    public async Task<decimal?> GetTotalCarbonFootprintAsync(Guid productId)
    {
        var product = await _context.Products.FindAsync(productId);
        if (product == null) return null;

        var trackedEmissions = await _context.CarbonTrackings
            .Where(ct => ct.ProductId == productId)
            .SumAsync(ct => ct.CarbonEmitted);

        return product.BaseCarbonFootprint + trackedEmissions;
    }

    public async Task<(bool NotFound, string? ErrorMessage)> UpdateAsync(Guid id, UpdateCarbonTrackingRequest request)
    {
        var tracking = await _context.CarbonTrackings.FindAsync(id);
        if (tracking == null) return (true, null);

        if (tracking.ProductId != request.ProductId)
        {
            var productExists = await _context.Products.AnyAsync(p => p.Id == request.ProductId);
            if (!productExists) return (false, "O novo Produto informado não existe na base de dados.");
        }

        tracking.ActivityDescription = request.ActivityDescription;
        tracking.CarbonEmitted = request.CarbonEmitted;
        tracking.TrackingDate = request.TrackingDate;
        tracking.ProductId = request.ProductId;

        _context.CarbonTrackings.Update(tracking);
        await _context.SaveChangesAsync();

        return (false, null);
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        var tracking = await _context.CarbonTrackings.FindAsync(id);
        if (tracking == null) return false;

        _context.CarbonTrackings.Remove(tracking);
        await _context.SaveChangesAsync();

        return true;
    }
}