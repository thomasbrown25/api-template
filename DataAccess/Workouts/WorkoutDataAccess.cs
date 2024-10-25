using AutoMapper;
using Microsoft.EntityFrameworkCore;
using personal_trainer_api.Data;
using personal_trainer_api.Dtos.Workout;
using personal_trainer_api.Services.LoggingService;

namespace personal_trainer_api.DataAccess.Workouts
{
    public class WorkoutDataAccess(DataContext context, IMapper mapper, ILoggingService logging) : IWorkoutDataAccess
    {
        private readonly DataContext _context = context;
        private readonly ILoggingService _loggingService = logging;
        private readonly IMapper _mapper = mapper;

        public async Task<LoadWorkoutDto> AddWorkout(AddWorkoutDto newWorkout)
        {
            try
            {
                var dbWorkout = _mapper.Map<Workout>(newWorkout);

                await _context.Workouts.AddAsync(dbWorkout);
                await _context.SaveChangesAsync();

                return _mapper.Map<LoadWorkoutDto>(dbWorkout);
            }
            catch (Exception ex)
            {
                _loggingService.LogException(ex);
                return null;
            }
        }

        public async Task<LoadWorkoutDto> GetWorkout(int id)
        {
            try
            {
                var dbWorkout = await _context.Workouts
                    .Where(x => x.Id == id)
                    .FirstOrDefaultAsync();

                return _mapper.Map<LoadWorkoutDto>(dbWorkout);
            }
            catch (Exception ex)
            {
                _loggingService.LogException(ex);
                return null;
            }
        }

        public async Task<List<LoadWorkoutDto>> GetWorkouts()
        {
            try
            {
                var dbWorkouts = await _context.Workouts
                    .ToListAsync();

                return dbWorkouts.Select(_mapper.Map<LoadWorkoutDto>).ToList();
            }
            catch (Exception ex)
            {
                _loggingService.LogException(ex);
                return null;
            }
        }
    }
}