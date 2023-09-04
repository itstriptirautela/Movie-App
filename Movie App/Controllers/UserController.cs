using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Movie_App.DTO;
using Movie_App.Model;
using Movie_App.Service;
using System.ComponentModel.DataAnnotations;
using System.Runtime.InteropServices;

namespace Movie.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {

        private readonly ILogger<UserController> _logger;
        private readonly IUserService _userService;

        public UserController(ILogger<UserController> logger, IUserService userService)
        {
            _logger = logger;
            _userService = userService;
        }

        // GET: AllUsers
        [HttpGet("AllUser")]
        public ActionResult<List<User>> GetAllUsers()
        {
            try
            {
                _logger.LogInformation("Getting All users");
                var userList = _userService.GetAllUsers();
                if (userList.Count < 1)
                {
                    _logger.LogWarning("No users in db");
                    return BadRequest("No user found");
                }
                _logger.LogInformation("Users found");
                return Ok(userList);
            }
            catch (Exception e)
            {
                _logger.LogTrace(e.StackTrace);
                return StatusCode(500, new { Message = "Internal Server Error." });
            }
        }
        [HttpPost]
        public async Task<ActionResult> Login([FromBody]LoginDto loginDto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest("Details not correct. Please try again.");
                }
                var user = await _userService.Login(loginDto);
                if (user != null)
                {
                    return Ok(user);
                }
                return Unauthorized("Invalid Credntials");
            }
            catch (Exception e)
            {
                _logger.LogTrace(e.StackTrace);

                return StatusCode(500, new { Message = "Internal Server Error." });
            }

        }
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] User registerDto)
        {
            if (!ModelState.IsValid)
            {
                _logger.LogInformation("Modelstate not valid");
                return BadRequest("Details not correct. Please try again.");
            }
            try
            {
                return Ok(await _userService.Register(registerDto));
            }
            catch (Exception e)
            {
                _logger.LogTrace(e.StackTrace);
                return StatusCode(500, new { Message = "Internal Server Error." });
            }
        }
        [HttpPut("forgot")]
        public async Task<ActionResult> ForgotPassword(string loginId, ForgotPasswordDto forgotPasswordDto)
        {
            try
            {
                var response = (await _userService.ForgotPassword(loginId, forgotPasswordDto));
                if (response == null)
                {
                    return BadRequest("User not found");
                }
                return Ok(new {  response });
            }
            catch (Exception e)
            {
                return StatusCode(500, new { Message = "Internal Server Error." });
            }
        }


    }
}
