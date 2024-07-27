using Identity.Domain.Dtos.Role;
using Microsoft.AspNetCore.Mvc;

namespace Identity.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class RolesController(IRoleService roleService) : ControllerBase
{
    private readonly IRoleService _roleService = roleService;

    [HttpGet]
    public IActionResult GetAll()
    {
        var roles = _roleService.GetAll();
        if (roles is null)
            return NotFound(ResponseConstants.NoElements);

        return Ok(roles);
    }

    [HttpGet("{id}")]
    public IActionResult GetById(int id)
    {
        var role = _roleService.GetById(id);
        if (role is null)
            return BadRequest(ResponseConstants.NotFound);

        return Ok(role);
    }

    [HttpPost]
    public IActionResult Create([FromBody] RoleDto roleDto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var result = _roleService.Create(roleDto);

        return Ok(result);
    }

    [HttpPut("{id}")]
    public IActionResult Update(int id, [FromBody] RoleDto roleDto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var result = _roleService.Update(id, roleDto);
        return Ok(result);
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        var role = _roleService.Delete(id);
        return Ok(role);
    }
}
