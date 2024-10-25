
using personal_trainer_api.Dtos.Client;
using personal_trainer_api.Dtos.User;
using personal_trainer_api.Dtos.UserManagement;

namespace personal_trainer_api.Services.UserManagementService;

public interface IUserManagementService
{
    Task<ServiceResponse<LoadUserDto>> MapUser(MapUserDto newMappedUser);
    Task<ServiceResponse<LoadClientDto>> UpdateClient(UpdateClientDto newClient);
    Task<ServiceResponse<List<LoadClientDto>>> GetAllClients();
    Task<ServiceResponse<LoadClientDto>> GetClientById(int id);
    Task<ServiceResponse<List<LoadClientDto>>> DeleteClient(int id);
}