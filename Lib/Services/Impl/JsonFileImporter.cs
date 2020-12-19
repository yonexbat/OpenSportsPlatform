using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using OpenSportsPlatform.Lib.Database;
using OpenSportsPlatform.Lib.Entities;
using OpenSportsPlatform.Lib.Services.Contract;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
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
            int index = 0;
            foreach(string file in list)
            {
                await ReadFile(file);
                if(index % 50 == 0)
                {
                    await _dbContext.SaveChangesAsync();
                    _dbContext.ChangeTracker.Clear();
                }
                index++;
            }
            await _dbContext.SaveChangesAsync();
        }

        private async Task ReadFile(string fileNameAndPath)
        {
            var stream = File.OpenRead(fileNameAndPath);
            JsonDocument document = await JsonDocument.ParseAsync(stream);
            JsonElement rooteElem = document.RootElement;
            string sport = null;
            
            foreach(JsonElement elem in rooteElem.EnumerateArray())
            {
                foreach(JsonProperty property in elem.EnumerateObject())
                {
                    _logger.LogTrace($"{property.Name}, {property.Value}");
                    if(property.Name == "sport")
                    {
                        sport = property.Value.ToString();
                    }
                }
            }


            Workout wo = new Workout();
            wo.SportsCategory = await GetSportCat(sport);
            await _dbContext.AddAsync(wo);
        }

        private async Task<SportsCategory> GetSportCat(string name)
        {
            SportsCategory sc =  await _dbContext.SportsCategory.Where(x => x.Name == name)
                .FirstOrDefaultAsync();
            if(sc == null)
            {
                sc = new SportsCategory
                {
                    Name = name,
                };
                await _dbContext.AddAsync(sc);
                await _dbContext.SaveChangesAsync();
            }
            return sc;
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
