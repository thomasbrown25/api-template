using AutoMapper;
using personal_trainer_api.Data;
using personal_trainer_api.Dtos.Client;
using personal_trainer_api.Services.LoggingService;
using Microsoft.EntityFrameworkCore;
using personal_trainer_api.Dtos.User;
using personal_trainer_api.Dtos.UserManagement;
using personal_trainer_api.Enums;

namespace personal_trainer_api.DataAccess.UserManagement
{
    public class UserManagementDataAccess(DataContext context, IMapper mapper, ILoggingService logging) : IUserManagementDataAccess
    {
        private readonly DataContext _context = context;
        private readonly ILoggingService _loggingService = logging;
        private readonly IMapper _mapper = mapper;

        public async Task<LoadUserDto> GetAdmin(string email)
        {
            return _mapper.Map<LoadUserDto>(_context.Admins
                            .Where(x => x.Email == email)
                            .FirstOrDefaultAsync().Result);
        }

        public async Task<LoadUserDto> GetTrainer(string email)
        {
            return _mapper.Map<LoadUserDto>(_context.Trainers
                            .Where(x => x.Email == email)
                            .FirstOrDefaultAsync().Result);
        }

        public async Task<LoadUserDto> GetClient(string email)
        {
            return _mapper.Map<LoadUserDto>(_context.Clients
                            .Where(x => x.Email == email)
                            .FirstOrDefaultAsync().Result);
        }

        public async Task<LoadUserDto> MapUser(LoadUserDto user, LoadUserDto userToBeMapped)
        {
            try
            {

                switch (user.Role)
                {

                    case EUserRole.Admin:
                        var adminUser = _context.Admins
                            .Where(x => x.Email == userToBeMapped.Email)
                            .FirstOrDefaultAsync().Result;

                        adminUser.UserId = user.Id;
                        userToBeMapped = _mapper.Map<LoadUserDto>(adminUser);
                        break;

                    case EUserRole.Trainer:
                        var trainerUser = _context.Trainers
                            .Where(x => x.Email == userToBeMapped.Email)
                            .FirstOrDefaultAsync().Result;

                        trainerUser.UserId = user.Id;
                        userToBeMapped = _mapper.Map<LoadUserDto>(trainerUser);
                        break;

                    case EUserRole.Client:
                        var clientUser = _context.Clients
                            .Where(x => x.Email == userToBeMapped.Email)
                            .FirstOrDefaultAsync().Result;

                        clientUser.UserId = user.Id;
                        clientUser.TrainerId = userToBeMapped.TrainerId;
                        userToBeMapped = _mapper.Map<LoadUserDto>(clientUser);
                        break;

                }

                await _context.SaveChangesAsync();

                return _mapper.Map<LoadUserDto>(userToBeMapped);
            }
            catch (Exception ex)
            {
                _loggingService.LogException(ex);
                return null;
            }
        }

        public async Task<LoadClientDto> UpdateClient(UpdateClientDto newClient)
        {
            try
            {
                var dbClient = _context.Clients
                    .Where(x => x.Email == newClient.Email)
                    .FirstOrDefault();

                if (dbClient is not null)
                {
                    dbClient.TrainerId = newClient.TrainerId;
                }
                else
                {
                    return null;
                }

                await _context.SaveChangesAsync();

                return _mapper.Map<LoadClientDto>(newClient);
            }
            catch (Exception ex)
            {
                _loggingService.LogException(ex);
                return null;
            }
        }

        public async Task<List<LoadClientDto>> GetAllClients(int trainerId)
        {
            try
            {
                var dbClients = await _context.Clients
                        .Where(x => x.TrainerId == trainerId)
                        .OrderBy(x => x.FirstName)
                        .ToListAsync();

                return dbClients.Select(x => _mapper.Map<LoadClientDto>(x)).ToList();
            }
            catch (Exception ex)
            {
                _loggingService.LogException(ex);
                return null;
            }
        }

        public async Task<LoadClientDto> GetClientById(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<List<LoadClientDto>> DeleteClient(int id)
        {
            throw new NotImplementedException();
        }


    }
}