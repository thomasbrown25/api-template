using personal_trainer_api.Dtos.User;

namespace personal_trainer_api.Services.TrainerService
{
    public interface ITrainerService
    {
        Task<ServiceResponse<List<LoadUserDto>>> GetClients(int trainerId);
        Task<ServiceResponse<List<LoadUserDto>>> GetLastVisitedClients(int trainerId);
        Task<ServiceResponse<List<LoadUserDto>>> GetTrainers();
    }
}