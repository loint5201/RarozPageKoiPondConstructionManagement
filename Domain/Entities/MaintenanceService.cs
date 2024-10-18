namespace Domain.Entities;

public partial class MaintenanceService
{
    public int ServiceId { get; set; }

    public string? ServiceName { get; set; } = null!;

    public string? ServiceImage { get; set; }

    public string? Description { get; set; }

    public string? Price { get; set; }

    public int Order { get; set; } = 0;

    public bool RequireDesign { get; set; } = false;

    public virtual ICollection<ServiceRequest> ServiceRequests { get; set; } = new List<ServiceRequest>();

    public virtual ICollection<ConstructionRequest> ConstructionRequests { get; set; } = new List<ConstructionRequest>();
}
