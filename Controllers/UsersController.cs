using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization; //[AllowAnonymous]
using WebApi.Auth;


namespace WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UsersController : ControllerBase
    {
        private IUsersService _userService;

        public UsersController(IUsersService userService)
        {
            _userService = userService;
        }

        [Authorize] //<-- Atributo en Herramientas
        [HttpGet]
        public IActionResult GetAll()
        {
            var users = _userService.GetAll();
            return Ok(users);
        }
    }
}
