using API.ValidateModels;
using Infra.Repository;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("v1/visitant")]

public class VisitantController : ControllerBase
{
    [HttpGet("get/{Id}")]
    public IActionResult GetById([FromServices] VisitantRepository repo, [FromRoute] Guid Id)
    {
        if (Id.ToString() == string.Empty)
            return NotFound(new ControllerResult(false, "Invalid Id"));

        var result = repo.GetById(Id);
        if (result == null)
            return NotFound(new ControllerResult(false, "object not found"));

        return Ok(new ControllerResult(true, result));
    }

    [HttpPost("add")]
    public IActionResult AddVisitant([FromServices] VisitantRepository repo, [FromBody] VisitantModel model)
    {
        if (!ModelState.IsValid)
            return BadRequest(new ControllerResult(false, "Invalid Visitant"));

        //create Visitant object
        var visitant = model.GetVisitant();

        repo.Create(visitant);
        return StatusCode(201, new ControllerResult(true, $"Object created successfuly; ID:{visitant.Id}"));
    }

    [HttpPut("update")]
    public IActionResult UpdateVisitant([FromServices] VisitantRepository repo, [FromBody] VisitantModel model)
    {
        if (!ModelState.IsValid)
            return BadRequest(new ControllerResult(false, "Invalid Visitant"));

        var visitant = model.GetVisitant();

        var result = repo.GetById(visitant.Id);

        if (result == null)
            return BadRequest(new ControllerResult(false, "object not found"));

        repo.Update(visitant);
        return StatusCode(201, new ControllerResult(true, $"Object updated successfuly; ID:{visitant.Id}"));
    }

    [HttpDelete("delete/{Id}")]
    public IActionResult DeleteVisitant([FromServices] VisitantRepository repo, [FromRoute] Guid Id)
    {
        if (Id.ToString() == string.Empty)
            return NotFound(new ControllerResult(false, "Invalid Id"));

        var visitant = repo.GetById(Id);

        if (visitant == null)
            return BadRequest(new ControllerResult(false, "object not found"));

        repo.Delete(visitant);
        return StatusCode(201, new ControllerResult(true, $"Object deleted successfuly; ID:{visitant.Id}"));
    }
}