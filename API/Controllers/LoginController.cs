using Domain.Commands.Login;
using Domain.Handlers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("login")]
    public class LoginController : ControllerBase
    {
        private readonly LoginHandler Handler;
        public LoginController(LoginHandler handler)
        {
            Handler = handler;
        }

        [HttpPost]
        [AllowAnonymous]
        public IActionResult Authenticate([FromBody] LoginCommand comm)
        {
            if (!ModelState.IsValid)
                return BadRequest(new ControllerResult(false, comm.Notifications));

            var login = Handler.Handle(comm);

            if (login == null)
                return NotFound(new ControllerResult(false, "Object not found"));

            if (login.IsSuccess)
                return Ok(new ControllerResult(true, login.Data));
            else
                return BadRequest(new ControllerResult(false, login.Data));
        }
    }
}
