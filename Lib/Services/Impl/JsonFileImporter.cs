using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using OpenSportsPlatform.Lib.Database;
using OpenSportsPlatform.Lib.Entities;
using OpenSportsPlatform.Lib.Services.Contract;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace OpenSportsPlatform.Lib.Services.Impl
{
    public class JsonFileImporterService : IJsonFileImporterService
    {
        private readonly ILogger _logger;
        private readonly OpenSportsPlatformDbContext _dbContext;
        private readonly IConfiguration _configuration;

        public JsonFileImporterService(
            ILogger<JsonFileImporterService> logger, 
            OpenSportsPlatformDbContext dbContext,
            IConfiguration configuration
        )
        {
            _logger = logger;
            _dbContext = dbContext;
            _configuration = configuration;
        }

        public async Task ImportFiles()
        {
            _logger.LogInformation("Importing files");
            List<string> list = GetFileList();

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

        private List<string> GetFileList()
        {
            string directory = _configuration.GetValue<string>("WorkoutFilesDirectory");
            _logger.LogInformation("Workoutdirectory: {0}", directory);

            string[] fileEntries = Directory.GetFiles(directory);
            return fileEntries.Where(x => x.EndsWith(".json")).ToList();
        }
    }
}
