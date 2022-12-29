using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using OpenSportsPlatform.Lib.Database;
using OpenSportsPlatform.Lib.Model.Dtos.Statistics;
using OpenSportsPlatform.Lib.Model.Dtos.Workout;
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
    public class WorkoutServiceTest
    {
        [Fact]
        public async Task TestSave()
        {
            IPrincipal principal = MockPrincipal.CreatePrincipal();
            ISecurityService securityService = new SecurityService(principal);        
            string dbName = Guid.NewGuid().ToString();
            int workOutId = 0;
            int sportsCategoryId = 0;

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

                dbContext.SaveChanges();
                workOutId = workout.Id;
                sportsCategoryId = sportsCategory.Id;
            }

            using (OpenSportsPlatformDbContext dbContext = MockDatabaseInMemory.GetDatabase(dbName, principal))
            {
                IWorkoutService workoutService = new WorkoutService(dbContext, new MockLogger<WorkoutService>(), securityService);
                SaveWorkoutDto dto = new SaveWorkoutDto()
                {
                    Id = workOutId,
                    Notes = "A note of a good session",
                    SportsCategoryId = sportsCategoryId,
                    Tag = "Blüemlisalplauf",
                };

                await workoutService.SaveWorkout(dto);
            }

            using (OpenSportsPlatformDbContext dbContext = MockDatabaseInMemory.GetDatabase(dbName, principal))
            {
                Workout wo = await dbContext
                    .Workout
                    .Where(x => x.Id == workOutId)
                    .Include(x => x.TagWorkouts)
                    .ThenInclude(x => x.Tag)
                    .FirstAsync();

                Assert.Equal("A note of a good session", wo.Notes);
            }

        }
    }
}
