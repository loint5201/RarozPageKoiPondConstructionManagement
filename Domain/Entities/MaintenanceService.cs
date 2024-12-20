﻿namespace Domain.Entities;

public partial class MaintenanceService
{
    public int ServiceId { get; set; }

    public string? ServiceName { get; set; } = null!;

    public string? ServiceImage { get; set; }

    public string? Description { get; set; }

    public decimal? Price { get; set; }

    public int Order { get; set; } = 0;

    public bool RequireDesign { get; set; } = false;

    public virtual ICollection<ConstructionRequest> ConstructionRequests { get; set; } = new List<ConstructionRequest>();
}
