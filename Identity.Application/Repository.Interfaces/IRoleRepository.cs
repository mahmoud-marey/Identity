namespace Identity.Application.Repository.Interfaces;
public interface IRoleRepository
{
    public IEnumerable<Role> GetAll(Expression<Func<Role, bool>> filter = null!);
    public Role Create(Role role);
    public Role GetById(int id, bool isUpdate = false);
    public Role Update(Role role);
    public Role Delete(Role role);
    public void DeleteAssociatedAuthorities(int id);
}
