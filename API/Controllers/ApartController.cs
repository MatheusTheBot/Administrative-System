using Domain.Commands.Apart;
using Domain.Handlers;
using Infra.Repository;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
public class ApartController : ControllerBase
{

    //Queries
    [HttpGet("get/{Apart},{Block}")]
    public IActionResult GetById([FromServices] ApartRepository repo, [FromRoute] int Apart, int Block)
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
    public IActionResult AddApart([FromServices] ApartRepository repo, [FromServices] ApartHandler handler, [FromBody] CreateApartCommand comm)
    {
        if (!ModelState.IsValid)
            return BadRequest(new ControllerResult(false, "Invalid Apart"));
        
        var result = handler.Handle(comm);
        
        if(result.IsSuccess == false)
            return BadRequest(new ControllerResult(false, result.Data));

        return StatusCode(201, new ControllerResult(true, $"Apart created successfuly; Apart: {comm.Number}, Block: {comm.Block}"));
    }

    [HttpPut("update")]
    public IActionResult UpdateApart([FromServices] ApartRepository repo, [FromBody] ApartModel model)
    {
        if (!ModelState.IsValid)
            return BadRequest(new ControllerResult(false, "Invalid Apart"));

        var apart = model.GetApart();

        var result = repo.GetById(apart.Id);

        if (result == null)
            return BadRequest(new ControllerResult(false, "object not found"));

        repo.Update(apart);
        return StatusCode(201, new ControllerResult(true, $"Object updated successfuly; ID:{apart.Id}"));
    }

    [HttpDelete("delete/{Id}")]
    public IActionResult DeleteApart([FromServices] ApartRepository repo, [FromRoute] Guid Id)
    {
        if (Id.ToString() == string.Empty)
            return NotFound(new ControllerResult(false, "Invalid Id"));

        var apart = repo.GetById(Id);

        if (apart == null)
            return BadRequest(new ControllerResult(false, "object not found"));

        repo.Delete(apart);
        return StatusCode(201, new ControllerResult(true, $"Object deleted successfuly; ID:{apart.Id}"));
    }
}