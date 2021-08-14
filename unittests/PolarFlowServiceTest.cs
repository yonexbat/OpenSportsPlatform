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
            IPolarFlowService service = CreateService();
            AccessTokenResponse res = await service.GetAuthToken("b83321ebdf17f3c8e51f1615ca772fa9");

            Assert.False(string.IsNullOrWhiteSpace(res.AccessToken));
            Assert.True(res.UserId > 0);
        }

        [Fact]
        public async Task RegisterUser()
        {
            IPolarFlowService service = CreateService();
            IConfiguration config = GetConfiguration();

            int userid = config.GetValue<int>("PolarUserId");
            string accessToken = config.GetValue<string>("PolarAccessToken");

            await service.RegisterUser("yonexbat@gmail.com", accessToken);
        }

        [Fact]
        public void DeserializeUserInfoResponse()
        {
            string input = @"
{""polar-user-id"":53241045,""member-id"":""yonexbat@gmail.com"",""registration-date"":""2021-08-14T07:37:12.000Z"",""first-name"":""Claude"",""last-name"":""Glauser"",""birthdate"":""1975-01-05"",""gender"":""MALE"",""weight"":82.0,""height"":196.0,""extra-info"":[{  ""value"": ""2"",  ""index"": 0,  ""name"": ""number-of-children"" }]}";
            var res = JsonSerializer.Deserialize<RegisterUserResponse>(input);
            Assert.True(res.ExtraInfo?.Any() == true);
            Assert.NotNull(res.ExtraInfo?.First()?.Name);
        }



        [Fact]
        public async Task DownloadTraining()
        {
            IPolarFlowService service = CreateService();
            IConfiguration config = GetConfiguration();

            int userid = config.GetValue<int>("PolarUserId");
            string accessToken = config.GetValue<string>("PolarAccessToken");

            TransactionResponse transaction = await service.CreateTransaction(userid.ToString(), accessToken);
            if(transaction != null)
            {
                var exercises = await service.ListExercises(userid.ToString(), transaction.TransactionId.ToString());
            }
        }

        private IPolarFlowService CreateService()
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
            return service;
        }


        
        private IConfiguration GetConfiguration()
        {
            var builder = new ConfigurationBuilder()
                .AddUserSecrets<PolarFlowServiceTest>();

            return builder.Build();
        }


    }
}
