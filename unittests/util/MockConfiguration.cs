using Microsoft.Extensions.Configuration;
using Moq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace unittests.util
{
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
}
