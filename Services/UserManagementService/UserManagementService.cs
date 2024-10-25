using personal_trainer_api.DataAccess.Clients;
using personal_trainer_api.DataAccess.UserManagement;
using personal_trainer_api.DataAccess.Users;
using personal_trainer_api.Dtos.Client;
using personal_trainer_api.Dtos.User;
using personal_trainer_api.Dtos.UserManagement;
using personal_trainer_api.Enums;
using personal_trainer_api.Services.LoggingService;

namespace personal_trainer_api.Services.UserManagementService;

public class UserManagementService(
    IUserManagementDataAccess userManagementDataAccess,
    IUserDataAccess userDataAccess,
    ILoggingService loggingService) : IUserManagementService
{
    private readonly IUserManagementDataAccess _userManagementDataAccess = userManagementDataAccess;
    private readonly IUserDataAccess _userDataAccess = userDataAccess;
    private readonly ILoggingService _loggingService = loggingService;

    public async Task<ServiceResponse<LoadUserDto>> MapUser(MapUserDto userToBeMapped)
    {
        ServiceResponse<LoadUserDto> response = new();

        try
        {
            var user = await _userDataAccess.GetUser(userToBeMapped.Email);
            var mappedUser = new LoadUserDto();

            switch (user.Role)
            {
                case EUserRole.Admin:
                    var adminUser = await _userManagementDataAccess.GetAdmin(user.Email);
                    mappedUser = await _userManagementDataAccess.MapUser(user, adminUser);
                    break;

                case EUserRole.Trainer:
                    var trainerUser = await _userManagementDataAccess.GetTrainer(user.Email);
                    mappedUser = await _userManagementDataAccess.MapUser(user, trainerUser);
                    break;

                case EUserRole.Client:
                    var clientUser = await _userManagementDataAccess.GetClient(user.Email);
                    clientUser.TrainerId = userToBeMapped.TrainerId;
                    mappedUser = await _userManagementDataAccess.MapUser(user, clientUser);
                    break;
            }

            response.Data = mappedUser;
        }
        catch (Exception ex)
        {
            response.Success = false;
            response.Message = ex.Message;
            return response;
        }
        return response;
    }

    public async Task<ServiceResponse<LoadClientDto>> UpdateClient(UpdateClientDto newClient)
    {
        ServiceResponse<LoadClientDto> response = new();

        try
        {
            response.Data = await _userManagementDataAccess.UpdateClient(newClient);
        }
        catch (Exception ex)
        {
            response.Success = false;
            response.Message = ex.Message;
            return response;
        }
        return response;
    }

    public async Task<ServiceResponse<List<LoadClientDto>>> GetAllClients()
    {
        ServiceResponse<List<LoadClientDto>> response = new();

        try
        {
            var user = await _userDataAccess.GetCurrentUser();

            response.Data = await _userManagementDataAccess.GetAllClients(user.Id);
        }
        catch (Exception ex)
        {
            response.Success = false;
            response.Message = ex.Message;
            return response;
        }
        return response;
    }

    public async Task<ServiceResponse<LoadClientDto>> GetClientById(int id)
    {
        throw new NotImplementedException();
    }

    public async Task<ServiceResponse<LoadClientDto>> UpdateClient(int id, UpdateClientDto updatedClient)
    {
        throw new NotImplementedException();
    }

    public async Task<ServiceResponse<List<LoadClientDto>>> DeleteClient(int id)
    {
        throw new NotImplementedException();
    }
}