using Domain.Commands.Visitant;
using Domain.Handlers;
using Infra.Repository;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("v1/visitant")]

public class VisitantController : ControllerBase
{
    //Queries
    [HttpGet("get/{Id}")]
    public IActionResult GetById([FromServices] VisitantRepository repo, [FromRoute] Guid Id)
    {
        var result = repo.GetById(Id);

        if (result == null)
            return NotFound(new ControllerResult(false, "object not found"));

        return Ok(new ControllerResult(true, result));
    }


    //Commands
    [HttpPost("add")]
    public IActionResult AddVisitant([FromServices] VisitantHandler handler, [FromBody] CreateVisitantCommand comm)
    {
        if (!ModelState.IsValid)
            return BadRequest(new ControllerResult(false, "Invalid Visitant"));

        var result = handler.Handle(comm);

        if(result.IsSuccess == false)
            return BadRequest(new ControllerResult(true, result.Data));

        return Ok(new ControllerResult(true, $"Object created successfuly; ID:{result.Data.Id}"));
    }

    [HttpPut("changeStatus")]
    public IActionResult ChangeActive([FromServices] VisitantHandler handler, [FromBody] ChangeActiveVisitantCommand comm)
    {
        if (!ModelState.IsValid)
            return BadRequest(new ControllerResult(false, "Invalid command"));

        var result = handler.Handle(comm);

        if (result.IsSuccess == false)
            return StatusCode(500, new HandlerResult(result.IsSuccess, result.Data));
        return Ok(new HandlerResult(true, result));
    }

    [HttpPut("changeDocs")]
    public IActionResult ChangeDocument([FromServices] VisitantHandler handler, [FromBody] ChangeDocumentVisitantCommand comm)
    {
        if (!ModelState.IsValid)
            return BadRequest(new ControllerResult(false, "Invalid command"));

        var result = handler.Handle(comm);

        if (result.IsSuccess == false)
            return StatusCode(500, new HandlerResult(result.IsSuccess, result.Data));
        return Ok(new HandlerResult(true, result));
    }

    [HttpPut("changeEmail")]
    public IActionResult ChangeEmail([FromServices] VisitantHandler handler, [FromBody] ChangeEmailVisitantCommand comm)
    {
        if (!ModelState.IsValid)
            return BadRequest(new ControllerResult(false, "Invalid command"));

        var result = handler.Handle(comm);

        if (result.IsSuccess == false)
            return StatusCode(500, new HandlerResult(result.IsSuccess, result.Data));
        return Ok(new HandlerResult(true, result));
    }

    [HttpPut("changeName")]
    public IActionResult ChangeName([FromServices] VisitantHandler handler, [FromBody] ChangeNameVisitantCommand comm)
    {
        if (!ModelState.IsValid)
            return BadRequest(new ControllerResult(false, "Invalid command"));

        var result = handler.Handle(comm);

        if (result.IsSuccess == false)
            return StatusCode(500, new HandlerResult(result.IsSuccess, result.Data));
        return Ok(new HandlerResult(true, result));
    }

    [HttpPut("changePhone")]
    public IActionResult ChangePhone([FromServices] VisitantHandler handler, [FromBody] ChangePhoneNumberVisitantCommand comm)
    {
        if (!ModelState.IsValid)
            return BadRequest(new ControllerResult(false, "Invalid command"));

        var result = handler.Handle(comm);

        if (result.IsSuccess == false)
            return StatusCode(500, new HandlerResult(result.IsSuccess, result.Data));
        return Ok(new HandlerResult(true, result));
    }
}