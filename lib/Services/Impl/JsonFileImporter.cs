using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using OpenSportsPlatform.Lib.Core;
using OpenSportsPlatform.Lib.Database;
using OpenSportsPlatform.Lib.Model.Entities;
using OpenSportsPlatform.Lib.Services.Contract;

namespace OpenSportsPlatform.Lib.Services.Impl;

public class MultiFileImporterService : IMultiFileImporterService
{
    private readonly ILogger _logger;
    private readonly OpenSportsPlatformDbContext _dbContext;
    private readonly IConfiguration _configuration;
    private readonly ITcxFileImporterService _tcxFileImporterService;
    private readonly ISecurityService _securityService;

    public MultiFileImporterService(
        ILogger<MultiFileImporterService> logger,
        OpenSportsPlatformDbContext dbContext,
        ITcxFileImporterService tcxFileImporterService,
        ISecurityService securityService,
        IConfiguration configuration
    )
    {
        _logger = logger;
        _dbContext = dbContext;
        _configuration = configuration;
        _tcxFileImporterService = tcxFileImporterService;
        _securityService = securityService;
    }

    public async Task ImportFiles()
    {
        _logger.LogInformation("Importing files");
        // await _dbContext.Database.ExecuteSqlRawAsync("DELETE FROM dbo.OSPSample");
        // await _dbContext.Database.ExecuteSqlRawAsync("DELETE FROM dbo.OSPSegment");
        // await _dbContext.Database.ExecuteSqlRawAsync("DELETE FROM dbo.OSPWorkout");
        // await CreateUserIfNotExists();
        List<string> list = GetFileList();
        int index = 0;
        foreach (string file in list)
        {
            using (Stream stream = File.OpenRead(file))
            {
                await _tcxFileImporterService.ImportWorkout(stream);
                _dbContext.ChangeTracker.Clear();
            }

            index++;
        }
        await _dbContext.SaveChangesAsync();
    }


    private async Task CreateUserIfNotExists()
    {
        string userid = _securityService.GetCurrentUserid();
        // Test if user exists.
        UserProfile? user = await _dbContext
            .UserProfile
            .Where(x => x.UserId == userid)
            .FirstOrDefaultAsync();

        if (user == null)
        {
            user = new UserProfile()
            {
                UserId = userid,
            };
            await _dbContext.AddAsync(user);
        }

        await _dbContext.SaveChangesAsync();
    }


    private List<string> GetFileList()
    {
        string directory = _configuration.GetValue<string>("WorkoutFilesDirectory") 
                           ?? throw new ConfigurationException("WorkoutFilesDirectory");
        _logger.LogInformation("Workoutdirectory: {0}", directory);

        string[] fileEntries = Directory.GetFiles(directory);
        return fileEntries.Where(x => x.EndsWith(".tcx")).ToList();
    }
}