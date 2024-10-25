
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using personal_trainer_api.Dtos.Client;
using personal_trainer_api.Dtos.UserManagement;
using personal_trainer_api.Services.UserManagementService;

namespace personal_trainer_api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserManagementController(IUserManagementService userManagementService) : ControllerBase
    {
        private readonly IUserManagementService _userManagementService = userManagementService;

        [Authorize]
        [HttpPost("map-user")]
        public async Task<ActionResult<ServiceResponse<LoadClientDto>>> MapUser(MapUserDto newClient)
        {
            var response = await _userManagementService.MapUser(newClient);

            if (!response.Success)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }

        [Authorize]
        [HttpPost("update")]
        public async Task<ActionResult<ServiceResponse<LoadClientDto>>> UpdateClient(UpdateClientDto newClient)
        {
            var response = await _userManagementService.UpdateClient(newClient);

            if (!response.Success)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }

        [Authorize]
        [HttpGet("")]
        public async Task<ActionResult<ServiceResponse<List<LoadClientDto>>>> GetAllClients()
        {
            var response = await _userManagementService.GetAllClients();

            if (!response.Success)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }

        [Authorize]
        [HttpGet("{id}")]
        public async Task<ActionResult<ServiceResponse<LoadClientDto>>> GetClientById(int id)
        {
            return Ok(await _userManagementService.GetClientById(id));
        }

        [Authorize]
        [HttpDelete("{id}")]
        public async Task<ActionResult<ServiceResponse<List<LoadClientDto>>>> DeleteClient(int id)
        {
            return Ok(await _userManagementService.DeleteClient(id));
        }
    }
}