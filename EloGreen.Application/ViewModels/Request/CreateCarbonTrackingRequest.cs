namespace EloGreen.Application.ViewModels.Request;

public class CreateCarbonTrackingRequest
{
    public string ActivityDescription { get; set; } = string.Empty;
    public decimal CarbonEmitted { get; set; }
    public DateTime TrackingDate { get; set; }
    public Guid ProductId { get; set; }
}