using personal_trainer_api.Dtos.User;

namespace personal_trainer_api.Services.ClientService
{
    public interface IClientService
    {
        Task<ServiceResponse<List<LoadUserDto>>> GetClients();
    }
}