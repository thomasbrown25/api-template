using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using personal_trainer_api.Dtos.Workout;
using personal_trainer_api.Services.WorkoutService;

namespace personal_trainer_api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class WorkoutController(IWorkoutService workoutService) : ControllerBase
    {
        private readonly IWorkoutService _workoutService = workoutService;

        [Authorize]
        [HttpPost("add-workout")]
        public async Task<ActionResult<ServiceResponse<LoadWorkoutDto>>> AddWorkout(AddWorkoutDto newWorkout)
        {
            var response = await _workoutService.AddWorkout(newWorkout);

            if (!response.Success)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }

        [Authorize]
        [HttpGet("{id}")]
        public async Task<ActionResult<ServiceResponse<LoadWorkoutDto>>> GetWorkout(int id)
        {
            var response = await _workoutService.GetWorkout(id);

            if (!response.Success)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }

        [Authorize]
        [HttpGet("")]
        public async Task<ActionResult<ServiceResponse<List<LoadWorkoutDto>>>> GetWorkouts()
        {
            var response = await _workoutService.GetWorkouts();

            if (!response.Success)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }
    }
}