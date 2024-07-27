namespace Identity.Application.Service.Interfaces;
public interface IRoleService
{
    public IResult GetAll();
    public IResult GetById(int id);
    public IResult Create(RoleDto roleDto);
    public IResult Update(int id, RoleDto roleDto);
    public IResult Delete(int id);
}