using Microsoft.AspNetCore.Mvc;
using OpenSportsPlatform.Lib.Services.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace application.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthenticationController
    {

        private readonly IJwtTokenService _jwtTokenService;

        public AuthenticationController(IJwtTokenService jwtTokenService)
        {
            _jwtTokenService = jwtTokenService;
        }

        [HttpGet("[action]")]
        public JsonResult JwtToken()
        {
           return new JsonResult(_jwtTokenService.GenerateJwtToken("hello@gmail.com"));
        }
    }
}
