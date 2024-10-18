namespace Domain.Entities;

public partial class CustomerOrderHistory
{
    public int HistoryId { get; set; }

    public int? CustomerId { get; set; }

    public int? RequestId { get; set; }

    public int? PaymentMethod { get; set; }

    public int? PaymentStatus { get; set; }

    public decimal ActualCost { get; set; }

    public DateTime CreatedAt { get; set; }

    public virtual User? Customer { get; set; }

    public virtual ConstructionRequest? Request { get; set; }
}
