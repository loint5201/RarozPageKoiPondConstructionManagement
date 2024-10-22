using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities;

public partial class Role
{
    public int RoleId { get; set; }

    public string? RoleName { get; set; } = null!;

    [NotMapped]
    public string? RoleNameVietnam { get; set; } = null!;

    public virtual ICollection<User> Users { get; set; } = new List<User>();
}
