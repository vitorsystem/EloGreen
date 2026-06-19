namespace EloGreen.Application.ViewModels.Request;

public class CreateSupplierRequest
{
    public string Name { get; set; } = string.Empty;
    public string Document { get; set; } = string.Empty;
    public bool IsEsgCertified { get; set; }
}