using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using OpenSportsPlatform.Lib.Database;
using OpenSportsPlatform.Lib.Model.Dtos.WorkoutOverview;
using OpenSportsPlatform.Lib.Services.Contract;
using OpenSportsPlatform.Lib.Util;
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

        public async Task<PagedResultDto<WorkoutOverviewItemDto>> SearchWorkoutItems(SearchWorkoutsDto search)
        {
            var query = _dbContext.Workout;

            IList<WorkoutOverviewItemDto> data = await query
                .Select(x => new WorkoutOverviewItemDto()
                {
                    Id = x.Id,
                    StartTime = x.StartTime,
                    EndTime = x.EndTime,
                    Sport = x.SportsCategory.Name,
                    DistanceInKm = x.DistanceInKm,
                    DurationInSec = x.DurationInSec,
                })
                .OrderByDescending(x => x.StartTime)
                .Skip(PageSize * search.Page)
                .Take(PageSize)
                .ToListAsync();

            foreach(var item in data)
            {
                item.StartTime = item.StartTime.AsUtc();
                item.EndTime = item.EndTime.AsUtc();
            }

            var count = await query.CountAsync();

            return new PagedResultDto<WorkoutOverviewItemDto>()
            {
                Data = data,
                Count = count,
            };
        }
    }
}
