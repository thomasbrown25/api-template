using personal_trainer_api.Dtos.Workout;

namespace personal_trainer_api.Services.WorkoutService
{
    public interface IWorkoutService
    {
        Task<ServiceResponse<LoadWorkoutDto>> AddWorkout(AddWorkoutDto newWorkout);
        Task<ServiceResponse<LoadWorkoutDto>> GetWorkout(int id);
        Task<ServiceResponse<List<LoadWorkoutDto>>> GetWorkouts();
    }
}