using Microsoft.Extensions.Logging;
using OpenSportsPlatform.Lib.Services.Contract;
using System.Threading.Tasks;

namespace OpenSportsPlatform.Lib.Services.Impl
{
    public class JsonFileImporterService : IJsonFileImporterService
    {
        private readonly ILogger _logger;

        public JsonFileImporterService(ILogger<JsonFileImporterService> logger)
        {
            _logger = logger;
        }

        public async Task ImportFiles()
        {
            _logger.LogInformation("Importing files");
        }
    }
}
