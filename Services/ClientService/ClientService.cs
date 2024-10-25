using personal_trainer_api.DataAccess.Clients;
using personal_trainer_api.Dtos.User;

namespace personal_trainer_api.Services.ClientService
{
    public class ClientService(IClientDataAccess clientDataAccess) : IClientService
    {
        private readonly IClientDataAccess _clientDataAccess = clientDataAccess;

        public async Task<ServiceResponse<List<LoadUserDto>>> GetClients()
        {
            ServiceResponse<List<LoadUserDto>> response = new()
            {
                Data = []
            };

            try
            {
                response.Data = await _clientDataAccess.GetClients();
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