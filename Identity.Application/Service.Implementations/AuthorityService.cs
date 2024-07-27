namespace Identity.Application.Service.Implementations;

public class AuthorityService(IAuthorityRepository authorityRepsitory, IMapper mapper) : IAuthorityService
{
    private readonly IAuthorityRepository _authorityRepsitory = authorityRepsitory;
    private readonly IMapper _mapper = mapper;
    public IResult GetAll()
    {
        var authorities = _authorityRepsitory.GetAll(a => !a.IsDeleted);

        if (!authorities.Any())
            return Result.Unavailable(ResponseConstants.NoElements);

        var authoritiesDto = _mapper.Map<IEnumerable<AuthorityIndexDto>>(authorities);

        return Result.Success(authoritiesDto);
    }
    public IResult GetById(int id)
    {
        var authority = _authorityRepsitory.GetById(id);

        if (authority is null)
            return Result.NotFound(ResponseConstants.NotFound);

        var dto = _mapper.Map<AuthorityIndexDto>(authority);
        return Result.Success(dto);
    }
    public IResult Create(AuthorityDto authorityDto)
    {
        var authority = _mapper.Map<Authority>(authorityDto);
        authority.CreatedAt = DateTime.Now;

        authority = _authorityRepsitory.Create(authority);
        if (authority is null)
            return Result.Conflict();

        return Result.SuccessWithMessage(ResponseConstants.CreatedSuccessfully);
    }
    public IResult Update(int id, AuthorityDto authorityDto)
    {
        var authority = _authorityRepsitory.GetById(id);
        if (authority is null)
            return Result.NotFound(ResponseConstants.NotFound);

        authority = _mapper.Map(authorityDto, authority);
        authority.UpdatedAt = DateTime.Now;
        authority = _authorityRepsitory.Update(authority);

        var dto = _mapper.Map<AuthorityIndexDto>(authority);
        return Result.Success(dto, ResponseConstants.UpdatedSuccessfully);

    }
    public IResult Delete(int id)
    {
        var authority = _authorityRepsitory.GetById(id);
        if (authority is null)
            return Result.NotFound(ResponseConstants.NotFound);

        authority.UpdatedAt = DateTime.Now;
        _authorityRepsitory.Delete(authority);

        var dto = _mapper.Map<AuthorityIndexDto>(authority);
        return Result.Success(dto, ResponseConstants.DeletedSuccessfully);
    }
}