namespace Domain.Entities;

public partial class PaymentPolicy
{
    public int PolicyId { get; set; }

    public string? PolicyName { get; set; } = null!;

    public string? Description { get; set; }

    public bool IsActive { get; set; }
}
