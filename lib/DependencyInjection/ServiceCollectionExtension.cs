using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OpenSportsPlatform.Lib.Database;
using OpenSportsPlatform.Lib.Services.Contract;
using OpenSportsPlatform.Lib.Services.Impl;
using System;
using System.Collections.Generic;
using System.Text;

namespace OpenSportsPlatform.Lib.DependencyInjection
{
    public static class ServiceCollectionExtension
    {
        public static IServiceCollection AddOpenSportsPlatformServices(this IServiceCollection serviceCollection, IConfiguration configuration)
        {
            string connectionString = configuration.GetConnectionString("DefaultConnection");

            serviceCollection.AddHttpClient<IPolarFlowService, PolarFlowService>()
                    .SetHandlerLifetime(TimeSpan.FromMinutes(5));

            return serviceCollection
                .AddSingleton<IJwtTokenService, JwtTokenService>()
                .AddSingleton<ICurrentDateTimeService, CurrentDateTimeService>()
                .AddScoped<ISecurityService, SecurityService>()
                .AddScoped<IMultiFileImporterService, MultiFileImporterService>()
                .AddScoped<IUserProfileService, UserProfileService>()
                .AddScoped<IWorkoutOverviewService, WorkoutOverviewService>()
                .AddScoped<ITcxFileImporterService, TcxFileImporterService>()
                .AddScoped<IWorkoutService, WorkoutService>()
                .AddScoped<IStatisticsService, StatisticsService>()
                .AddScoped<ISyncPolarService, SyncPolarService>()
                .AddDbContext<OpenSportsPlatformDbContext>(options =>
                options.UseSqlServer(connectionString, x => x.UseNetTopologySuite()));
        }
    }
}
