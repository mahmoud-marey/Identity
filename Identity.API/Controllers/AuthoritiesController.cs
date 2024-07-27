using Microsoft.AspNetCore.Mvc;

namespace Identity.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthoritiesController(IAuthorityService authorityService) : ControllerBase
{
    private readonly IAuthorityService _authorityService = authorityService;

    [HttpGet]
    public IActionResult GetAll()
    {
        var result = _authorityService.GetAll();
        return Ok(result);
    }

    [HttpGet("{id}")]
    public IActionResult GetById(int id)
    {
        var result = _authorityService.GetById(id);
        return Ok(result);
    }

    [HttpPost]
    public IActionResult Create([FromBody] AuthorityDto authorityDto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var result = _authorityService.Create(authorityDto);

        return Ok(result);
    }

    [HttpPut("{id}")]
    public IActionResult Update(int id, [FromBody] AuthorityDto authorityDto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var result = _authorityService.Update(id, authorityDto);

        return Ok(result);
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        var result = _authorityService.Delete(id);
        return Ok(result);
    }
}
