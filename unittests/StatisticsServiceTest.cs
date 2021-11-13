using Microsoft.Extensions.Logging;
using OpenSportsPlatform.Lib.Database;
using OpenSportsPlatform.Lib.Model.Dtos.Statistics;
using OpenSportsPlatform.Lib.Model.Entities;
using OpenSportsPlatform.Lib.Services.Contract;
using OpenSportsPlatform.Lib.Services.Impl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using unittests.util;
using Xunit;

namespace unittests
{
    public class StatisticsServiceTest
    {
        [Fact]
        public async Task TestGet()
        {
            IPrincipal principal = MockPrincipal.CreatePrincipal();
            ISecurityService securityService = new SecurityService(principal);
            ILogger<StatisticsService> logger = new MockLogger<StatisticsService>();
            ICurrentDateTimeService currentDateTimeService = new MockCurrentDateTimeService(new DateTimeOffset(2021, 11, 14, 0, 0, 0, TimeSpan.Zero));
            string dbName = Guid.NewGuid().ToString();

            using (OpenSportsPlatformDbContext dbContext = MockDatabaseInMemory.GetDatabase(dbName, principal))
            {
                dbContext.Database.EnsureCreated();

                UserProfile userProfile = new UserProfile();
                userProfile.UserId = securityService.GetCurrentUserid();
                dbContext.Add(userProfile);

                SportsCategory sportsCategory = new SportsCategory()
                {
                    Name = "RUNNING",
                };
                dbContext.Add(sportsCategory);

                Workout workout = new Workout()
                {
                    UserProfile = userProfile,
                    DistanceInKm = 10,
                    SportsCategory = sportsCategory,
                    StartTime = new DateTimeOffset(2021, 9, 13, 0, 0, 0, TimeSpan.Zero),
                };
                dbContext.Add(workout);

                workout = new Workout()
                {
                    UserProfile = userProfile,
                    DistanceInKm = 30,
                    SportsCategory = sportsCategory,
                    StartTime = new DateTimeOffset(2021, 10, 13, 0, 0, 0, TimeSpan.Zero),
                };
                dbContext.Add(workout);


                workout = new Workout()
                {
                    UserProfile = userProfile,
                    DistanceInKm = 42,
                    SportsCategory = sportsCategory,
                    StartTime = new DateTimeOffset(2021, 11, 13, 0, 0, 0, TimeSpan.Zero),
                };
                dbContext.Add(workout);

                dbContext.SaveChanges();
            }

            using (OpenSportsPlatformDbContext dbContext = MockDatabaseInMemory.GetDatabase(dbName, principal))
            {
                IStatisticsService statisticsService = new StatisticsService(logger, securityService, currentDateTimeService, dbContext);
                var stats = await statisticsService.GetStatistics(new GetStatisticsDto());
            }

        }
    }
}
