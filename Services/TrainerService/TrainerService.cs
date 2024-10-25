using personal_trainer_api.DataAccess.Trainers;
using personal_trainer_api.Dtos.User;
using personal_trainer_api.Services.LoggingService;

namespace personal_trainer_api.Services.TrainerService
{
    public class TrainerService(ITrainerDataAccess trainerDataAccess) : ITrainerService
    {
        private readonly ITrainerDataAccess _trainerDataAccess = trainerDataAccess;

        public async Task<ServiceResponse<List<LoadUserDto>>> GetClients(int trainerId)
        {
            ServiceResponse<List<LoadUserDto>> response = new()
            {
                Data = []
            };

            try
            {
                response.Data = await _trainerDataAccess.GetClients(trainerId);
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message;
                return response;
            }
            return response;
        }

        public async Task<ServiceResponse<List<LoadUserDto>>> GetLastVisitedClients(int trainerId)
        {
            ServiceResponse<List<LoadUserDto>> response = new()
            {
                Data = []
            };

            try
            {
                response.Data = await _trainerDataAccess.GetLastVisitedClients(trainerId);
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message;
                return response;
            }
            return response;
        }

        public async Task<ServiceResponse<List<LoadUserDto>>> GetTrainers()
        {
            ServiceResponse<List<LoadUserDto>> response = new()
            {
                Data = []
            };

            try
            {
                response.Data = await _trainerDataAccess.GetTrainers();
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