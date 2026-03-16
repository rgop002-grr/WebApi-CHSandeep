using BussinessLayer.IBusiness;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RepositoryLayer.ModelDto;

namespace WebApi_CHSandeep.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthBussiness _authBussiness;

        public AuthController(IAuthBussiness authService)
        {
            _authBussiness = authService;
        }

        [HttpPost("login")]
        public IActionResult Login(LoginDto loginDTO)
        {
            var token = _authBussiness.Authenticate(loginDTO);

            if (token == null)
                return Unauthorized();

            return Ok(new { Token = token });
        }
    }
}
