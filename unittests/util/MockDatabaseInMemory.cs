using Microsoft.EntityFrameworkCore;
using OpenSportsPlatform.Lib.Database;
using OpenSportsPlatform.Lib.Services.Contract;
using OpenSportsPlatform.Lib.Services.Impl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace unittests.util
{
    public class MockDatabaseInMemory
    {
        public static OpenSportsPlatformDbContext GetDatabase(string name, IPrincipal principal)
        {
            ISecurityService securityService = new SecurityService(principal);

            var options = new DbContextOptionsBuilder<OpenSportsPlatformDbContext>()
            .UseInMemoryDatabase(databaseName: name)
            .Options;
            return new OpenSportsPlatformDbContext(options, securityService);
        }
    }
}
