using Domain.Commands.Visitant;
using Domain.Entities;
using Domain.Handlers;
using Domain.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("v1/visitant")]
[Authorize]
public class VisitantController : ControllerBase
{
    private readonly IRepository<Visitant> Repo;
    private readonly VisitantHandler Handler;

    public VisitantController(IRepository<Visitant> repo, VisitantHandler handler)
    {
        Repo = repo;
        Handler = handler;
    }

    //Queries
    [HttpGet("get/{Id}")]
    public IActionResult GetById([FromRoute] Guid Id)
    {
        var result = Repo.GetById(Id);

        if (result == null)
            return NotFound(new ControllerResult(false, "object not found"));

        return Ok(new ControllerResult(true, result));
    }


    //Commands
    [HttpPut("changeStatus")]
    public IActionResult ChangeActive([FromBody] ChangeActiveVisitantCommand comm)
    {
        if (!ModelState.IsValid)
            return BadRequest(new ControllerResult(false, "Invalid command"));

        var result = Handler.Handle(comm);

        if (result.IsSuccess == false)
            return StatusCode(500, new HandlerResult(result.IsSuccess, result.Data));
        return Ok(new HandlerResult(true, result.Data));
    }

    [HttpPut("changeDocs")]
    public IActionResult ChangeDocument([FromBody] ChangeDocumentVisitantCommand comm)
    {
        if (!ModelState.IsValid)
            return BadRequest(new ControllerResult(false, "Invalid command"));

        var result = Handler.Handle(comm);

        if (result.IsSuccess == false)
            return StatusCode(500, new HandlerResult(result.IsSuccess, result.Data));
        return Ok(new HandlerResult(true, result.Data));
    }

    [HttpPut("changeEmail")]
    public IActionResult ChangeEmail([FromBody] ChangeEmailVisitantCommand comm)
    {
        if (!ModelState.IsValid)
            return BadRequest(new ControllerResult(false, "Invalid command"));

        var result = Handler.Handle(comm);

        if (result.IsSuccess == false)
            return StatusCode(500, new HandlerResult(result.IsSuccess, result.Data));
        return Ok(new HandlerResult(true, result.Data));
    }

    [HttpPut("changeName")]
    public IActionResult ChangeName([FromBody] ChangeNameVisitantCommand comm)
    {
        if (!ModelState.IsValid)
            return BadRequest(new ControllerResult(false, "Invalid command"));

        var result = Handler.Handle(comm);

        if (result.IsSuccess == false)
            return StatusCode(500, new HandlerResult(result.IsSuccess, result.Data));
        return Ok(new HandlerResult(true, result.Data));
    }

    [HttpPut("changePhone")]
    public IActionResult ChangePhone([FromBody] ChangePhoneNumberVisitantCommand comm)
    {
        if (!ModelState.IsValid)
            return BadRequest(new ControllerResult(false, "Invalid command"));

        var result = Handler.Handle(comm);

        if (result.IsSuccess == false)
            return StatusCode(500, new HandlerResult(result.IsSuccess, result.Data));
        return Ok(new HandlerResult(true, result.Data));
    }
}