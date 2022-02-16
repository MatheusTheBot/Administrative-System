using Domain.Commands.Resident;
using Domain.Handlers;
using Infra.Repository;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("v1/resident")]
public class ResidentControllers : ControllerBase
{
    [HttpGet("get/{Id}")]
    public IActionResult GetById([FromServices] ResidentRepository repo, [FromRoute] Guid Id)
    {
        var result = repo.GetById(Id);

        if (result == null)
            return NotFound(new ControllerResult(false, "object not found"));

        return Ok(new ControllerResult(true, result));
    }

    [HttpPost("add")]
    public IActionResult AddResident([FromServices] ResidentHandler handler, [FromBody] CreateResidentCommand comm)
    {
        if (!ModelState.IsValid)
            return BadRequest(new ControllerResult(false, "Invalid Resident"));

        var result = handler.Handle(comm);

        if (result.IsSuccess == false)
            return BadRequest(new ControllerResult(true, result.Data));

        return Ok(new ControllerResult(true, $"Object created successfuly; ID:{result.Data.Id}"));
    }

    [HttpPut("changeDocs")]
    public IActionResult ChangeDocument([FromServices] ResidentHandler handler, [FromBody] ChangeDocumentResidentCommand comm)
    {
        if (!ModelState.IsValid)
            return BadRequest(new ControllerResult(false, "Invalid command"));

        var result = handler.Handle(comm);

        if (result.IsSuccess == false)
            return StatusCode(500, new HandlerResult(result.IsSuccess, result.Data));
        return Ok(new HandlerResult(true, result));
    }

    [HttpPut("changeEmail")]
    public IActionResult ChangeEmail([FromServices] ResidentHandler handler, [FromBody] ChangeEmailResidentCommand comm)
    {
        if (!ModelState.IsValid)
            return BadRequest(new ControllerResult(false, "Invalid command"));

        var result = handler.Handle(comm);

        if (result.IsSuccess == false)
            return StatusCode(500, new HandlerResult(result.IsSuccess, result.Data));
        return Ok(new HandlerResult(true, result));
    }

    [HttpPut("changeName")]
    public IActionResult ChangeName([FromServices] ResidentHandler handler, [FromBody] ChangeNameResidentCommand comm)
    {
        if (!ModelState.IsValid)
            return BadRequest(new ControllerResult(false, "Invalid command"));

        var result = handler.Handle(comm);

        if (result.IsSuccess == false)
            return StatusCode(500, new HandlerResult(result.IsSuccess, result.Data));
        return Ok(new HandlerResult(true, result));
    }

    [HttpPut("changePhone")]
    public IActionResult ChangePhone([FromServices] ResidentHandler handler, [FromBody] ChangePhoneNumberResidentCommand comm)
    {
        if (!ModelState.IsValid)
            return BadRequest(new ControllerResult(false, "Invalid command"));

        var result = handler.Handle(comm);

        if (result.IsSuccess == false)
            return StatusCode(500, new HandlerResult(result.IsSuccess, result.Data));
        return Ok(new HandlerResult(true, result));
    }
}