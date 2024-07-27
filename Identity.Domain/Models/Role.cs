namespace Identity.Domain.Models;
public class Role : ModelBase
{
    public int Id { get; set; }
    [MaxLength(100)]
    public string Name { get; set; } = null!;
    public virtual ICollection<RoleAuthority> Authorities { get; set; } = new List<RoleAuthority>();
}
