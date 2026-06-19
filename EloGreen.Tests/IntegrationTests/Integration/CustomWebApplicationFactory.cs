using EloGreen.Domain.Entities;
using EloGreen.Infrastructure.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestPlatform.TestHost;

namespace EloGreen.Tests.Integration;

public class CustomWebApplicationFactory : WebApplicationFactory<EloGreen.API.Program>
{
    public readonly Guid TestProductId = Guid.Parse("11111111-1111-1111-1111-111111111111");
    public readonly Guid TestSupplierId = Guid.Parse("22222222-2222-2222-2222-222222222222");

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureServices(services =>
        {
            var descriptor = services.SingleOrDefault(d => d.ServiceType == typeof(DbContextOptions<DatabaseContext>));
            if (descriptor != null) services.Remove(descriptor);

            services.AddDbContext<DatabaseContext>(options =>
            {
                options.UseInMemoryDatabase("InMemoryDbForTesting");
            });

            var sp = services.BuildServiceProvider();
            using var scope = sp.CreateScope();
            var db = scope.ServiceProvider.GetRequiredService<DatabaseContext>();

            db.Database.EnsureCreated();

            if (!db.Suppliers.Any())
            {
                db.Suppliers.Add(new Supplier
                {
                    Id = TestSupplierId,
                    Name = "Fornecedor Teste",
                    Document = "12345678901234",
                    IsEsgCertified = true,
                    CreatedAt = DateTime.UtcNow
                });

                db.Products.Add(new Product
                {
                    Id = TestProductId,
                    Name = "Produto Teste",
                    BaseCarbonFootprint = 100m,
                    SupplierId = TestSupplierId,
                    CreatedAt = DateTime.UtcNow
                });

                db.CarbonTrackings.Add(new CarbonTracking
                {
                    Id = Guid.NewGuid(),
                    ProductId = TestProductId,
                    ActivityDescription = "Teste Emissão Integrado",
                    CarbonEmitted = 50m,
                    TrackingDate = DateTime.UtcNow
                });

                db.SaveChanges();
            }
        });
    }
}