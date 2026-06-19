namespace EloGreen.Domain.Entities
{
    public class Product
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public decimal BaseCarbonFootprint { get; set; } // Pegada de carbono média para fabricar 1 unidade (em kg CO2)
        public DateTime CreatedAt { get; set; }
        public Guid SupplierId { get; set; }
        public Supplier? Supplier { get; set; }
        public ICollection<CarbonTracking> CarbonTrackings { get; set; } = new List<CarbonTracking>();
    }
}
