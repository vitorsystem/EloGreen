namespace EloGreen.Application.ViewModels.Request;

public class UpdateProductRequest
{
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public decimal BaseCarbonFootprint { get; set; }
    public Guid SupplierId { get; set; }
}