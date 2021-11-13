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


            using (var connection = MockDatabaseSqlLite.CreateInMemoryDatabase())
            {
                using (OpenSportsPlatformDbContext dbContext = MockDatabaseSqlLite.CreateDbContext(principal, connection))
                {
                    dbContext.Database.EnsureCreated();

                    UserProfile userProfile = new UserProfile();
                    userProfile.UserId = securityService.GetCurrentUserid();
                    dbContext.Add(userProfile);
                    dbContext.SaveChanges();
                }

                using (OpenSportsPlatformDbContext dbContext = MockDatabaseSqlLite.CreateDbContext(principal, connection))
                {
                    ITcxFileImporterService service = new TcxFileImporterService(logger, securityService, dbContext);
                    Stream stream = File.OpenRead("Files\\testactivity.tcx");
                    await service.ImportWorkout(stream);
                }

            }
        }
    }
}
