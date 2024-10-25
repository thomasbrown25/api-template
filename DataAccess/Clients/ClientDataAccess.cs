using AutoMapper;
using personal_trainer_api.Data;
using personal_trainer_api.Dtos.Client;
using personal_trainer_api.Services.LoggingService;
using Microsoft.EntityFrameworkCore;
using personal_trainer_api.Dtos.User;

namespace personal_trainer_api.DataAccess.Clients
{
    public class ClientDataAccess(DataContext context, IMapper mapper, ILoggingService logging) : IClientDataAccess
    {
        private readonly DataContext _context = context;
        private readonly ILoggingService _loggingService = logging;
        private readonly IMapper _mapper = mapper;

        public async Task<LoadUserDto> UpdateClient(UpdateClientDto newClient)
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

                return _mapper.Map<LoadUserDto>(newClient);
            }
            catch (Exception ex)
            {
                _loggingService.LogException(ex);
                return null;
            }
        }

        public async Task<List<LoadUserDto>> GetClients()
        {
            try
            {
                var dbClients = await _context.Clients
                        .OrderBy(x => x.FirstName)
                        .ToListAsync();

                return dbClients.Select(_mapper.Map<LoadUserDto>).ToList();
            }
            catch (Exception ex)
            {
                _loggingService.LogException(ex);
                return null;
            }
        }

        public async Task<LoadUserDto> GetClientById(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<List<LoadUserDto>> DeleteClient(int id)
        {
            throw new NotImplementedException();
        }
    }
}