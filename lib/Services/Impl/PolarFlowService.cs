using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using OpenSportsPlatform.Lib.Core;
using OpenSportsPlatform.Lib.Model.Dtos.Polar;
using OpenSportsPlatform.Lib.Services.Contract;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

namespace OpenSportsPlatform.Lib.Services.Impl
{
    public class PolarFlowService : IPolarFlowService
    {
        private readonly string _polarClientId;
        private readonly string _polarSecret;
        private readonly ILogger _logger;
        private readonly HttpClient _httpClient;

        public PolarFlowService(IConfiguration configuration, HttpClient httpClient, ILogger<PolarFlowService> logger)
        {
            _polarClientId = configuration.GetValue<string>("PolarClientID") ??
                             throw new ConfigurationException("PolarClientID");
            _polarSecret = configuration.GetValue<string>("PolarClientSecret") ??
                           throw new ConfigurationException("PolarClientSecret");
            _httpClient = httpClient;
            _logger = logger;
        }

        public async Task CommitTransaction(string userId, ulong transactionId, string accessCode)
        {
            _logger.LogInformation("Commit transaction {0} for user {1}", transactionId, userId);

            string url = $"https://www.polaraccesslink.com/v3/users/{userId}/exercise-transactions/{transactionId}";
            var request = new HttpRequestMessage(HttpMethod.Put, url);
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", accessCode);
            request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            request.Content = null; /*empty body*/

            HttpResponseMessage response = await GetHttpClient().SendAsync(request);
            if (response.StatusCode == System.Net.HttpStatusCode.NoContent)
            {
                _logger.LogWarning("Nothing to commit in transaction {0}", transactionId);
                return;
            }

            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                return;
            }

            throw new ArgumentException($"Got unexpected status-code from server. StatusCode: {response.StatusCode}");
        }

        public async Task<TransactionResponse?> CreateTransaction(string userId, string accessCode)
        {
            _logger.LogInformation("Starting transaction for user {0}", userId);

            string url = $"https://www.polaraccesslink.com/v3/users/{userId}/exercise-transactions";
            var request = new HttpRequestMessage(HttpMethod.Post, url);
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", accessCode);
            request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            request.Content = null; /*empty body*/
            HttpResponseMessage response = await GetHttpClient().SendAsync(request);
            if (response.StatusCode == System.Net.HttpStatusCode.NoContent)
            {
                return null;
            }

            if (response.StatusCode == System.Net.HttpStatusCode.Created)
            {
                string json = await response.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<TransactionResponse>(json)!;
            }

            throw new ArgumentException($"Got unexpected status-code from server. StatusCode {response.StatusCode}");
        }

        public async Task<AccessTokenResponse> GetAuthToken(string code)
        {
            _logger.LogInformation("Getting auth token");
            var request = new HttpRequestMessage(HttpMethod.Post, "https://polarremote.com/v2/oauth2/token");
            string authorizationValue = GetAuthorizationHeader(_polarClientId, _polarSecret);
            request.Headers.Authorization = new AuthenticationHeaderValue("Basic", authorizationValue);
            request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            IDictionary<string, string> formValues = new Dictionary<string, string>();
            formValues["grant_type"] = "authorization_code";
            formValues["code"] = code;
            request.Content = new FormUrlEncodedContent(formValues);
            HttpResponseMessage response = await GetHttpClient().SendAsync(request);
            if (!response.IsSuccessStatusCode)
            {
                throw new ArgumentException("Error exchanging token from polar");
            }

            string json = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<AccessTokenResponse>(json)!;
        }


        public async Task<Stream> GetExerciseAsTcx(string urlToEx, string accessCode)
        {
            _logger.LogInformation("Get Exercise");
            string url = $"{urlToEx}/tcx";
            var request = new HttpRequestMessage(HttpMethod.Get, url);
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", accessCode);
            request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/vnd.garmin.tcx+xml"));
            HttpResponseMessage response = await GetHttpClient().SendAsync(request);
            if (!response.IsSuccessStatusCode)
            {
                throw new ArgumentException($"Got a 404 for url {url}");
            }

            Stream stream = await response.Content.ReadAsStreamAsync();
            return stream;
        }

        public string GetRegisterUrl()
        {
            return $"https://flow.polar.com/oauth2/authorization?response_type=code&client_id={this._polarClientId}";
        }

        public async Task<ListExercisesResponse> ListExercises(string userId, string transactionId, string accessCode)
        {
            _logger.LogInformation("ListExercises for userid {0}", userId);

            string url = $"https://www.polaraccesslink.com/v3/users/{userId}/exercise-transactions/{transactionId}";
            var request = new HttpRequestMessage(HttpMethod.Get, url);
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", accessCode);
            request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            HttpResponseMessage response = await GetHttpClient().SendAsync(request);
            string json = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<ListExercisesResponse>(json)!;
        }

        public async Task<RegisterUserResponse> RegisterUser(string userId, string accessCode)
        {
            _logger.LogInformation("Register user {0}", userId);
            string url = $"https://www.polaraccesslink.com/v3/users";
            var request = new HttpRequestMessage(HttpMethod.Post, url);
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", accessCode);
            request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            var requestObj = new RegisterUserRequest() { MemberId = userId };
            var requestJson = JsonSerializer.Serialize(requestObj);
            request.Content = new StringContent(requestJson, Encoding.UTF8, "application/json");
            HttpResponseMessage response = await GetHttpClient().SendAsync(request);
            string json = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<RegisterUserResponse>(json)!;
        }

        private string GetAuthorizationHeader(string clientId, string secret)
        {
            string combined = $"{clientId}:{secret}";
            byte[] bytes = Encoding.UTF8.GetBytes(combined);
            return Convert.ToBase64String(bytes);
        }

        /// <summary>
        /// If we have to change to IHttpClientFactory
        /// https://docs.microsoft.com/en-us/aspnet/core/fundamentals/http-requests?view=aspnetcore-5.0
        /// </summary>
        /// <returns></returns>
        private HttpClient GetHttpClient()
        {
            return _httpClient;
        }
    }
}