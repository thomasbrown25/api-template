using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using personal_trainer_api.Dtos.User;
using personal_trainer_api.Dtos.UserManagement;
using personal_trainer_api.Services.ClientService;
using personal_trainer_api.Services.UserManagementService;
using personal_trainer_api.Services.UserService;

namespace personal_trainer_api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ClientController(IUserService userService, IClientService clientService, IUserManagementService userManagementService) : ControllerBase
    {
        //private readonly IClientService _clientService = clientService;
        private readonly IUserService _userService = userService;
        private readonly IClientService _clientService = clientService;
        private readonly IUserManagementService _userManagementService = userManagementService;

        [Authorize]
        [HttpPost("add-client")]
        public async Task<ActionResult<ServiceResponse<LoadUserDto>>> AddClient(AddUserDto newUser)
        {
            var response = await _userService.Register(newUser);

            if (!response.Success)
            {
                return BadRequest(response);
            }

            response = await _userManagementService.MapUser(new MapUserDto
            {
                Email = response.Data.Email,
                Role = response.Data.Role,
                TrainerId = newUser.TrainerId
            });

            if (!response.Success)
            {
                return BadRequest(response);
            }

            return Ok(response);
        }

        [Authorize]
        [HttpGet("")]
        public async Task<ActionResult<ServiceResponse<List<LoadUserDto>>>> GetClients()
        {
            var response = await _clientService.GetClients();

            if (!response.Success)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }


    }
}