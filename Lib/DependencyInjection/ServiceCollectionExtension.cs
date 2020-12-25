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

            return serviceCollection
                .AddSingleton<IJwtTokenService, JwtTokenService>()
                .AddScoped<ISecurityService, SecurityService>()
                .AddScoped<IJsonFileImporterService, JsonFileImporterService>()
                .AddScoped<IUserProfileService, UserProfileService>()
                .AddDbContext<OpenSportsPlatformDbContext>(options =>
                options.UseSqlServer(connectionString, x => x.UseNetTopologySuite()));
        }
    }
}
