using API.Services;
using Domain.Commands.Login;
using Domain.Entities;
using Domain.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("login")]
    public class LoginController : ControllerBase
    {
        private readonly IRepository<Resident> ResiRepo;
        private readonly IRepository<Administrator> AdminRepo;
        public LoginController(IRepository<Resident> resiRepo, IRepository<Administrator> adminRepo)
        {
            ResiRepo = resiRepo;
            AdminRepo = adminRepo;
        }
        [HttpPost]
        [AllowAnonymous]
        public IActionResult Authenticate([FromBody] LoginCommand comm)
        {
            if (!ModelState.IsValid)
                return BadRequest(new ControllerResult(false, comm.Notifications));


            if (comm.Role == "Admin")
            {
                var result = AdminRepo.GetById(comm.Id);

                if (result == null)
                    return BadRequest(new ControllerResult(false, "Administrator not found"));
                else
                    return Ok(new ControllerResult(true, ServiceToken.GenerateToken(comm)));
            }
            if (comm.Role == "Resident")
            {
                var result = ResiRepo.GetById(comm.Number, comm.Block, comm.Id);

                if (result == null)
                    return BadRequest(new ControllerResult(false, "Resident not found"));
                else
                    return Ok(new ControllerResult(true, ServiceToken.GenerateToken(comm)));
            }

            else
                return BadRequest(new ControllerResult(false, "Invalid Login entry(ies)"));
        }
    }
}
