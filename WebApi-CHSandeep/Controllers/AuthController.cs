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

        //[HttpPost("login")]
        //public IActionResult Login(LoginDto loginDTO)
        //{
        //    var token = _authBussiness.Authenticate(loginDTO);

        //    if (token == null)
        //        return Unauthorized();

        //    return Ok(new { Token = token });
        //}

        [HttpPost("login-with-refresh")]
        public IActionResult LoginWithRefresh(LoginDto loginDTO)
        {
            var response = _authBussiness.AuthenticateWithRefresh(loginDTO);

            if (response == null)
                return Unauthorized();

            return Ok(response);
        }

        [HttpPost("refresh")]
        public IActionResult Refresh([FromQuery] string refreshToken)
        {
            var response = _authBussiness.RefreshToken(refreshToken);

            if (response == null)
                return Unauthorized();

            return Ok(response);
        }
    }
}
