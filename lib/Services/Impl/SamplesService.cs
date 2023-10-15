using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using OpenSportsPlatform.Lib.Database;
using OpenSportsPlatform.Lib.Model.Dtos.Workout;
using OpenSportsPlatform.Lib.Services.Contract;
namespace OpenSportsPlatform.Lib.Services.Impl;

public class SamplesService : ISamplesService
{
    private readonly ISecurityService _securityService;
    private readonly ILogger _logger;
    private readonly OpenSportsPlatformDbContext _dbContext;
    
    public SamplesService(
        OpenSportsPlatformDbContext dbContext,
        ISecurityService securityService,
        ILogger<SamplesService> logger
        )
    {
        _securityService = securityService;
        _logger = logger;
        _dbContext = dbContext;
    }

    public async Task<IAsyncEnumerable<SampleDto>> GetSamples(int workoutId)
    {
        _logger.LogInformation("Getting workout for {workoutId}", workoutId);
        
        var workout = await _dbContext.Workout
            .Where(x => x.Id == workoutId)
            .Include(x => x.UserProfile)
            .Include(x => x.SportsCategory)
            .SingleAsync();
        
        _securityService.CheckAccess(workout);
        
        var samples = _dbContext
            .Sample
            .Where(x => x.Segment!.Workout!.Id == workoutId)
            .Where(x => x.Latitude.HasValue && x.Longitude.HasValue)
            .OrderBy(x => x.Timestamp)
            .Select(x => new SampleDto()
            {
                Latitude = x.Latitude,
                Longitude = x.Longitude,
                AltitudeInMeters = x.AltitudeInMeters,
                HeartRateBpm = x.HeartRateBpm,
                Timestamp = x.Timestamp,
            });
        
        return samples.AsAsyncEnumerable();
    }
}