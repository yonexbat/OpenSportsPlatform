using System;
using System.Collections.Generic;
using System.Text;

namespace OpenSportsPlatform.Lib.Services.Contract
{
    public interface IJwtTokenService
    {
        string GenerateJwtToken(string userId);

        string ValidateJwtToken(string token);
    }
}
