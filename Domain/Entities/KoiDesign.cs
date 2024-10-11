namespace Domain.Entities;

public partial class KoiDesign
{
    public int DesignId { get; set; }

    public string DesignName { get; set; } = null!;

    public string DesignImage { get; set; } = null!;

    public string? Description { get; set; }

    public string CostEstimate { get; set; }

    public virtual ICollection<ConstructionRequest> ConstructionRequests { get; set; } = new List<ConstructionRequest>();
}
