using System.Text.Json.Serialization;

namespace OpenSportsPlatform.Lib.Model.Dtos.Polar;

public class AccessTokenResponse
{
    [JsonPropertyName("access_token")]
    public string? AccessToken { get; set; }
        
    [JsonPropertyName("x_user_id")]
    public ulong? UserId { get; set; }
        
    [JsonPropertyName("expires_in")]
    public ulong? ExpiresIn { get; set; }

    [JsonPropertyName("token_type")]
    public string? TokenType { get; set; }
}