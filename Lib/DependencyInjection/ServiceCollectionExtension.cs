using Microsoft.Extensions.DependencyInjection;
using OpenSportsPlatform.Lib.Services.Contract;
using OpenSportsPlatform.Lib.Services.Impl;
using System;
using System.Collections.Generic;
using System.Text;

namespace OpenSportsPlatform.Lib.DependencyInjection
{
    public static class ServiceCollectionExtension
    {
        public static IServiceCollection AddOpenSportsPlatformServices(this IServiceCollection serviceCollection)
        {
            return serviceCollection.AddTransient<IJsonFileImporterService, JsonFileImporterService>();
        }
    }
}
