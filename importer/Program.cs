using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using OpenSportsPlatform.Lib.DependencyInjection;
using OpenSportsPlatform.Lib.Services.Contract;

namespace OpenSportsPlatform.Importer
{
    class Program
    {
        static void Main(string[] args)
        {
            //setup our DI
            ServiceProvider serviceProvider = new ServiceCollection()
                .AddLogging(configure => configure.AddConsole())
                .AddOpenSportsPlatformServices()               
                .BuildServiceProvider();


            ILogger logger = serviceProvider.GetService<ILoggerFactory>()
                .CreateLogger<Program>();
            logger.LogDebug("Starting importer");

            IJsonFileImporterService jsonFileImporterService = serviceProvider.GetService<IJsonFileImporterService>();
            jsonFileImporterService.ImportFiles();

            logger.LogDebug("All done!");
        }
    }
}
