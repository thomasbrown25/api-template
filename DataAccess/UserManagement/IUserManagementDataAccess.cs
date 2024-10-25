using personal_trainer_api.Dtos.Client;
using personal_trainer_api.Dtos.User;
using personal_trainer_api.Dtos.UserManagement;

namespace personal_trainer_api.DataAccess.UserManagement
{
    public interface IUserManagementDataAccess
    {
        Task<LoadUserDto> MapUser(LoadUserDto user, LoadUserDto userToBeMapped);
        Task<LoadUserDto> GetAdmin(string email);
        Task<LoadUserDto> GetTrainer(string email);
        Task<LoadUserDto> GetClient(string email);
        Task<LoadClientDto> UpdateClient(UpdateClientDto updatedClient);
        Task<List<LoadClientDto>> GetAllClients(int trainerId);
        Task<LoadClientDto> GetClientById(int id);
        Task<List<LoadClientDto>> DeleteClient(int id);
    }
}