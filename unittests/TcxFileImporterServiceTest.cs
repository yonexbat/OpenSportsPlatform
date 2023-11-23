using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using OpenSportsPlatform.Lib.Database;
using OpenSportsPlatform.Lib.Model.Entities;
using OpenSportsPlatform.Lib.Services.Contract;
using OpenSportsPlatform.Lib.Services.Impl;
using System;
using System.IO;
using System.Linq;
using System.Security.Principal;
using System.Threading.Tasks;
using unittests.util;
using Xunit;

namespace unittests;

public class TcxFileImporterServiceTest
{
    [Fact]
    public async Task TestImport()
    {
        // Arrange
        IPrincipal principal = MockPrincipal.CreatePrincipal();
        ISecurityService securityService = new SecurityService(principal);
        ILogger<TcxFileImporterService> logger = new MockLogger<TcxFileImporterService>();
        string dbName = Guid.NewGuid().ToString();

        await using (OpenSportsPlatformDbContext dbContext = MockDatabaseInMemory.GetDatabase(dbName, principal))
        {
            await dbContext.Database.EnsureCreatedAsync();

            UserProfile userProfile = new UserProfile();
            userProfile.UserId = securityService.GetCurrentUserid();
            await dbContext.AddAsync(userProfile);

            await dbContext.SaveChangesAsync();
        }

        await using (OpenSportsPlatformDbContext dbContext = MockDatabaseInMemory.GetDatabase(dbName, principal))
        {
            ITcxFileImporterService service = new TcxFileImporterService(logger, securityService, dbContext);
            Stream stream = File.OpenRead($"Files{Path.DirectorySeparatorChar}testactivity.tcx");
                
            //  Act
            await service.ImportWorkout(stream);
        }

        // Assert
        await using (OpenSportsPlatformDbContext dbContext = MockDatabaseInMemory.GetDatabase(dbName, principal))
        {
            var numSamples = await dbContext.Sample
                .Where(x => x.Segment.Workout.UserProfile.UserId == securityService.GetCurrentUserid())
                .CountAsync();
            Assert.True(numSamples > 0);

        }
    }
}