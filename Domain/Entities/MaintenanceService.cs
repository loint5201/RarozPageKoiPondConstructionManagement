namespace Domain.Entities;

public partial class MaintenanceService
{
    public int ServiceId { get; set; }

    public string ServiceName { get; set; } = null!;

    public string ServiceImage { get; set; }

    public string? Description { get; set; }

    public string Price { get; set; }

    public virtual ICollection<ServiceRequest> ServiceRequests { get; set; } = new List<ServiceRequest>();
}
