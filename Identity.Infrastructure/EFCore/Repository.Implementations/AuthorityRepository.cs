namespace Identity.Infrastructure.EFCore.Repository.Implementations;
public class AuthorityRepository(ApplicationDbContext context) : IAuthorityRepository
{
    private readonly ApplicationDbContext _context = context;
    public IEnumerable<Authority> GetAll(Expression<Func<Authority, bool>> filter = null!)
    {
        var authorities = _context.Authorities.Where(filter is null ? a => true : filter).AsNoTracking().ToList();
        return authorities;
    }
    public Authority GetById(int id)
    {
        var authority = _context.Authorities.Where(a => !a.IsDeleted).FirstOrDefault(a => a.Id == id);
        return authority!;
    }
    public Authority Create(Authority authority)
    {
        _context.Authorities.Add(authority);
        _context.SaveChanges();
        return authority;
    }
    public Authority Update(Authority authority)
    {
        _context.Authorities.Update(authority);
        _context.SaveChanges();
        return authority;
    }
    public Authority Delete(Authority authority)
    {
        authority.IsDeleted = true;
        _context.SaveChanges();
        return authority;
    }
    public bool ValidateAuthoritiesExist(IEnumerable<int> authorityIds, out List<int> notFoundIds)
    {
        bool allExist = true;
        notFoundIds = new List<int>();
        var existingAuthorityIds = _context.Authorities
            .Where(a => !a.IsDeleted)
            .Select(a => a.Id)
            .ToList();

        foreach (int authorityId in authorityIds)
        {
            if (!existingAuthorityIds.Contains(authorityId))
            {
                allExist = false;
                notFoundIds.Add(authorityId);
            }
        }

        return allExist;
    }
}