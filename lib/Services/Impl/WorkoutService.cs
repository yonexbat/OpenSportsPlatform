using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using OpenSportsPlatform.Lib.Database;
using OpenSportsPlatform.Lib.Model;
using OpenSportsPlatform.Lib.Model.Dtos.Common;
using OpenSportsPlatform.Lib.Model.Dtos.Workout;
using OpenSportsPlatform.Lib.Model.Entities;
using OpenSportsPlatform.Lib.Services.Contract;
using System.Linq;
using System.Security;
using System.Threading.Tasks;

namespace OpenSportsPlatform.Lib.Services.Impl
{
    public class WorkoutService : IWorkoutService
    {
        private readonly ILogger _logger;
        private readonly OpenSportsPlatformDbContext _dbContext;
        private readonly ISecurityService _securityService;

        public WorkoutService(OpenSportsPlatformDbContext dbContext,
            ILogger<WorkoutService> logger,
            ISecurityService securityService)
        {
            _dbContext = dbContext;
            _logger = logger;
            _securityService = securityService;
        }

        public async Task<bool> DeleteWorkout(int id)
        {
            _logger.LogInformation("Deleting workout with id {0}", id);
            Workout workout = await _dbContext.Workout.Where(x => x.Id == id)
                .Include(x => x.UserProfile)
                .Include(x => x.Segments)
                .ThenInclude(x => x.Samples)
                .SingleAsync();

            _securityService.CheckAccess(workout);

            _dbContext.Remove(workout);
            await _dbContext.SaveChangesAsync();
            return true;
        }

        public async Task<EditWorkoutDto> GetEditWorkout(int id)
        {
            _logger.LogDebug("GetEditWorkout. Id: {0}", id);
            EditWorkoutDto result = await _dbContext.Workout
                .Where(x => x.Id == id)
                .Select(x => new EditWorkoutDto()
                {
                    Id = x.Id,
                    SportsCategoryId = x.SportsCategoryId,
                    Notes = x.Notes,
                }).SingleAsync();

            result.FirstSampleTimestamp = await _dbContext.Sample
                .Where(x => x.Segment.Workout.Id == id)
                .Where(x => x.Timestamp.HasValue)
                .OrderBy(x => x.Id)
                .MinAsync(x => x.Timestamp);
                

            result.LastSampleTimestamp = await _dbContext.Sample
                .Where(x => x.Segment.Workout.Id == id)
                .Where(x => x.Timestamp.HasValue)
                .OrderBy(x => x.Id)
                .MaxAsync(x => x.Timestamp);

            result.SportsCategories = await _dbContext.SportsCategory.Select(
                x => new SelectItemDto()
                {
                    Id = x.Id,
                    Name = x.Name,
                })
                .OrderBy(x => x.Name)
                .ToListAsync();

            result.Ticks = (result.LastSampleTimestamp - result.FirstSampleTimestamp)?.TotalMilliseconds;

            return result;
        }

        public async Task<WorkoutDto> GetWorkout(int id)
        {
            var res = await _dbContext
                .Workout
                .Where(x => x.Id == id)
                .Select(x => new WorkoutDto()
                {
                    Id = x.Id,
                    StartTime = x.StartTime,
                    EndTime = x.EndTime,
                    Sport = x.SportsCategory.Name,
                    DistanceInKm = x.DistanceInKm,
                    AscendInMeters = x.AscendInMeters,
                    DescendInMeters = x.DescendInMeters,
                    CaloriesInKCal = x.CaloriesInKCal,
                    HeartRateAvgBpm = x.HeartRateAvgBpm,
                    HeartRateMaxBpm = x.HeartRateMaxBpm,
                    DurationInSec = x.DurationInSec,
                }).SingleAsync();

            res.StartTime = res.StartTime;
            res.EndTime = res.EndTime;

            //Samples
            res.Samples = await _dbContext
                .Sample
                .Where(x => x.Segment.Workout.Id == id)
                .Where(x => x.Latitude.HasValue && x.Longitude.HasValue)
                .OrderBy(x => x.Timestamp)
                .Select(x => new SampleDto()
                {
                    Latitude = x.Latitude,
                    Longitude = x.Longitude,
                    AltitudeInMeters = x.AltitudeInMeters,
                    HeartRateBpm = x.HeartRateBpm,
                    Timestamp = x.Timestamp,
                }).ToListAsync();

            return res;
        }

        public async Task<bool> SaveWorkout(SaveWorkoutDto dto)
        {
            var workout = await _dbContext.Workout
                .Where(x => x.Id == dto.Id)
                .Include(x => x.UserProfile)
                .SingleAsync();

            _securityService.CheckAccess(workout);

            workout.SportsCategoryId = dto.SportsCategoryId ?? throw new ArgumentNullException(nameof(SaveWorkoutDto.SportsCategoryId));
            workout.Notes = dto.Notes;
            await _dbContext.SaveChangesAsync();

            return true;
        }
       
    }
}
