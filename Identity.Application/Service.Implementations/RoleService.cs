namespace Identity.Application.Service.Implementations;
public class RoleService(IRoleRepository roleRepsitory, IAuthorityRepository authorityRepsitory, IMapper mapper) : IRoleService
{
    private readonly IRoleRepository _roleRepsitory = roleRepsitory;
    private readonly IAuthorityRepository _authorityRepsitory = authorityRepsitory;
    private readonly IMapper _mapper = mapper;

    public IResult GetAll()
    {
        var roles = _roleRepsitory.GetAll(a => !a.IsDeleted);
        if (!roles.Any())
            Result.Unavailable(ResponseConstants.NoElements);

        var rolesDto = _mapper.Map<IEnumerable<RoleIndexDto>>(roles);

        return Result.Success(rolesDto);
    }
    public IResult GetById(int id)
    {
        var role = _roleRepsitory.GetById(id);
        if (role is null)
            return Result.NotFound(ResponseConstants.NotFound);

        var dto = _mapper.Map<RoleIndexDto>(role);
        return Result.Success(dto);
    }
    public IResult Create(RoleDto roleDto)
    {
        var result = AssertAuthorities(roleDto);

        if (result.IsNotFound())
            return result;

        var role = _mapper.Map<Role>(roleDto);
        foreach (var authority in roleDto.SelectedAuthorities)
            role.Authorities.Add(new RoleAuthority() { AuthorityId = authority });

        role.CreatedAt = DateTime.Now;

        role = _roleRepsitory.Create(role);
        var dto = _mapper.Map<RoleIndexDto>(role);
        return Result<RoleIndexDto>.Success(dto, ResponseConstants.CreatedSuccessfully);

    }
    public IResult Update(int id, RoleDto roleDto)
    {
        var result = AssertAuthorities(roleDto);

        if (result.IsNotFound())
            return result;

        var role = _roleRepsitory.GetById(id, true);
        if (role is null)
            return Result.NotFound(ResponseConstants.NotFound);
        _roleRepsitory.DeleteAssociatedAuthorities(id);
        role = _mapper.Map(roleDto, role);

        role.Authorities = new List<RoleAuthority>();

        foreach (var authority in roleDto.SelectedAuthorities)
            role.Authorities.Add(new() { AuthorityId = authority, RoleId = role.Id });


        role.UpdatedAt = DateTime.Now;
        role = _roleRepsitory.Update(role);

        var dto = _mapper.Map<RoleIndexDto>(role);
        return Result<RoleIndexDto>.Success(dto, ResponseConstants.UpdatedSuccessfully);
    }
    public IResult Delete(int id)
    {
        var role = _roleRepsitory.GetById(id);
        if (role is null)
            return Result.NotFound(ResponseConstants.NotFound);

        role.UpdatedAt = DateTime.Now;
        role = _roleRepsitory.Delete(role);

        var dto = _mapper.Map<RoleIndexDto>(role);
        return Result<RoleIndexDto>.Success(dto, ResponseConstants.DeletedSuccessfully);
    }
    private Result AssertAuthorities(RoleDto roleDto)
    {
        var authoritiesExists = _authorityRepsitory.ValidateAuthoritiesExist(roleDto.SelectedAuthorities, out List<int> notFound);
        if (!authoritiesExists)
            return Result.NotFound(string.Format
                (ResponseConstants.AuthoritiesNotFound,
                string.Join(',', notFound)));
        return Result.Success();
    }
}
