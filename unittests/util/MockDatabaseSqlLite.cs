﻿using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using OpenSportsPlatform.Lib.Database;
using OpenSportsPlatform.Lib.Services.Contract;
using OpenSportsPlatform.Lib.Services.Impl;
using System.Data.Common;
using System.Security.Principal;

namespace unittests.util;

public static class MockDatabaseSqlLite
{
    public static OpenSportsPlatformDbContext CreateDbContext(IPrincipal principal, DbConnection connection)
    {
        ISecurityService securityService = new SecurityService(principal);

        var options = new DbContextOptionsBuilder<OpenSportsPlatformDbContext>()
            .UseSqlite(connection, x => x.UseNetTopologySuite())
            .Options;

        return new OpenSportsPlatformDbContext(options, securityService);
    }



    public static DbConnection CreateInMemoryDatabase()
    {
        var connection = new SqliteConnection("Filename=:memory:");
        connection.Open();
        return connection;
    }
}