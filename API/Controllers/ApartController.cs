using Domain.Commands.Apart;
using Domain.Handlers;
using Infra.Repository;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("v1/apart")]
public class ApartController : ControllerBase
{

    //Queries
    [HttpGet("get/{int Apart}/{int Block}")]
    public IActionResult GetById([FromServices] ApartRepository repo, [FromRoute] int Apart, [FromRoute] int Block)
    {
        if (Block.Equals(0) || Apart.Equals(0))
            return NotFound(new ControllerResult(false, "Invalid Id"));

        var result = repo.GetById(Apart, Block);
        if (result == null)
            return NotFound(new ControllerResult(false, "Apart not found"));

        return Ok(new ControllerResult(true, result));
    }

    //Commands
    [HttpPost("add")]
    public IActionResult AddApart([FromServices] ApartHandler handler, [FromBody] CreateApartCommand comm)
    {
        if (!ModelState.IsValid)
            return BadRequest(new ControllerResult(false, "Invalid Apart"));

        var result = handler.Handle(comm);

        if (result.IsSuccess == false)
            return BadRequest(new ControllerResult(false, result.Data));

        return StatusCode(201, new ControllerResult(true, $"Apart created successfuly; Apart: {comm.Number}, Block: {comm.Block}"));
    }

    [HttpPut("add/package")]
    public IActionResult AddPackage([FromServices] PackagesHandler packagesHandler, [FromServices] ApartHandler handler, [FromBody] AddPackageToApartCommand comm)
    {
        if (!ModelState.IsValid)
            return BadRequest(new ControllerResult(false, "Invalid command"));

        var result = handler.Handle(comm);

        if (result.IsSuccess == false)
            return StatusCode(500, new HandlerResult(result.IsSuccess, result.Data));

        var packResult = packagesHandler.Handle(comm.Package);

        if (packResult.IsSuccess == false)
            return StatusCode(500, new HandlerResult(packResult.IsSuccess, packResult.Data));

        return Ok(new HandlerResult(true, result));
    }

    [HttpPut("add/resident")]
    public IActionResult AddResident([FromServices] ResidentHandler residentHandler, [FromServices] ApartHandler handler, [FromBody] AddResidentToApartCommand comm)
    {
        if (!ModelState.IsValid)
            return BadRequest(new ControllerResult(false, "Invalid command"));

        var result = handler.Handle(comm);

        if (result.IsSuccess == false)
            return StatusCode(500, new HandlerResult(result.IsSuccess, result.Data));

        var residentResult = residentHandler.Handle(comm.Resident);

        if (residentResult.IsSuccess == false)
            return StatusCode(500, new HandlerResult(residentResult.IsSuccess, residentResult.Data));

        return Ok(new HandlerResult(true, result));
    }

    [HttpPut("add/visitant")]
    public IActionResult AddVisitant([FromServices] VisitantHandler visitantHandler, [FromServices] ApartHandler handler, [FromBody] AddVisitantToApartCommand comm)
    {
        if (!ModelState.IsValid)
            return BadRequest(new ControllerResult(false, "Invalid command"));

        var result = handler.Handle(comm);

        if (result.IsSuccess == false)
            return StatusCode(500, new HandlerResult(result.IsSuccess, result.Data));

        var visitantResult = visitantHandler.Handle(comm.Visitant);

        if (visitantResult.IsSuccess == false)
            return StatusCode(500, new HandlerResult(visitantResult.IsSuccess, visitantResult.Data));

        return Ok(new HandlerResult(true, result));
    }

    [HttpDelete("delete/package")]
    public IActionResult DeleteVisitant([FromServices] ApartHandler handler, [FromBody] DeletePackageFromApartCommand comm)
    {
        if (!ModelState.IsValid)
            return BadRequest(new ControllerResult(false, "Invalid command"));

        var result = handler.Handle(comm);

        if (result.IsSuccess == false)
            return StatusCode(500, new HandlerResult(result.IsSuccess, result.Data));
        return Ok(new HandlerResult(true, result));
    }

    [HttpDelete("delete/resident")]
    public IActionResult DeleteResident([FromServices] ApartHandler handler, [FromBody] DeleteResidentFromApartCommand comm)
    {
        if (!ModelState.IsValid)
            return BadRequest(new ControllerResult(false, "Invalid command"));

        var result = handler.Handle(comm);

        if (result.IsSuccess == false)
            return StatusCode(500, new HandlerResult(result.IsSuccess, result.Data));
        return Ok(new HandlerResult(true, result));
    }

    [HttpDelete("delete/visitant")]
    public IActionResult DeleteVisitant([FromServices] ApartHandler handler, [FromBody] DeleteVisitantFromApartCommand comm)
    {
        if (!ModelState.IsValid)
            return BadRequest(new ControllerResult(false, "Invalid command"));

        var result = handler.Handle(comm);

        if (result.IsSuccess == false)
            return StatusCode(500, new HandlerResult(result.IsSuccess, result.Data));
        return Ok(new HandlerResult(true, result));
    }
}