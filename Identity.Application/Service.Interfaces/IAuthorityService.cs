namespace Identity.Application.Service.Interfaces;

public interface IAuthorityService
{
    public IResult GetAll();
    public IResult GetById(int id);
    public IResult Create(AuthorityDto authorityDto);
    public IResult Update(int id, AuthorityDto authorityDto);
    public IResult Delete(int id);
}