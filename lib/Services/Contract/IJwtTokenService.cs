using Google.Apis.Auth;

namespace OpenSportsPlatform.Lib.Services.Contract;

public interface IJwtTokenService
{
    string GenerateJwtToken(string userId);

    string? ValidateJwtToken(string token);

    Task<GoogleJsonWebSignature.Payload> ValidateGoogleTokenAndGetUserId(string idToken);
}