using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using OpenSportsPlatform.Lib.DependencyInjection;
using OpenSportsPlatform.Lib.Model;
using OpenSportsPlatform.Lib.Services.Contract;
using System;
using System.IO;
using System.Security.Principal;
using System.Threading.Tasks;

namespace OpenSportsPlatform.Importer
{
    class Program
    {
        static async Task Main(string[] args)
        {
            // Build configuration
            IConfiguration configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetParent(AppContext.BaseDirectory).FullName)
                .AddJsonFile("appsettings.json", false)
                .AddUserSecrets<Program>()
                .Build();

            // Principal
            GenericIdentity identity = new GenericIdentity("importjob");
            string[] rolenames = { Role.Admin.ToString() };
            IPrincipal principal = new GenericPrincipal(identity, rolenames);

            //setup our DI
            ServiceProvider serviceProvider = new ServiceCollection()
                .AddLogging(configure => configure.AddConsole())
                .AddSingleton<IConfiguration>(configuration)
                .AddSingleton<IPrincipal>(principal)
                .AddOpenSportsPlatformServices(configuration)                 
                .BuildServiceProvider();


            ILogger logger = serviceProvider.GetService<ILoggerFactory>()
                .CreateLogger<Program>();
            logger.LogDebug("Starting importer");

            IMultiFileImporterService jsonFileImporterService = serviceProvider.GetService<IMultiFileImporterService>();
            await jsonFileImporterService.ImportFiles();

            logger.LogDebug("All done!");
        }
    }
}
