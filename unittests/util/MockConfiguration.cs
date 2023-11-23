using Microsoft.Extensions.Configuration;
using System.Collections.Generic;

namespace unittests.util;

public static class MockConfiguration
{
    public static IConfiguration GetConfiguraton(IDictionary<string, string> dict)
    {
        var configuration = new ConfigurationBuilder()
            .AddInMemoryCollection(dict)
            .Build();

        return configuration;
    }
}