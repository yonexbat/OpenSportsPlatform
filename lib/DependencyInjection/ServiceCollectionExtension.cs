using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OpenSportsPlatform.Lib.Core;
using OpenSportsPlatform.Lib.Database;
using OpenSportsPlatform.Lib.Services.Contract;
using OpenSportsPlatform.Lib.Services.Impl;
using System.Net;

namespace OpenSportsPlatform.Lib.DependencyInjection;

public static class ServiceCollectionExtension
{
    public static IServiceCollection AddOpenSportsPlatformServices(this IServiceCollection serviceCollection, IConfiguration configuration)
    {
        string connectionString = configuration.GetConnectionString("DefaultConnection") ?? throw new ConfigurationException("DefaultConnection");

        var httpclient = serviceCollection.AddHttpClient<IPolarFlowService, PolarFlowService>()
            .SetHandlerLifetime(TimeSpan.FromMinutes(5))
            .ConfigurePrimaryHttpMessageHandler(provider => new HttpClientHandler
            {
                AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate | DecompressionMethods.Brotli,
            });


        return serviceCollection
            .AddSingleton<IJwtTokenService, JwtTokenService>()
            .AddSingleton<ICurrentDateTimeService, CurrentDateTimeService>()
            .AddScoped<ISecurityService, SecurityService>()
            .AddScoped<IMultiFileImporterService, MultiFileImporterService>()
            .AddScoped<IUserProfileService, UserProfileService>()
            .AddScoped<ICalculateWorkoutStatisticsService, CalculateWorkoutStatisticsService>()
            .AddScoped<IWorkoutOverviewService, WorkoutOverviewService>()
            .AddScoped<ITcxFileImporterService, TcxFileImporterService>()
            .AddScoped<IWorkoutService, WorkoutService>()
            .AddScoped<IStatisticsService, StatisticsService>()
            .AddScoped<ISyncPolarService, SyncPolarService>()
            .AddScoped<ICropWorkoutService, CropWorkoutService>()
            .AddScoped<ISamplesService, SamplesService>()
            .AddScoped<IWorkoutStatisticsService, WorkoutStatisticsService>()
            .AddDbContext<OpenSportsPlatformDbContext>(options =>
                options.UseSqlServer(connectionString, builder => builder.UseNetTopologySuite().UseCompatibilityLevel(120)));
    }
}