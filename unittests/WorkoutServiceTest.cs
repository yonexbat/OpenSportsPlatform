using Azure;
using Microsoft.EntityFrameworkCore;
using OpenSportsPlatform.Lib.Database;
using OpenSportsPlatform.Lib.Model.Dtos.Workout;
using OpenSportsPlatform.Lib.Model.Entities;
using OpenSportsPlatform.Lib.Services.Contract;
using OpenSportsPlatform.Lib.Services.Impl;
using System;
using System.Linq;
using System.Security.Principal;
using System.Threading.Tasks;
using unittests.util;
using Xunit;
using Xunit.Sdk;

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
                Workout workout = await SetUpWorkout(dbContext, securityService);
                workOutId = workout.Id;
                sportsCategoryId = workout.SportsCategoryId;
            }

            // Act
            using (OpenSportsPlatformDbContext dbContext = MockDatabaseInMemory.GetDatabase(dbName, principal))
            {
                IWorkoutService workoutService = CreateService(dbContext, securityService);
                SaveWorkoutDto dto = new SaveWorkoutDto()
                {
                    Id = workOutId,
                    Notes = "A note of a good session",
                    SportsCategoryId = sportsCategoryId,
                };

                await workoutService.SaveWorkout(dto);
            }

            // Assert
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

        [Fact]
        public async Task TestSearchTag()
        {
            IPrincipal principal = MockPrincipal.CreatePrincipal();
            ISecurityService securityService = new SecurityService(principal);
            string dbName = Guid.NewGuid().ToString();
            using (OpenSportsPlatformDbContext dbContext = MockDatabaseInMemory.GetDatabase(dbName, principal))
            {
                await dbContext.AddAsync(new Tag() { Name = "Blüemlisalp Lauf" });
                await dbContext.AddAsync(new Tag() { Name = "Grand-Prix" });
                await dbContext.AddAsync(new Tag() { Name = "Gurten-Classic" });
                await dbContext.SaveChangesAsync();
            }

            // Act and assert
            using (OpenSportsPlatformDbContext dbContext = MockDatabaseInMemory.GetDatabase(dbName, principal))
            {
                IWorkoutService service = CreateService(dbContext, securityService);
                var res = await service.SearchTags("Grand");
                Assert.Single(res);
            }
        }

        [Fact]
        public async Task TestAddTag()
        {
            IPrincipal principal = MockPrincipal.CreatePrincipal();
            ISecurityService securityService = new SecurityService(principal);
            string dbName = Guid.NewGuid().ToString();
            int workOutId = 0;

            await using (OpenSportsPlatformDbContext dbContext = MockDatabaseInMemory.GetDatabase(dbName, principal))
            {
                Workout workout = await SetUpWorkout(dbContext, securityService);
                workOutId = workout.Id;
            }

            // Act
            await using (OpenSportsPlatformDbContext dbContext = MockDatabaseInMemory.GetDatabase(dbName, principal))
            {
                IWorkoutService service = CreateService(dbContext, securityService);
                await service.AddTag(new AddTagDto() { Id = workOutId, Name = "Bluemlisalp-Lauf" });
            }

            // Assert
            await using (OpenSportsPlatformDbContext dbContext = MockDatabaseInMemory.GetDatabase(dbName, principal))
            {
                Workout wo = await dbContext
                    .Workout
                    .Where(x => x.Id == workOutId)
                    .Include(x => x.TagWorkouts)
                    .ThenInclude(x => x.Tag)
                    .FirstAsync();
                Assert.Single(wo.TagWorkouts);
                Assert.Equal("Bluemlisalp-Lauf", wo.TagWorkouts[0].Tag.Name);
            }
        }

        [Fact]
        public async Task TestRemoveTag()
        {
            IPrincipal principal = MockPrincipal.CreatePrincipal();
            ISecurityService securityService = new SecurityService(principal);
            string dbName = Guid.NewGuid().ToString();
            int workOutId = 0;
            int tagId = 0;

            using (OpenSportsPlatformDbContext dbContext = MockDatabaseInMemory.GetDatabase(dbName, principal))
            {
                Workout workout = await SetUpWorkout(dbContext, securityService);
                workOutId = workout.Id;
                Tag tag = new Tag { Name = "TestTag" };
                await dbContext.Tag.AddAsync(tag);
                await dbContext.TagWorkout.AddAsync(new TagWorkout { Tag = tag, Workout = workout });
                await dbContext.SaveChangesAsync();
                tagId = tag.Id;
            }

            // Act
            using (OpenSportsPlatformDbContext dbContext = MockDatabaseInMemory.GetDatabase(dbName, principal))
            {
                IWorkoutService service = CreateService(dbContext, securityService);
                await service.RemoveTag(new RemoveTagDto { Id = workOutId, Name = "TestTag" });
            }

            // Assert
            using (OpenSportsPlatformDbContext dbContext = MockDatabaseInMemory.GetDatabase(dbName, principal))
            {
                Workout wo = await dbContext
                    .Workout
                    .Where(x => x.Id == workOutId)
                    .Include(x => x.TagWorkouts)
                    .ThenInclude(x => x.Tag)
                    .FirstAsync();
                Assert.Empty(wo.TagWorkouts);
            }
        }

        [Fact]
        public async Task TestEditWorkout()
        {
            IPrincipal principal = MockPrincipal.CreatePrincipal();
            ISecurityService securityService = new SecurityService(principal);
            string dbName = Guid.NewGuid().ToString();
            int workOutId = 0;

            await using (OpenSportsPlatformDbContext dbContext = MockDatabaseInMemory.GetDatabase(dbName, principal))
            {
                Workout workout = await SetUpWorkout(dbContext, securityService);
                workOutId = workout.Id;
                Tag tag = new Tag { Name = "gurten classic" };
                await dbContext.Tag.AddAsync(tag);
                await dbContext.TagWorkout.AddAsync(new TagWorkout { Tag = tag, Workout = workout });
                await dbContext.SaveChangesAsync();
            }

            // Act and Assert
            await using (OpenSportsPlatformDbContext dbContext = MockDatabaseInMemory.GetDatabase(dbName, principal))
            {
                IWorkoutService service = CreateService(dbContext, securityService);
                var res = await service.GetEditWorkout(workOutId);

                // Assert
                Assert.NotNull(res);
                Assert.NotNull(res.Tags);
            }
        }

        private static async Task<Workout> SetUpWorkout(OpenSportsPlatformDbContext dbContext,
            ISecurityService securityService)
        {
            await dbContext.Database.EnsureCreatedAsync();

            UserProfile userProfile = new UserProfile();
            userProfile.UserId = securityService.GetCurrentUserid();
            await dbContext.AddAsync(userProfile);

            SportsCategory sportsCategory = new SportsCategory()
            {
                Name = "RUNNING",
            };
            await dbContext.AddAsync(sportsCategory);

            Workout workout = new Workout()
            {
                UserProfile = userProfile,
                DistanceInKm = 10,
                SportsCategory = sportsCategory,
                StartTime = new DateTimeOffset(2021, 9, 13, 0, 0, 0, TimeSpan.Zero),
            };
            await dbContext.AddAsync(workout);

            await dbContext.SaveChangesAsync();
            return workout;
        }


        private static IWorkoutService CreateService(OpenSportsPlatformDbContext dbContext,
            ISecurityService securityService)
        {
            IWorkoutService workoutService =
                new WorkoutService(dbContext, new MockLogger<WorkoutService>(), securityService);
            return workoutService;
        }
    }
}