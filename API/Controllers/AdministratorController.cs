using API.Services;
using API.Tools;
using Domain.Commands.Administrator;
using Domain.Entities;
using Domain.Handlers;
using Domain.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PasswordGenerator;

namespace API.Controllers;

[ApiController]
[Route("v1/admin")]
[Authorize("Admin")]
public class AdministratorControllers : ControllerBase
{
    private readonly IRepository<Administrator> Repo;
    private readonly AdministratorHandler Handler;

    public AdministratorControllers(IRepository<Administrator> repo, AdministratorHandler handler)
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


    [HttpPut("add")]
    public IActionResult addAdmin(CreateAdministratorCommand comm)
    {
        if (!ModelState.IsValid)
            return BadRequest(new ControllerResult<ControllerBase>(false, "Invalid command"));

        comm.Password = PasswordTool.Encript(comm.Password);

        if (comm.Type == Domain.Enums.EDocumentType.CPF)
        {
            if (DocumentValidatorTool.CPF(comm.DocumentNumber) == false)
                return BadRequest(new ControllerResult<ControllerBase>(false, "Invalid document number"));
        }

        if (comm.Type == Domain.Enums.EDocumentType.CNPJ)
        {
            if (DocumentValidatorTool.CNPJ(comm.DocumentNumber) == false)
                return BadRequest(new ControllerResult<ControllerBase>(false, "Invalid document number"));
        }

        var result = Handler.Handle(comm);

        if (result.IsSuccess == false)
            return StatusCode(500, new HandlerResult(result.IsSuccess, result.Data));
        return Ok(new HandlerResult(true, result.Data));
    }

    [HttpPut("changeDocs")]
    public IActionResult ChangeDocument([FromBody] ChangeDocumentAdministratorCommand comm)
    {
        if (!ModelState.IsValid)
            return BadRequest(new ControllerResult<ControllerBase>(false, "Invalid command"));

        if (comm.Type == Domain.Enums.EDocumentType.CPF)
        {
            if (DocumentValidatorTool.CPF(comm.DocumentNumber) == false)
                return BadRequest(new ControllerResult<ControllerBase>(false, "Invalid document number"));
        }

        if (comm.Type == Domain.Enums.EDocumentType.CNPJ)
        {
            if (DocumentValidatorTool.CNPJ(comm.DocumentNumber) == false)
                return BadRequest(new ControllerResult<ControllerBase>(false, "Invalid document number"));
        }

        var result = Handler.Handle(comm);

        if (result.IsSuccess == false)
            return StatusCode(500, new HandlerResult(result.IsSuccess, result.Data));
        return Ok(new HandlerResult(true, result.Data));
    }

    [HttpPut("changeEmail")]
    public IActionResult ChangeEmail([FromBody] ChangeEmailAdministratorCommand comm)
    {
        if (!ModelState.IsValid)
            return BadRequest(new ControllerResult<ControllerBase>(false, "Invalid command"));

        var result = Handler.Handle(comm);

        if (result.IsSuccess == false)
            return StatusCode(500, new HandlerResult(result.IsSuccess, result.Data));
        return Ok(new HandlerResult(true, result.Data));
    }

    [HttpPut("changeName")]
    public IActionResult ChangeName([FromBody] ChangeNameAdministratorCommand comm)
    {
        if (!ModelState.IsValid)
            return BadRequest(new ControllerResult<ControllerBase>(false, "Invalid command"));

        var result = Handler.Handle(comm);

        if (result.IsSuccess == false)
            return StatusCode(500, new HandlerResult(result.IsSuccess, result.Data));
        return Ok(new HandlerResult(true, result.Data));
    }

    [HttpPut("changePhone")]
    public IActionResult ChangePhone([FromBody] ChangePhoneNumberAdministratorCommand comm)
    {
        if (!ModelState.IsValid)
            return BadRequest(new ControllerResult<ControllerBase>(false, "Invalid command"));

        var result = Handler.Handle(comm);

        if (result.IsSuccess == false)
            return StatusCode(500, new HandlerResult(result.IsSuccess, result.Data));
        return Ok(new HandlerResult(true, result.Data));
    }

    [HttpPut("changePassword")]
    public IActionResult ChangePassword([FromServices] ServiceEmail Email, [FromBody] ChangePasswordAdministratorCommand comm)
    {
        if (!ModelState.IsValid)
            return BadRequest(new ControllerResult<ControllerBase>(false, "Invalid command"));

        var myNewPassword = comm.NewPassword;
        comm.NewPassword = PasswordTool.Encript(comm.NewPassword);

        var administrator = Repo.GetById(comm.Id);
        if (administrator == null)
            return NotFound(new ControllerResult<ControllerBase>(false, "object not found"));

        bool verify = PasswordTool.Verify(administrator.Password, comm.Password);
        if (verify == false)
            return BadRequest(new ControllerResult<ControllerBase>(false, "Wrong password"));

        var result = Handler.Handle(comm);

        if (result.IsSuccess == false)
            return StatusCode(500, new HandlerResult(result.IsSuccess, result.Data));

        try
        {
            Email.SendEmail(result.Data.Name, result.Data.Email, Configurations.EmailMessages.SendPasswordSubject, Configurations.EmailMessages.SendPasswordBody + myNewPassword);
        }
        catch (Exception)
        {
            return StatusCode(500, new ControllerResult<ControllerBase>(false, "Failed to send via email your new password, but it is already registered"));
        }

        return Ok(new HandlerResult(true, "Your password was successfully changed!!!"));
    }
    [HttpPut("changePassword-generated")]
    public IActionResult ChangePasswordGenerated([FromServices] ServiceEmail Email, [FromBody] GenerateNewPasswordAdminCommand comm)
    {
        if (!ModelState.IsValid)
            return BadRequest(new ControllerResult<ControllerBase>(false, "Invalid command"));

        comm.NewPassword = new Password(true, true, true, false, 16).Next();
        comm.NewPassword = PasswordTool.Encript(comm.NewPassword);

        var administrator = Repo.GetById(comm.Id);
        if (administrator == null)
            return NotFound(new ControllerResult<ControllerBase>(false, "object not found"));

        bool verify = PasswordTool.Verify(administrator.Password, comm.Password);
        if (verify == false)
            return BadRequest(new ControllerResult<ControllerBase>(false, "Wrong password"));

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