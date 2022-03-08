using API.Tools;
using Domain.Commands.Login;
using Domain.Entities;
using Domain.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [AllowAnonymous]
    public class LoginController : ControllerBase
    {
        private readonly IRepository<Administrator> Repository;
        private readonly IRepository<Resident> ResidentRepository;
        public LoginController(IRepository<Administrator> repository, IRepository<Resident> residentRepository)
        {
            Repository = repository;
            ResidentRepository = residentRepository;
        }

        [HttpPost]
        [Route("login")]
        public IActionResult Authenticate([FromBody] LoginCommand comm)
        {
            if (!ModelState.IsValid)
                return BadRequest(new ControllerResult<ControllerBase>(false, comm.Notifications));

            if (comm.Role == "Admin")
            {
                Administrator? search;
                try
                {
                    search = Repository.GetById(comm.Id);
                }
                catch (Exception ex)
                {
                    return StatusCode(500, new ControllerResult<ControllerBase>(false, "Unable to access database, unable to perform requested operation: " + ex));
                }
                if (search == null)
                    return BadRequest(new ControllerResult<ControllerBase>(false, "Administrator not found"));

                var IsTrue = PasswordTool.Verify(search.Password, comm.Password);
                if (IsTrue)
                    return Ok(new ControllerResult<ControllerBase>(true, TokenTool.GenerateToken(comm)));
                return BadRequest(new ControllerResult<ControllerBase>(false, "Access Denied"));
            }

            if (comm.Role == "Resident")
            {
                Resident? search;
                try
                {
                    search = ResidentRepository.GetById(comm.Number, comm.Block, comm.Id);
                }
                catch (Exception)
                {
                    return StatusCode(500, new ControllerResult<ControllerBase>(false, "Unable to access database, unable to perform requested operation"));
                }
                if (search == null)
                    return BadRequest(new ControllerResult<ControllerBase>(false, "Resident not found"));

                var IsTrue = PasswordTool.Verify(search.Password, comm.Password);
                if (IsTrue)
                    return Ok(new ControllerResult<ControllerBase>(true, TokenTool.GenerateToken(comm)));
                return BadRequest(new ControllerResult<ControllerBase>(false, "Access Denied"));
            }
            return BadRequest(new ControllerResult<ControllerBase>(false, "Invalid Role"));
        }
    }
}
