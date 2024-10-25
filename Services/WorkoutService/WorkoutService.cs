using personal_trainer_api.DataAccess.Workouts;
using personal_trainer_api.Dtos.Workout;

namespace personal_trainer_api.Services.WorkoutService
{
    public class WorkoutService(IWorkoutDataAccess workoutDataAccess) : IWorkoutService
    {
        private readonly IWorkoutDataAccess _workoutDataAccess = workoutDataAccess;

        public async Task<ServiceResponse<LoadWorkoutDto>> AddWorkout(AddWorkoutDto newWorkout)
        {
            ServiceResponse<LoadWorkoutDto> response = new();

            try
            {
                response.Data = await _workoutDataAccess.AddWorkout(newWorkout);
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message;
                return response;
            }
            return response;
        }

        public async Task<ServiceResponse<LoadWorkoutDto>> GetWorkout(int id)
        {
            ServiceResponse<LoadWorkoutDto> response = new();

            try
            {
                response.Data = await _workoutDataAccess.GetWorkout(id);
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message;
                return response;
            }
            return response;
        }

        public async Task<ServiceResponse<List<LoadWorkoutDto>>> GetWorkouts()
        {
            ServiceResponse<List<LoadWorkoutDto>> response = new()
            {
                Data = []
            };

            try
            {
                response.Data = await _workoutDataAccess.GetWorkouts();
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message;
                return response;
            }
            return response;
        }
    }
}