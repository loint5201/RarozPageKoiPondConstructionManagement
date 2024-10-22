namespace Domain.Entities;

public partial class ConstructionProcess
{
    public int ProcessId { get; set; }

    public int? RequestId { get; set; }

    public int? Step { get; set; } = null!;

    public string? StepInfo { get; set; } = null!;

    public string? Note { get; set; } = null!;

    public int? Status { get; set; } = null!;

    public int? AssignedStaffId { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public virtual User? AssignedStaff { get; set; }

    public virtual ConstructionRequest? Request { get; set; }
}
