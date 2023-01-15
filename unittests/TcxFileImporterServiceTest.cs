using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using OpenSportsPlatform.Lib.Database;
using OpenSportsPlatform.Lib.Model.Entities;
using OpenSportsPlatform.Lib.Services.Contract;
using OpenSportsPlatform.Lib.Services.Impl;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using unittests.util;
using Xunit;

namespace unittests
{
    public class TcxFileImporterServiceTest
    {
        [Fact]
        public async Task TestImport()
        {
            IPrincipal principal = MockPrincipal.CreatePrincipal();
            ISecurityService securityService = new SecurityService(principal);
            ILogger<TcxFileImporterService> logger = new MockLogger<TcxFileImporterService>();
            string dbName = Guid.NewGuid().ToString();

            using (OpenSportsPlatformDbContext dbContext = MockDatabaseInMemory.GetDatabase(dbName, principal))
            {
                dbContext.Database.EnsureCreated();

                UserProfile userProfile = new UserProfile();
                userProfile.UserId = securityService.GetCurrentUserid();
                await dbContext.AddAsync(userProfile);

                await dbContext.SaveChangesAsync();
            }
            using (OpenSportsPlatformDbContext dbContext = MockDatabaseInMemory.GetDatabase(dbName, principal))
            {
                ITcxFileImporterService service = new TcxFileImporterService(logger, securityService, dbContext);
                Stream stream = File.OpenRead("Files\\testactivity.tcx");
                await service.ImportWorkout(stream);
            }

            // Assert
            using (OpenSportsPlatformDbContext dbContext = MockDatabaseInMemory.GetDatabase(dbName, principal))
            {
                var numSamples = await dbContext.Sample
                    .Where(x => x.Segment.Workout.UserProfile.UserId == securityService.GetCurrentUserid())
                    .CountAsync();
                Assert.True(numSamples > 0);

            }
        }
    }
}
