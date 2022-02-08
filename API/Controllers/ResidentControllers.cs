using Domain.Entities;
using Infra.Repository;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("v1/resident")]
public class ResidentControllers : ControllerBase
{
    [HttpGet("get/{Id}")]
    public IActionResult GetById([FromServices] ResidentRepository repo, [FromRoute]Guid Id)
    {
        var result = repo.GetById(Id);
        if (result == null)
            return NotFound(new ControllerResult(false, "object not found"));

        return Ok(new ControllerResult(true, result));
    }

    [HttpPost("add")]
    public IActionResult AddResident([FromServices] ResidentRepository repo, [FromBody] Resident resident)
    {
        if (resident == null)
            return BadRequest(new ControllerResult(false, "Invalid Resident"));

        repo.Create(resident);
        return StatusCode(201, new ControllerResult(true, "Object created successfuly"));
    }
}