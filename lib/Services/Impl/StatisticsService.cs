using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using OpenSportsPlatform.Lib.Database;
using OpenSportsPlatform.Lib.Model.Dtos.Common;
using OpenSportsPlatform.Lib.Model.Dtos.Statistics;
using OpenSportsPlatform.Lib.Services.Contract;
using OpenSportsPlatform.Lib.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenSportsPlatform.Lib.Services.Impl
{
    public class StatisticsService : IStatisticsService
    {
        private readonly ILogger _logger;
        private readonly OpenSportsPlatformDbContext _dbContext;
        private readonly ISecurityService _securityService;
        private readonly ICurrentDateTimeService _currentDateTimeService;

        public StatisticsService(
            ILogger<StatisticsService> logger,
            ISecurityService securityService,   
            ICurrentDateTimeService currentDateTimeService,
            OpenSportsPlatformDbContext dbContext)
        {
            _logger = logger;
            _dbContext = dbContext;
            _securityService = securityService;
            _currentDateTimeService = currentDateTimeService;
        }

        public async Task<StatisticsDto> GetStatistics(GetStatisticsDto dto)
        {
            StatisticsDto result = new StatisticsDto();
            await GetStatisticsPerSport(result);
            await GetRunningLastYear(result);

            return result;
        }

        private async Task GetRunningLastYear(StatisticsDto result)
        {
            DateTimeOffset now = _currentDateTimeService.GetCurrentTime();
            DateTimeOffset start = now.FirstOfMonth().AddMonths(-11);
            result.RunningLast12Months = new List<MonthValueItemDto>();
            string[] categoies = new string[] { "RUNNING", "HIKING", "WALKING" };
            for (int i=0; i<12; i++)
            {
                var from = start.AddMonths(i);
                var to = start.AddMonths(i + 1);

                var sum = await _dbContext.Workout
                    .Where(x => x.StartTime >= from)
                    .Where(x => x.StartTime < to)
                    .Where(x => categoies.Any(y => y == x.SportsCategory.Name))
                    .SumAsync(x => x.DistanceInKm);

                result.RunningLast12Months.Add(new MonthValueItemDto()
                {
                    Month = from.Month,
                    Year = from.Year,
                    Value = sum ?? 0,
                });
            }
        }

        private async Task GetStatisticsPerSport(StatisticsDto result)
        {
            DateTimeOffset now = _currentDateTimeService.GetCurrentTime();

            int year = now.Year;
            int month = now.Month;
            DateTime fromMonth = new DateTime(year, month, 1);
            DateTime fromYear = new DateTime(year, 1, 1);
            DateTime toExcl = fromMonth.AddMonths(1);

            string userId = _securityService.GetCurrentUserid();

            result.MonthItems = await GetStatsForPeriod(fromMonth, toExcl, userId);
            result.YearItems = await GetStatsForPeriod(fromYear, toExcl, userId);
        }


        private async Task<IList<StatisticsItemDto>> GetStatsForPeriod(DateTime from, DateTime toExcl, string userId)
        {
            return await _dbContext.Workout
                .Where(x => x.UserProfile!.UserId == userId)
                .Where(x => x.StartTime >= from && x.StartTime < toExcl)
                .GroupBy(x => new { x.SportsCategory!.Name })
                .Select(g => new StatisticsItemDto()
                {
                    SportsCategoryName = g.Key.Name!,
                    DistanceInKm = g.Sum(wo => wo.DistanceInKm),
                })
                .ToListAsync();
        }
    }
}
