using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using OpenSportsPlatform.Lib.Database;
using OpenSportsPlatform.Lib.Model.Dtos.Statistics;
using OpenSportsPlatform.Lib.Services.Contract;
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

        public StatisticsService(ILogger<StatisticsService> logger,
            ISecurityService securityService,           
            OpenSportsPlatformDbContext dbContext)
        {
            _logger = logger;
            _dbContext = dbContext;
            _securityService = securityService;
        }

        public async Task<StatisticsDto> GetStatistics(GetStatisticsDto dto)
        {
            StatisticsDto result = new StatisticsDto();

            int year = DateTime.Today.Year;
            int month = DateTime.Today.Month;
            DateTime fromMonth = new DateTime(year, month, 1);
            DateTime fromYear = new DateTime(year, 1, 1);
            DateTime toExcl = fromMonth.AddMonths(1);

            string userId = _securityService.GetCurrentUserid();

            result.MonthItems = await GetStatsForPeriod(fromMonth, toExcl, userId);
            result.YearItems = await GetStatsForPeriod(fromYear, toExcl, userId);

            return result;
        }

        private async Task<IList<StatisticsItemDto>> GetStatsForPeriod(DateTime from, DateTime toExcl, string userId)
        {
            return await _dbContext.Workout
                .Where(x => x.UserProfile.UserId == userId)
                .Where(x => x.StartTime >= from && x.StartTime < toExcl)
                .GroupBy(x => new { x.SportsCategory.Name })
                .Select(g => new StatisticsItemDto()
                {
                    SportsCategoryName = g.Key.Name,
                    DistanceInKm = g.Sum(wo => wo.DistanceInKm),
                })
                .ToListAsync();
        }
    }
}
