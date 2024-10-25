using personal_trainer_api.Dtos.Workout;

namespace personal_trainer_api.DataAccess.Workouts
{
    public interface IWorkoutDataAccess
    {
        Task<LoadWorkoutDto> AddWorkout(AddWorkoutDto newWorkout);
        Task<LoadWorkoutDto> GetWorkout(int id);
        Task<List<LoadWorkoutDto>> GetWorkouts();
    }
}