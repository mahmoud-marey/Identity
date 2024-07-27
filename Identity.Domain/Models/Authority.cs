namespace Identity.Domain.Models;
public class Authority : ModelBase
{
    public int Id { get; set; }
    [MaxLength(50)]
    public string Name { get; set; } = null!;
    public virtual ICollection<RoleAuthority> Roles { get; set; } = new List<RoleAuthority>();
}
