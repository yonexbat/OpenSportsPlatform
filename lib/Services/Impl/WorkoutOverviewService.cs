using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using OpenSportsPlatform.Lib.Database;
using OpenSportsPlatform.Lib.Model.Dtos.WorkoutOverview;
using OpenSportsPlatform.Lib.Model.Entities;
using OpenSportsPlatform.Lib.Services.Contract;

namespace OpenSportsPlatform.Lib.Services.Impl;

public class WorkoutOverviewService : IWorkoutOverviewService
{
    private readonly ILogger _logger;
    private readonly OpenSportsPlatformDbContext _dbContext;
    private readonly ISecurityService _securityService;
    private static int PageSize = 10;

    public WorkoutOverviewService(
        OpenSportsPlatformDbContext dbContext,
        ISecurityService securityService,
        ILogger<WorkoutOverviewService> logger)
    {
        _dbContext = dbContext;
        _securityService = securityService;
        _logger = logger;
    }

    public async Task<PagedResultDto<WorkoutOverviewItemDto>> SearchWorkoutItems(SearchWorkoutsDto search)
    {
        string userId = _securityService.GetCurrentUserid();
        _logger.LogInformation("Searching for workouts. userId: {userId}", userId);

        var query = _dbContext.Workout
            .Where(x => x.UserProfile!.UserId == userId);

        var count = await query.CountAsync();


        IList<Workout> workouts = await query
            .Include(x => x.TagWorkouts!)
            .ThenInclude(tw => tw.Tag)
            .Include(wo => wo.SportsCategory)
            .OrderByDescending(x => x.StartTime)
            .Skip(PageSize * search.Page)
            .Take(PageSize)
            .ToListAsync();
                      
        var data = workouts.Select(wo => new WorkoutOverviewItemDto()
            {
                DistanceInKm = wo.DistanceInKm,
                DurationInSec = wo.DurationInSec,
                EndTime = wo.EndTime,
                StartTime = wo.StartTime,
                Id = wo.Id,
                Sport = wo.SportsCategory?.Name,
                Tags = wo.TagWorkouts?.Select(tw => tw.Tag.Name).ToList(),
            })
            .ToList();

        return new PagedResultDto<WorkoutOverviewItemDto>()
        {
            Data = data,
            Count = count,
        };
    }
}