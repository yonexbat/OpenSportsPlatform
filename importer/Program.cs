using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using OpenSportsPlatform.Lib.DependencyInjection;
using OpenSportsPlatform.Lib.Services.Contract;
using System;
using System.IO;

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

            // Build configuration
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetParent(AppContext.BaseDirectory).FullName)
                .AddJsonFile("appsettings.json", false)
                .AddUserSecrets<Program>()
                .Build();


            IJsonFileImporterService jsonFileImporterService = serviceProvider.GetService<IJsonFileImporterService>();
            jsonFileImporterService.ImportFiles();

            logger.LogDebug("All done!");
        }
    }
}
