namespace Domain.Entities;

public partial class ServiceRequest
{
    public int ServiceRequestId { get; set; }

    public int? CustomerId { get; set; }

    public int? ServiceId { get; set; }

    public string Status { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public virtual User? Customer { get; set; }

    public virtual ICollection<Feedback> Feedbacks { get; set; } = new List<Feedback>();

    public virtual MaintenanceService? Service { get; set; }
}
