namespace Domain.Entities;

public partial class Promotion
{
    public int PromotionId { get; set; }

    public string? PromotionName { get; set; } = null!;

    public string? PromotionImage { get; set; } = null!;

    public string? Description { get; set; }

    public decimal DiscountPercentage { get; set; }

    public DateOnly StartDate { get; set; }

    public DateOnly EndDate { get; set; }
}
