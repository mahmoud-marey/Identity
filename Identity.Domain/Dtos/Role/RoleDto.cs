namespace Identity.Domain.Dtos.Role;
public class RoleDto
{
    public string Name { get; set; } = null!;
    public IEnumerable<int> SelectedAuthorities { get; set; } = null!;
}