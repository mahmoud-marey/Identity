namespace Identity.Application.Mapping;
public class MappingProfile : Profile
{
    public MappingProfile()
    {
        #region Authorities Mapping
        CreateMap<Authority, AuthorityIndexDto>().ReverseMap();
        CreateMap<AuthorityDto, Authority>();
        #endregion

        #region Roles Mapping
        CreateMap<Role, RoleIndexDto>()
            .ForMember(dest => dest.Authorities,
                opt => opt.MapFrom(
                    src => src.Authorities
                        .Select(a => new AuthorityViewDto { Id = a.AuthorityId, Name = a.Authority == null ? "" : a.Authority.Name }))).ReverseMap();

        CreateMap<RoleDto, Role>();
        CreateMap<Role, RoleViewDto>();
        #endregion
    }
}
