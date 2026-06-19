namespace EloGreen.Application.ViewModels.Request;

public class UpdateSupplierRequest
{
    public string Name { get; set; } = string.Empty;
    public string Document { get; set; } = string.Empty;
    public bool IsEsgCertified { get; set; }
}