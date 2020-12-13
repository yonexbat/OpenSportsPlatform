using Microsoft.Extensions.Logging;
using OpenSportsPlatform.Lib.Database;
using OpenSportsPlatform.Lib.Entities;
using OpenSportsPlatform.Lib.Services.Contract;
using System.Threading.Tasks;

namespace OpenSportsPlatform.Lib.Services.Impl
{
    public class JsonFileImporterService : IJsonFileImporterService
    {
        private readonly ILogger _logger;
        private readonly OpenSportsPlatformDbContext _dbContext;

        public JsonFileImporterService(
            ILogger<JsonFileImporterService> logger, 
            OpenSportsPlatformDbContext dbContext
        )
        {
            _logger = logger;
            _dbContext = dbContext;
        }

        public async Task ImportFiles()
        {
            _logger.LogInformation("Importing files");
            SportsCategory sportsCategory = new SportsCategory()
            {
                Name = "Test"
            };
            Workout wo = new Workout()
            {
                Name = "Test",
                SportsCategory = sportsCategory,
            };
            await _dbContext.AddAsync(sportsCategory);
            await _dbContext.AddAsync(wo);
            await _dbContext.SaveChangesAsync();
        }
    }
}
