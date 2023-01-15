using OpenSportsPlatform.Lib.Database;
using OpenSportsPlatform.Lib.Model.Dtos.WorkoutOverview;
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
    public class WorkoutOverviewServiceTest
    {
        [Fact]
        public async Task GetWorkouts()
        {
            // Setup
            IPrincipal principal = MockPrincipal.CreatePrincipal(name: "eric");
            ISecurityService securityService = new SecurityService(principal);
            string dbName = Guid.NewGuid().ToString();

            using (OpenSportsPlatformDbContext dbContext = MockDatabaseInMemory.GetDatabase(dbName, principal))
            {
                await CreateWorkouts(dbContext);
            }

            // Act and Assert
            using (OpenSportsPlatformDbContext dbContext = MockDatabaseInMemory.GetDatabase(dbName, principal))
            {
                IWorkoutOverviewService service = CreateService(dbContext, securityService);
                var res = await service.SearchWorkoutItems(new SearchWorkoutsDto()
                {
                    Page = 0,
                });
                Assert.Equal(2, res.Count);
                Assert.Equal(2, res.Data.Count);
            }
        }


        private static async Task CreateWorkouts(OpenSportsPlatformDbContext dbContext)
        {
            SportsCategory sportsCategory = new SportsCategory()
            {
                Name = "Cycling",
            };
            await dbContext.AddAsync(sportsCategory);

            Tag tag = new Tag { Name = "Testtag" };
            await dbContext.AddAsync(new Tag { Name = "Testtag" });

            await dbContext.SaveChangesAsync();

            await CreateWorkoutsForUser(dbContext, "eric", sportsCategory, tag);
            await CreateWorkoutsForUser(dbContext, "kyle", sportsCategory, tag);
        }

        private static async Task CreateWorkoutsForUser(OpenSportsPlatformDbContext dbContext, string userID, SportsCategory sportsCategory, Tag tag)
        {
            UserProfile userProfie = new UserProfile()
            {
                Name = $"Name {userID}",
                UserId = userID,
            };
            await dbContext.AddAsync(userProfie);


            await AddWorkout(dbContext, userProfie, sportsCategory, tag);
            await AddWorkout(dbContext, userProfie, sportsCategory, tag);            

            await dbContext.SaveChangesAsync();
        }

        private static async Task AddWorkout(OpenSportsPlatformDbContext dbContext, UserProfile userProfile, SportsCategory sportsCategory, Tag tag)
        {
            Workout workout = new Workout()
            {
                UserProfile = userProfile,
                DistanceInKm = 12,
                StartTime = DateTime.Today.AddDays(-10),
                SportsCategory = sportsCategory,
            };

            await dbContext.AddAsync(workout);

            TagWorkout tagWorkout = new TagWorkout() { Tag = tag, Workout = workout };
            await dbContext.AddAsync(tagWorkout);
        }

        private static IWorkoutOverviewService CreateService(OpenSportsPlatformDbContext dbContext, ISecurityService securityService)
        {
            IWorkoutOverviewService workoutService = new WorkoutOverviewService(dbContext, securityService, new MockLogger<WorkoutOverviewService>());
            return workoutService;
        }
    }
}
