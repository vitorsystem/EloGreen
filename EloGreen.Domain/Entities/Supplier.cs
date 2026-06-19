namespace EloGreen.Domain.Entities;
public class Supplier
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Document { get; set; } = string.Empty; // CNPJ
    public bool IsEsgCertified { get; set; }
    public DateTime CreatedAt { get; set; }
    public ICollection<Product> Products { get; set; } = new List<Product>();
}