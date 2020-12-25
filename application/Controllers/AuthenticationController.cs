using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using OpenSportsPlatform.Lib.Model.Dtos;
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
        [AllowAnonymous]
        public async Task<JsonResult> ExchangeToken([FromBody] ExchangeTokenDto token)
        {
            _logger.LogDebug($"Exchanging token");
            string userId =  await _jwtTokenService.ValidateGoogelTokenAndGetUserId(token.IdToken);
            return new JsonResult(_jwtTokenService.GenerateJwtToken(userId));
        }



    }
}
