using Identity.Domain.Dtos.Authority;
namespace Identity.Domain.Dtos.Role;
public class RoleIndexDto
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public IEnumerable<AuthorityViewDto> Authorities { get; set; } = new List<AuthorityViewDto>();
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
}
