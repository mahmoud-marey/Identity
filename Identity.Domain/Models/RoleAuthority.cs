namespace Identity.Domain.Models;
public class RoleAuthority
{
    public int RoleId { get; set; }
    public virtual Role? Role { get; set; }
    public int AuthorityId { get; set; }
    public virtual Authority? Authority { get; set; }
}