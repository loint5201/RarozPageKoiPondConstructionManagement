namespace Domain.Entities;

public partial class ConstructionRequest
{
    public int RequestId { get; set; }

    public int? CustomerId { get; set; }

    public int? DesignId { get; set; }

    public int? MaintenanceServiceId { get; set; }

    public string? CustomDesignDescription { get; set; }

    public decimal? CostEstimate { get; set; }

    public int? Status { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public virtual ICollection<ConstructionProcess> ConstructionProcesses { get; set; } = new List<ConstructionProcess>();

    public virtual User? Customer { get; set; }

    public virtual ICollection<CustomerOrderHistory> CustomerOrderHistories { get; set; } = new List<CustomerOrderHistory>();

    public virtual KoiDesign? Design { get; set; }

    public virtual ICollection<Feedback> Feedbacks { get; set; } = new List<Feedback>();

    public MaintenanceService? MaintenanceService { get; set; }
}
