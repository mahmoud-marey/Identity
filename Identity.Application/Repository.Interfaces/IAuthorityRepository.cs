namespace Identity.Application.Repository.Interfaces;
public interface IAuthorityRepository
{
    public IEnumerable<Authority> GetAll(Expression<Func<Authority, bool>> filter = null!);
    public Authority GetById(int id);
    public Authority Create(Authority authority);
    public Authority Update(Authority authority);
    public Authority Delete(Authority authority);
    public bool ValidateAuthoritiesExist(IEnumerable<int> authorityIds, out List<int> notFoundIds);
}
