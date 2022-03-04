using API.Services;
using API.Tools;
using Domain.Commands.Resident;
using Domain.Entities;
using Domain.Handlers;
using Domain.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PasswordGenerator;

namespace API.Controllers;

[ApiController]
[Route("v1/resident")]
[Authorize]
public class ResidentControllers : ControllerBase
{
    private readonly IRepository<Resident> Repo;
    private readonly ResidentHandler Handler;

    public ResidentControllers(IRepository<Resident> repo, ResidentHandler handler)
    {
        Repo = repo;
        Handler = handler;
    }

    [HttpGet("get/{Id}")]
    public IActionResult GetById([FromRoute] Guid Id)
    {
        var result = Repo.GetById(Id);

        if (result == null)
            return NotFound(new ControllerResult<ControllerBase>(false, "object not found"));

        return Ok(new ControllerResult<ControllerBase>(true, result));
    }


    [HttpPut("changeDocs")]
    //TODO: Changes may begin just if YOU are the resident to adjust
    public IActionResult ChangeDocument([FromBody] ChangeDocumentResidentCommand comm)
    {
        if (!ModelState.IsValid)
            return BadRequest(new ControllerResult<ControllerBase>(false, "Invalid command"));

        var result = Handler.Handle(comm);

        if (result.IsSuccess == false)
            return StatusCode(500, new HandlerResult(result.IsSuccess, result.Data));
        return Ok(new HandlerResult(true, result.Data));
    }

    [HttpPut("changeEmail")]
    public IActionResult ChangeEmail([FromBody] ChangeEmailResidentCommand comm)
    {
        if (!ModelState.IsValid)
            return BadRequest(new ControllerResult<ControllerBase>(false, "Invalid command"));

        var result = Handler.Handle(comm);

        if (result.IsSuccess == false)
            return StatusCode(500, new HandlerResult(result.IsSuccess, result.Data));
        return Ok(new HandlerResult(true, result.Data));
    }

    [HttpPut("changeName")]
    public IActionResult ChangeName([FromBody] ChangeNameResidentCommand comm)
    {
        if (!ModelState.IsValid)
            return BadRequest(new ControllerResult<ControllerBase>(false, "Invalid command"));

        var result = Handler.Handle(comm);

        if (result.IsSuccess == false)
            return StatusCode(500, new HandlerResult(result.IsSuccess, result.Data));
        return Ok(new HandlerResult(true, result.Data));
    }

    [HttpPut("changePhone")]
    public IActionResult ChangePhone([FromBody] ChangePhoneNumberResidentCommand comm)
    {
        if (!ModelState.IsValid)
            return BadRequest(new ControllerResult<ControllerBase>(false, "Invalid command"));

        var result = Handler.Handle(comm);

        if (result.IsSuccess == false)
            return StatusCode(500, new HandlerResult(result.IsSuccess, result.Data));
        return Ok(new HandlerResult(true, result.Data));
    }
    [HttpPut("changePassword")]
    public IActionResult ChangePassword([FromServices] ServiceEmail Email, [FromBody] ChangePasswordResidentCommand comm)
    {
        if (!ModelState.IsValid)
            return BadRequest(new ControllerResult<ControllerBase>(false, "Invalid command"));

        var newpassword = comm.NewPassword;

        comm.NewPassword = PasswordTool.Encript(comm.NewPassword);

        var result = Handler.Handle(comm);

        if (result.IsSuccess == false)
            return StatusCode(500, new HandlerResult(result.IsSuccess, result.Data));

        try
        {
            Email.SendEmail(result.Data.Name, result.Data.Email, Configurations.EmailMessages.SendPasswordSubject, Configurations.EmailMessages.SendPasswordBody + newpassword);
        }
        catch (Exception)
        {
            return StatusCode(500, new ControllerResult<ControllerBase>(false, "Failed to send via email your new password, but it is already registered"));
        }

        return Ok(new HandlerResult(true, "Your password was successfully changed!!!"));
    }
    [HttpPut("changePassword-generated")]
    public IActionResult ChangePasswordGenerated([FromServices] ServiceEmail Email, [FromBody] GenerateNewPasswordResidentCommand comm)
    {
        if (!ModelState.IsValid)
            return BadRequest(new ControllerResult<ControllerBase>(false, "Invalid command"));

        comm.NewPassword = new Password(true, true, true, false, 16).Next();
        comm.NewPassword = PasswordTool.Encript(comm.NewPassword);

        var result = Handler.Handle(comm);

        if (result.IsSuccess == false)
            return StatusCode(500, new HandlerResult(result.IsSuccess, result.Data));

        try
        {
            Email.SendEmail(result.Data.Name, result.Data.Email, Configurations.EmailMessages.SendPasswordSubject, Configurations.EmailMessages.SendPasswordBody);
        }
        catch (Exception)
        {
            return StatusCode(500, new ControllerResult<ControllerBase>(false, $"Failed to send via email your new password, but it is already registered ({result.Data.NewPassword})"));
        }

        return Ok(new HandlerResult(true, "Your password was successfully changed, verify your email to see it!!"));
    }
}