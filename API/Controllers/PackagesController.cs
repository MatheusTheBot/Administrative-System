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
        if (Id.ToString() == string.Empty)
            return NotFound(new ControllerResult(false, "Invalid Id"));

        var result = repo.GetById(Id);
        if (result == null)
            return NotFound(new ControllerResult(false, "object not found"));

        return Ok(new ControllerResult(true, result));
    }

    //Commands
    [HttpPost("add/{Apart},{Block}")]
    public IActionResult AddPackages([FromServices] ApartRepository repos, [FromBody] CreatePackageCommand comm, [FromServices] PackagesHandler handler, [FromRoute] int Apart, int Block)
    {
        if (!ModelState.IsValid)
            return BadRequest(new ControllerResult(false, "Invalid Packages"));

        var result = handler.Handle(comm);

        if(result.IsSuccess != true)
            return BadRequest(new ControllerResult(false, result.Data));

        var apart = repos.GetById(Apart, Block);
        if (apart == null)
            return BadRequest(new ControllerResult(false, "Apart not found"))
    }

    [HttpPut("update")]
    public IActionResult UpdatePackages([FromServices] PackageRepository repo, [FromBody] UpdatePackageCommand comm, [FromServices] PackagesHandler handler)
    {
        if (!ModelState.IsValid)
            return BadRequest(new ControllerResult(false, "Invalid Packages"));

        var package = repo.GetById(comm.PackageId);

        if (package == null)
            return BadRequest(new ControllerResult(false, "object not found"));

        var result = handler.Handle(comm);

        if (result.IsSuccess == true)
            return StatusCode(201, new ControllerResult(true, $"Object updated successfuly;"));
        return BadRequest(new ControllerResult(false, result));
    }
}
