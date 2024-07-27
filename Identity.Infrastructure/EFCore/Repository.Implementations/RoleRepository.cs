using AutoMapper;
using System.Data;

namespace Identity.Infrastructure.EFCore.Repository.Implementations;

public class RoleRepository(ApplicationDbContext context, IMapper mapper) : IRoleRepository
{
    private readonly ApplicationDbContext _context = context;
    private readonly IMapper _mapper = mapper;
    public IEnumerable<Role> GetAll(Expression<Func<Role, bool>> filter = null!)
    {
        var roles = _context.Roles
            .Where(filter is null ? a => true : filter)
            .AsNoTracking()
            .ToList();
        return roles;
    }
    public Role GetById(int id, bool isUpdate = false)
    {
        Role role = null!;
        if (isUpdate)
            role = _context.Roles
            .Where(a => !a.IsDeleted)
            .FirstOrDefault(a => a.Id == id)!;

        else
            role = _context.Roles
            .Where(a => !a.IsDeleted)
            .AsNoTracking()
            .FirstOrDefault(a => a.Id == id)!;

        return role;
    }
    public Role Create(Role role)
    {
        _context.Roles.Add(role);
        _context.Entry(role)
            .Collection(r => r.Authorities)
            .Query()
            .Include(ra => ra.Authority)
            .Load();
        _context.SaveChanges();

        role = GetById(role.Id);
        return role;
    }
    public Role Update(Role role)
    {
        _context.Roles.Update(role);
        _context.SaveChanges();
        role = GetById(role.Id);
        return role;
    }
    public Role Delete(Role role)
    {
        role.IsDeleted = true;
        _context.SaveChanges();
        return role;
    }
    public void DeleteAssociatedAuthorities(int id)
    {
        var roleAuthorities = _context.RoleAuthority.Where(ra => ra.RoleId == id);
        _context.RoleAuthority.RemoveRange(roleAuthorities);
        _context.SaveChanges();
    }
}
