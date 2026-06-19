namespace EloGreen.Application.ViewModels.Response;

public class SupplierResponse
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Document { get; set; } = string.Empty;
    public bool IsEsgCertified { get; set; }
    public DateTime CreatedAt { get; set; }
}