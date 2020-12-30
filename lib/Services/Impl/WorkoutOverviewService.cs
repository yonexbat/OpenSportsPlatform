using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using OpenSportsPlatform.Lib.Database;
using OpenSportsPlatform.Lib.Model.Dtos.WorkoutOverview;
using OpenSportsPlatform.Lib.Services.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenSportsPlatform.Lib.Services.Impl
{
    public class WorkoutOverviewService : IWorkoutOverviewService
    {
        private readonly ILogger _logger;
        private readonly OpenSportsPlatformDbContext _dbContext;
        private static int PageSize = 10;

        public WorkoutOverviewService(
            OpenSportsPlatformDbContext dbContext,
            ILogger<WorkoutOverviewService> logger)
        {
            _dbContext = dbContext;
            _logger = logger;
        }

        public async Task<IList<WorkoutOverviewItemDto>> SearchWorkoutItems(SearchWorkoutsDto search)
        {
            return await _dbContext.Workout
                .Select(x => new WorkoutOverviewItemDto()
                {
                    StartTime = x.StartTime,
                    EndTime = x.EndTime,
                })
                .OrderByDescending(x => x.StartTime)
                .Skip(PageSize*search.Page)
                .Take(PageSize)
                .ToListAsync();
        }
    }
}
