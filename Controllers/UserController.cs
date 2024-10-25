
using personal_trainer_api.Dtos.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using personal_trainer_api.Dtos.UserSetting;
using personal_trainer_api.Services.UserService;

namespace personal_trainer_api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController(
        IUserService userService
        ) : ControllerBase
    {
        private readonly IUserService _userService = userService;

        [HttpPost("register")]
        public async Task<ActionResult<ServiceResponse<LoadUserDto>>> Register(AddUserDto request)
        {
            var response = await _userService.Register(request);

            if (!response.Success)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }

        [HttpPost("login")]
        public async Task<ActionResult<ServiceResponse<LoadUserDto>>> Login(UserLoginDto request)
        {
            var response = await _userService.Login(request.Email, request.Password);

            if (!response.Success)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }

        // Load current user
        [Authorize]
        [HttpGet("load-user")]
        public async Task<ActionResult<ServiceResponse<LoadUserDto>>> LoadUser()
        {
            var response = await _userService.LoadUser();

            if (!response.Success)
            {
                return Unauthorized(response);
            }
            return Ok(response);
        }

        [Authorize]
        [HttpDelete("{userId}")]
        public async Task<ActionResult<ServiceResponse<string>>> DeleteUser(int userId)
        {
            var response = await _userService.DeleteUser(userId);

            if (!response.Success)
            {
                return Unauthorized(response);
            }
            return Ok(response);
        }

        [HttpGet("settings")]
        public async Task<ActionResult<ServiceResponse<SettingsDto>>> GetSettings()
        {
            var response = await _userService.GetSettings();

            if (!response.Success)
            { // need to set this to server error
                return BadRequest(response);
            }
            return Ok(response);
        }

        [Authorize]
        [HttpPost("settings")]
        public async Task<ActionResult<ServiceResponse<SettingsDto>>> SaveSettings(SettingsDto newSettings)
        {
            var response = await _userService.SaveSettings(newSettings);

            if (!response.Success)
            { // need to set this to server error
                return BadRequest(response);
            }
            return Ok(response);
        }
    }
}
