namespace Domain.Entities;

public partial class Feedback
{
    public int FeedbackId { get; set; }

    public int? CustomerId { get; set; }

    public int? RequestId { get; set; }

    public int? ServiceRequestId { get; set; }

    public int? Rating { get; set; }

    public string? Comment { get; set; }

    public DateTime CreatedAt { get; set; }

    public virtual User? Customer { get; set; }

    public virtual ConstructionRequest? Request { get; set; }

    public virtual ServiceRequest? ServiceRequest { get; set; }
}
