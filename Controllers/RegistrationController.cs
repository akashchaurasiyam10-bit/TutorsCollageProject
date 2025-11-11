using Infra.Library;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service.Library;

namespace TutorsCollageProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegistrationController : ControllerBase
    {
        private readonly RegistrationService _registrationService;
        public RegistrationController(RegistrationService registrationService)
        {
            _registrationService = registrationService;
        }
        [HttpPost("registerBoth")]
        public async Task<IActionResult> Register([FromBody] Registration registration)
        {
            try
            {
                var result = await _registrationService.RegisterAsync(registration);
                return result ? Ok("Registration successful") : BadRequest("Registration failed");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        //[HttpGet("types")]
        //public async Task<IActionResult> GetUserTypes()
        //{
        //    var types = await _registrationService.GetUserTypesAsync();
        //    return Ok(types);
        //}

        [HttpGet("GetUsersByType")]
        public async Task<IActionResult> GetUsers([FromQuery] string type)
        {
            var users = await _registrationService.GetUsersByType(type);
            return Ok(users);
        }

        [HttpPost("LoginByType")]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            var user = await _registrationService.ValidateLogin(request.Email, request.Password, request.Type);
            if (user != null)
                return Ok($"Login successful as {user.Type}");
            else
                return Unauthorized("Invalid credentials or type");
        }
    }

    public class LoginRequest
    {
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string Type { get; set; } = null!;
    }

}

