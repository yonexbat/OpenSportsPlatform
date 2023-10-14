using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using OpenSportsPlatform.Lib.Model.Dtos.Polar;
using OpenSportsPlatform.Lib.Services.Contract;
using OpenSportsPlatform.Lib.Services.Impl;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
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
            AccessTokenResponse res = await service.GetAuthToken("cadefc6c043b89efa06ede0d7ada36");

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
        public async Task DownloadTrainings()
        {
            IPolarFlowService service = CreateService();
            IConfiguration config = GetConfiguration();

            int userid = config.GetValue<int>("PolarUserId");
            string accessToken = config.GetValue<string>("PolarAccessToken");

            TransactionResponse transaction = await service.CreateTransaction(userid.ToString(), accessToken);

            if (transaction != null && transaction.TransactionId != null)
            {
                ListExercisesResponse exercises = await service.ListExercises(userid.ToString(), transaction.TransactionId.ToString(),
                    accessToken);
                foreach (var exercise in exercises.Exercises!)
                {
                    // var res = await service.GetExerciseAsGpx(exercise, accessToken);
                    await using Stream stream = await service.GetExerciseAsTcx(exercise, accessToken);
                    Guid guid = Guid.NewGuid();
                    WriteToFile(stream, $"D:\\Tmp\\{guid}.tcx");
                }

                await service.CommitTransaction(userid.ToString(), transaction.TransactionId.Value, accessToken);
            }
        }

        private IPolarFlowService CreateService()
        {
            ILogger<PolarFlowService> logger = new MockLogger<PolarFlowService>();

            IConfiguration config = GetConfiguration();
            var clientId = config.GetValue<string>("PolarClientID");
            var clientSecret = config.GetValue<string>("PolarClientSecret");

            var inMemorySettings = new Dictionary<string, string>
            {
                { "PolarClientID", clientId },
                { "PolarClientSecret", clientSecret },
            };

            IConfiguration mockConfiguration = new ConfigurationBuilder()
                .AddInMemoryCollection(inMemorySettings)
                .Build();

            HttpClient httpClient = new HttpClient();


            IPolarFlowService service = new PolarFlowService(mockConfiguration, httpClient, logger);
            return service;
        }


        private IConfiguration GetConfiguration()
        {
            var builder = new ConfigurationBuilder()
                .AddUserSecrets<PolarFlowServiceTest>();

            return builder.Build();
        }


        private static void WriteToFile(Stream stream, string destinationFile)
        {
            using Stream file = File.Create(destinationFile);
            CopyStream(stream, file);
        }

        private static void CopyStream(Stream input, Stream output)
        {
            byte[] buffer = new byte[8 * 1024];
            int len;
            while ((len = input.Read(buffer, 0, buffer.Length)) > 0)
            {
                output.Write(buffer, 0, len);
            }
        }
    }
}