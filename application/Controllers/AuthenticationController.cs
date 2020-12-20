using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using OpenSportsPlatform.Lib.Dtos;
using OpenSportsPlatform.Lib.Services.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OpenSportsPlatform.Application.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthenticationController
    {

        private readonly IJwtTokenService _jwtTokenService;
        private readonly ILogger _logger;

        public AuthenticationController(ILogger<AuthenticationController> logger, IJwtTokenService jwtTokenService)
        {
            _jwtTokenService = jwtTokenService;
            _logger = logger;
        }

        [HttpPost("[action]")]
        public async Task<JsonResult> ExchangeToken([FromBody] ExchangeToken token)
        {
            _logger.LogDebug($"Exchanging token");
            string userId =  await _jwtTokenService.ValidateGoogelTokenAndGetUserId(token.IdToken);
            return new JsonResult(_jwtTokenService.GenerateJwtToken(userId));
        }

    }
}
