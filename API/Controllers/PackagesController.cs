using Domain.Commands.Packages;
using Domain.Handlers;
using Infra.Repository;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("v1/packages")]
public class PackagesController : ControllerBase
{
    //Queries
    [HttpGet("get/{Id}")]
    public IActionResult GetById([FromServices] PackageRepository repo, [FromRoute] Guid Id)
    {
        var result = repo.GetById(Id);

        if (result == null)
            return NotFound(new ControllerResult(false, "object not found"));

        return Ok(new ControllerResult(true, result));
    }

    //Commands
    [HttpPost("add")]
    public IActionResult AddPackages([FromServices] PackagesHandler handler, [FromBody] CreatePackageCommand comm)
    {
        if (!ModelState.IsValid)
            return BadRequest(new ControllerResult(false, "Invalid command"));

        var result = handler.Handle(comm);

        if (result.IsSuccess == false)
            return BadRequest(new ControllerResult(true, result.Data));

        return Ok(new ControllerResult(true, $"Object created successfuly; ID:{result.Data.Id}"));
    }

    [HttpPut("update")]
    public IActionResult UpdatePackages([FromServices] PackagesHandler handler, [FromBody] UpdatePackageCommand comm)
    {
        if (!ModelState.IsValid)
            return BadRequest(new ControllerResult(false, "Invalid command"));

        var result = handler.Handle(comm);

        if (result.IsSuccess == false)
            return StatusCode(500, new HandlerResult(result.IsSuccess, result.Data));
        return Ok(new HandlerResult(true, result));
    }

    [HttpPut("changeType")]
    public IActionResult ChangeType([FromServices] PackagesHandler handler, [FromBody] ChangePackageTypeCommand comm)
    {
        if (!ModelState.IsValid)
            return BadRequest(new ControllerResult(false, "Invalid command"));

        var result = handler.Handle(comm);

        if (result.IsSuccess == false)
            return StatusCode(500, new HandlerResult(result.IsSuccess, result.Data));
        return Ok(new HandlerResult(true, result));
    }
}
