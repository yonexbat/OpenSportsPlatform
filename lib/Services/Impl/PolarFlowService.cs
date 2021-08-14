using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using OpenSportsPlatform.Lib.Model.Dtos.Polar;
using OpenSportsPlatform.Lib.Services.Contract;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace OpenSportsPlatform.Lib.Services.Impl
{
    public class PolarFlowService : IPolarFlowService
    {

        private readonly string PolarClientId;
        private readonly string PolarSecret;
        private readonly ILogger _logger;

        public PolarFlowService(IConfiguration configuration, ILogger<PolarFlowService> logger)
        {
            PolarClientId = configuration.GetValue<string>("PolarClientID");
            PolarSecret = configuration.GetValue<string>("PolarClientSecret");
            _logger = logger;
        }

        public async Task<TransactionResponse> CreateTransaction(string userId, string accessCode)
        {
            _logger.LogInformation("Starting transaction for user {0}", userId);
            using (HttpClient client = new HttpClient())
            {

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessCode);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                string url = $"https://www.polaraccesslink.com/v3/users/{userId}/exercise-transactions";
                HttpResponseMessage response = await client.PostAsync(url, null /*empty body*/);

                // no new exercises
                if(response.StatusCode == System.Net.HttpStatusCode.NoContent)
                {
                    return null;
                }
                if (response.StatusCode == System.Net.HttpStatusCode.Created)
                {
                    string json = await response.Content.ReadAsStringAsync();
                    return JsonSerializer.Deserialize<TransactionResponse>(json);
                }
                throw new ArgumentException($"Got unexpected statuscode from server. Statuscode {response.StatusCode}");
            }
        }

        public async Task<AccessTokenResponse> GetAuthToken(string code)
        {
            _logger.LogInformation("Getting auth token");
            using (HttpClient client = new HttpClient())
            {
                string authorizationValue = GetAuthorizationHeader(PolarClientId, PolarSecret);
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", authorizationValue);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                IDictionary<string, string> formvalues = new Dictionary<string, string>();
                formvalues["grant_type"] = "authorization_code";
                formvalues["code"] = code;

                HttpResponseMessage response = await client.PostAsync("https://polarremote.com/v2/oauth2/token", new FormUrlEncodedContent(formvalues));
                string json = await response.Content.ReadAsStringAsync();

                return JsonSerializer.Deserialize<AccessTokenResponse>(json);
            }
        }

        public async Task<RegisterUserResponse> RegisterUser(string userId, string accessCode)
        {
            _logger.LogInformation("Register{0}", userId);
            using (HttpClient client = new HttpClient())
            {

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessCode);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                string url = $"https://www.polaraccesslink.com/v3/users";

                var requestObj = new RegisterUserRequest() { MemberId = userId };
                var requestJson = JsonSerializer.Serialize(requestObj);

                HttpResponseMessage response = await client.PostAsync(url, new StringContent(requestJson, Encoding.UTF8, "application/json"));
                string json = await response.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<RegisterUserResponse>(json);
            }
        }

        private string GetAuthorizationHeader(string clientId, string secret)
        {
            string combined = $"{clientId}:{secret}";
            byte[] bytes = Encoding.UTF8.GetBytes(combined);
            return Convert.ToBase64String(bytes);
        }
    }
}
