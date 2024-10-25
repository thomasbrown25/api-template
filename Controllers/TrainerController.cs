using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using personal_trainer_api.Dtos.User;
using personal_trainer_api.Dtos.UserManagement;
using personal_trainer_api.Services.TrainerService;
using personal_trainer_api.Services.UserManagementService;
using personal_trainer_api.Services.UserService;

namespace personal_trainer_api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TrainerController(ITrainerService trainerService, IUserService userService, IUserManagementService userManagementService) : ControllerBase
    {
        private readonly ITrainerService _trainerService = trainerService;
        private readonly IUserService _userService = userService;
        private readonly IUserManagementService _userManagementService = userManagementService;

        [Authorize]
        [HttpPost("add-trainer")]
        public async Task<ActionResult<ServiceResponse<LoadUserDto>>> AddTrainer(AddUserDto newUser)
        {
            var response = await _userService.Register(newUser);

            if (!response.Success)
            {
                return BadRequest(response);
            }

            response = await _userManagementService.MapUser(new MapUserDto
            {
                Email = response.Data.Email,
                Role = response.Data.Role
            });

            if (!response.Success)
            {
                return BadRequest(response);
            }

            return Ok(response);
        }

        [Authorize]
        [HttpGet("{trainerId}/clients")]
        public async Task<ActionResult<ServiceResponse<List<LoadUserDto>>>> GetClients(int trainerId)
        {
            var response = await _trainerService.GetClients(trainerId);

            if (!response.Success)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }

        [Authorize]
        [HttpGet("{trainerId}/clients/last-visited")]
        public async Task<ActionResult<ServiceResponse<List<LoadUserDto>>>> GetLastVisitedClients(int trainerId)
        {
            var response = await _trainerService.GetLastVisitedClients(trainerId);

            if (!response.Success)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }

        [Authorize]
        [HttpGet("")]
        public async Task<ActionResult<ServiceResponse<List<LoadUserDto>>>> GetTrainers()
        {
            var response = await _trainerService.GetTrainers();

            if (!response.Success)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }
    }
}