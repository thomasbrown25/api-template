using personal_trainer_api.Dtos.Client;
using personal_trainer_api.Dtos.User;

namespace personal_trainer_api.DataAccess.Clients
{
    public interface IClientDataAccess
    {
        Task<LoadUserDto> UpdateClient(UpdateClientDto updatedClient);
        Task<List<LoadUserDto>> GetClients();
        Task<LoadUserDto> GetClientById(int id);
        Task<List<LoadUserDto>> DeleteClient(int id);
    }
}