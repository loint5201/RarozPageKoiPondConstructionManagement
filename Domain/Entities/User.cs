namespace Domain.Entities;

public partial class User
{
    public int UserId { get; set; }
    public string? Avatar { get; set; }

    public string? FullName { get; set; } = null!;

    public string? Email { get; set; } = null!;

    public string? PasswordHash { get; set; } = null!;

    public string? PhoneNumber { get; set; }

    public string? Address { get; set; }

    public int? RoleId { get; set; }

    public virtual ICollection<ConstructionProcess> ConstructionProcesses { get; set; } = new List<ConstructionProcess>();

    public virtual ICollection<ConstructionRequest> ConstructionRequests { get; set; } = new List<ConstructionRequest>();

    public virtual ICollection<CustomerOrderHistory> CustomerOrderHistories { get; set; } = new List<CustomerOrderHistory>();

    public virtual ICollection<Feedback> Feedbacks { get; set; } = new List<Feedback>();

    public virtual Role? Role { get; set; }
}
