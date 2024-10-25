
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using personal_trainer_api.Data;
using personal_trainer_api.Dtos.User;
using personal_trainer_api.Services.LoggingService;

namespace personal_trainer_api.DataAccess.Trainers
{
    public class TrainerDataAccess(DataContext context, IMapper mapper, ILoggingService loggingService) : ITrainerDataAccess
    {
        private readonly DataContext _context = context;
        private readonly IMapper _mapper = mapper;
        private readonly ILoggingService _loggingService = loggingService;

        public async Task<List<LoadUserDto>> GetClients(int trainerId)
        {
            try
            {
                var dbClients = await _context.Clients
                        .Where(x => x.TrainerId == trainerId)
                        .OrderBy(x => x.FirstName)
                        .ToListAsync();

                return dbClients.Select(x => _mapper.Map<LoadUserDto>(x)).ToList();
            }
            catch (Exception ex)
            {
                _loggingService.LogException(ex);
                return null;
            }

        }

        public async Task<List<LoadUserDto>> GetLastVisitedClients(int trainerId)
        {
            try
            {
                var dbClients = await _context.Clients
                        .Where(x => x.TrainerId == trainerId)
                        .OrderByDescending(x => x.LastVisited)
                        .ToListAsync();

                return dbClients.Select(_mapper.Map<LoadUserDto>).ToList();
            }
            catch (Exception ex)
            {
                _loggingService.LogException(ex);
                return null;
            }
        }

        public async Task<List<LoadUserDto>> GetTrainers()
        {
            try
            {
                var dbTrainers = await _context.Trainers
                        .OrderBy(x => x.FirstName)
                        .ToListAsync();

                return dbTrainers.Select(x => _mapper.Map<LoadUserDto>(x)).ToList();
            }
            catch (Exception ex)
            {
                _loggingService.LogException(ex);
                return null;
            }
        }
    }
}