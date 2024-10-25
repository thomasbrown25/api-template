using personal_trainer_api.Dtos.User;

namespace personal_trainer_api.DataAccess.Trainers
{
    public interface ITrainerDataAccess
    {
        Task<List<LoadUserDto>> GetClients(int trainerId);
        Task<List<LoadUserDto>> GetLastVisitedClients(int trainerId);
        Task<List<LoadUserDto>> GetTrainers();
    }
}