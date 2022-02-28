using Domain.Commands.Apart;
using Domain.Entities;
using Domain.Handlers;
using Domain.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("v1/apart")]
[Authorize]
public class ApartController : ControllerBase
{
    private readonly IRepository<Apart> Repo;
    private readonly ApartHandler Handler;

    public ApartController(IRepository<Apart> repo, ApartHandler handler)
    {
        Repo = repo;
        Handler = handler;
    }

    //Queries
    [HttpGet("get/{Apart}/{Block}")]
    [Authorize("Admin")]
    public IActionResult Get([FromRoute] int Apart, [FromRoute] int Block)
    {
        if (Block.Equals(0) || Apart.Equals(0))
            return NotFound(new ControllerResult(false, "Invalid Id"));

        var result = Repo.GetById(Apart, Block);

        if (result == null)
            return NotFound(new ControllerResult(false, "Apart not found"));

        return Ok(new ControllerResult(true, result));
    }

    [HttpGet("get/all-visitants/{Apart}/{Block}")]
    public IActionResult GetVisitants([FromRoute] int Apart, [FromRoute] int Block)
    {
        if (Block.Equals(0) || Apart.Equals(0))
            return NotFound(new ControllerResult(false, "Invalid Id"));

        var result = Repo.GetById(Apart, Block);

        if (result == null)
            return NotFound(new ControllerResult(false, "Apart not found"));

        return Ok(new ControllerResult(true, result.Visitants));
    }

    [HttpGet("get/all-packages/{Apart}/{Block}")]
    public IActionResult GetPackages([FromRoute] int Apart, [FromRoute] int Block)
    {
        if (Block.Equals(0) || Apart.Equals(0))
            return NotFound(new ControllerResult(false, "Invalid Id"));

        var result = Repo.GetById(Apart, Block);

        if (result == null)
            return NotFound(new ControllerResult(false, "Apart not found"));

        return Ok(new ControllerResult(true, result.Packages));
    }

    [HttpGet("get/all-residents/{Apart}/{Block}")]
    public IActionResult GetResidents([FromRoute] int Apart, [FromRoute] int Block)
    {
        if (Block.Equals(0) || Apart.Equals(0))
            return NotFound(new ControllerResult(false, "Invalid Id"));

        var result = Repo.GetById(Apart, Block);

        if (result == null)
            return NotFound(new ControllerResult(false, "Apart not found"));

        return Ok(new ControllerResult(true, result.Residents));
    }


    //Commands
    [HttpPost("add")]
    [Authorize("Admin")]
    public IActionResult AddApart([FromBody] CreateApartCommand comm)
    {
        if (!ModelState.IsValid)
            return BadRequest(new ControllerResult(false, "Invalid Apart"));

        var result = Handler.Handle(comm);

        if (result.IsSuccess == false)
            return BadRequest(new ControllerResult(false, result.Data));

        return StatusCode(201, new ControllerResult(true, $"Apart created successfuly; Apart: {comm.Number}, Block: {comm.Block}"));
    }

    [HttpPut("add/package")]
    public IActionResult AddPackage([FromServices] PackagesHandler packagesHandler, [FromBody] AddPackageToApartCommand comm)
    {
        if (!ModelState.IsValid)
            return BadRequest(new ControllerResult(false, "Invalid command"));

        var packResult = packagesHandler.Handle(comm.Package);

        if (packResult.IsSuccess == false)
            return StatusCode(500, new HandlerResult(packResult.IsSuccess, packResult.Data));

        return Redirect($"https://localhost:7167/v1/packages/get/{packResult.Data.Id}");
    }

    [HttpPut("add/resident")]
    public IActionResult AddResident([FromServices] ResidentHandler residentHandler, [FromBody] AddResidentToApartCommand comm)
    {
        if (!ModelState.IsValid)
            return BadRequest(new ControllerResult(false, "Invalid command"));

        var resiResult = residentHandler.Handle(comm.Resident);

        if (resiResult.IsSuccess == false)
            return StatusCode(500, new HandlerResult(resiResult.IsSuccess, resiResult.Data));

        return Redirect($"https://localhost:7167/v1/resident/get/{resiResult.Data.Id}");
    }

    [HttpPut("add/visitant")]
    public IActionResult AddVisitant([FromServices] VisitantHandler visitantHandler, [FromBody] AddVisitantToApartCommand comm)
    {
        if (!ModelState.IsValid)
            return BadRequest(new ControllerResult(false, "Invalid command"));

        var visiResult = visitantHandler.Handle(comm.Visitant);

        if (visiResult.IsSuccess == false)
            return StatusCode(500, new HandlerResult(visiResult.IsSuccess, visiResult.Data));

        return Redirect($"https://localhost:7167/v1/visitant/get/{visiResult.Data.Id}");
    }

    [HttpDelete("delete/package")]
    public IActionResult DeleteVisitant([FromBody] DeletePackageFromApartCommand comm)
    {
        if (!ModelState.IsValid)
            return BadRequest(new ControllerResult(false, "Invalid command"));

        var result = Handler.Handle(comm);

        if (result.IsSuccess == false)
            return StatusCode(500, new HandlerResult(result.IsSuccess, result.Data));

        return Ok(new HandlerResult(true, result.Data));
    }

    [HttpDelete("delete/resident")]
    public IActionResult DeleteResident([FromBody] DeleteResidentFromApartCommand comm)
    {
        if (!ModelState.IsValid)
            return BadRequest(new ControllerResult(false, "Invalid command"));

        var result = Handler.Handle(comm);

        if (result.IsSuccess == false)
            return StatusCode(500, new HandlerResult(result.IsSuccess, result.Data));
        return Ok(new HandlerResult(true, result.Data));
    }

    [HttpDelete("delete/visitant")]
    public IActionResult DeleteVisitant([FromBody] DeleteVisitantFromApartCommand comm)
    {
        if (!ModelState.IsValid)
            return BadRequest(new ControllerResult(false, "Invalid command"));

        var result = Handler.Handle(comm);

        if (result.IsSuccess == false)
            return StatusCode(500, new HandlerResult(result.IsSuccess, result.Data));
        return Ok(new HandlerResult(true, result.Data));
    }
}