using Microsoft.EntityFrameworkCore;
using OpenSportsPlatform.Lib.Database;
using OpenSportsPlatform.Lib.Services.Contract;
using OpenSportsPlatform.Lib.Services.Impl;
using System.Security.Principal;

namespace unittests.util;

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