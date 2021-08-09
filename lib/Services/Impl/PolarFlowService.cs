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
        
        public async Task<AccessTokenResult> GetAuthToken(string code)
        {
            _logger.LogInformation("Getting auth token");
            using (HttpClient client = new HttpClient())
            {
                string authorizationValue = GetAuthorizationHeader(PolarClientId, PolarSecret);
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", authorizationValue);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                //client.DefaultRequestHeaders.ContentType

                IDictionary<string, string> formvalues = new Dictionary<string, string>();
                formvalues["grant_type"] = "authorization_code";
                formvalues["code"] = code;

                HttpResponseMessage response = await client.PostAsync("https://polarremote.com/v2/oauth2/token", new FormUrlEncodedContent(formvalues));
                string json = await response.Content.ReadAsStringAsync();

                return JsonSerializer.Deserialize<AccessTokenResult>(json);
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
