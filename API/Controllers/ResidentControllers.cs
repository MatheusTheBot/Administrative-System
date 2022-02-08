using API.ValidateModels;
using Infra.Repository;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("v1/resident")]
public class ResidentControllers : ControllerBase
{
    [HttpGet("get/{Id}")]
    public IActionResult GetById([FromServices] ResidentRepository repo, [FromRoute]Guid Id)
    {
        if(Id.ToString() == string.Empty)
            return NotFound(new ControllerResult(false, "Invalid Id"));

        var result = repo.GetById(Id);
        if (result == null)
            return NotFound(new ControllerResult(false, "object not found"));

        return Ok(new ControllerResult(true, result));
    }

    [HttpPost("add")]
    public IActionResult AddResident([FromServices] ResidentRepository repo, [FromBody] ResidentModel model)
    {
        if(!ModelState.IsValid)
            return BadRequest(new ControllerResult(false, "Invalid Resident"));

        //create Resident object
        var resident = model.GetResident();

        repo.Create(resident);
        return StatusCode(201, new ControllerResult(true, $"Object created successfuly; ID:{resident.Id}"));
    }

    [HttpPut("update")]
    public IActionResult UpdateResident([FromServices] ResidentRepository repo, [FromBody] ResidentModel model)
    {
        if (!ModelState.IsValid)
            return BadRequest(new ControllerResult(false, "Invalid Resident"));

        var resident = model.GetResident();

        var result = repo.GetById(resident.Id);

        if (result == null)
            return BadRequest(new ControllerResult(false, "object not found"));

        repo.Update(resident);
        return StatusCode(201, new ControllerResult(true, $"Object updated successfuly; ID:{resident.Id}"));
    }

    [HttpDelete("delete/{Id}")]
    public IActionResult DeleteResident([FromServices] ResidentRepository repo, [FromRoute] Guid Id)
    {
        if (Id.ToString() == string.Empty)
            return NotFound(new ControllerResult(false, "Invalid Id"));

        var resident = repo.GetById(Id);

        if (resident == null)
            return BadRequest(new ControllerResult(false, "object not found"));

        repo.Delete(resident);
        return StatusCode(201, new ControllerResult(true, $"Object deleted successfuly; ID:{resident.Id}"));
    }
}