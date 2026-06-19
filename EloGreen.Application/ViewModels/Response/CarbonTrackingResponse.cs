namespace EloGreen.Application.ViewModels.Response;

public class CarbonTrackingResponse
{
    public Guid Id { get; set; }
    public string ActivityDescription { get; set; } = string.Empty;
    public decimal CarbonEmitted { get; set; }
    public DateTime TrackingDate { get; set; }
    public Guid ProductId { get; set; }
}