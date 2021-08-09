using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using OpenSportsPlatform.Lib.Model.Dtos.Polar;
using OpenSportsPlatform.Lib.Services.Contract;
using OpenSportsPlatform.Lib.Services.Impl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using unittests.util;
using Xunit;

namespace unittests
{
    public class PolarFlowServiceTest
    {
        [Fact]
        public async Task GetAuthtokenOk()
        {
            IPrincipal principal = MockPrincipal.CreatePrincipal();
            ISecurityService securityService = new SecurityService(principal);
            ILogger<PolarFlowService> logger = new MockLogger<PolarFlowService>();

            IConfiguration config = GetConfiguration();
            var clientId = config.GetValue<string>("PolarClientID");
            var clientSecret = config.GetValue<string>("PolarClientSecret");

            var inMemorySettings = new Dictionary<string, string> {
                {"PolarClientID", clientId},
                {"PolarClientSecret", clientSecret},
            };

            IConfiguration mockConfiguration = new ConfigurationBuilder()
                .AddInMemoryCollection(inMemorySettings)
                .Build();


            IPolarFlowService service = new PolarFlowService(mockConfiguration, logger);
            var resString = await service.GetAuthToken("7c6bc931d064964a2863af3b95975a9a");

            
            
        }


        
        private IConfiguration GetConfiguration()
        {
            var builder = new ConfigurationBuilder()
                .AddUserSecrets<PolarFlowServiceTest>();

            return builder.Build();
        }


    }
}
