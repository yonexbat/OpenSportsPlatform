using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using OpenSportsPlatform.Lib.Database;
using OpenSportsPlatform.Lib.Services.Contract;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace OpenSportsPlatform.Lib.Services.Impl
{
    public class DatabaseMigrationService : IDatabaseMigrationService
    {
        private readonly ILogger _logger;
        private readonly OpenSportsPlatformDbContext _dbContext;

        public DatabaseMigrationService(ILogger<DatabaseMigrationService> logger, OpenSportsPlatformDbContext dbContext)
        {
            _logger = logger;
            _dbContext = dbContext;
        }

        public async Task Migrate()
        {
            _logger.LogInformation("Migrating database");
            _dbContext.Database.Migrate();
        }
    }
}
