using Google.Apis.Auth;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace OpenSportsPlatform.Lib.Services.Contract
{
    public interface IJwtTokenService
    {
        string GenerateJwtToken(string userId);

        string? ValidateJwtToken(string token);

        Task<GoogleJsonWebSignature.Payload> ValidateGoogelTokenAndGetUserId(string idToken);
    }
}
