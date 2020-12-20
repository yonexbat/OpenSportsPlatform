using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using OpenSportsPlatform.Lib.Services.Contract;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;

namespace application.middleware
{
    public class JwtMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IJwtTokenService _jwtTokenService;

        public JwtMiddleware(RequestDelegate next, IJwtTokenService jwtTokenService)
        {
            _next = next;
            _jwtTokenService = jwtTokenService;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();

            if (token != null)
            {
                string userId = _jwtTokenService.ValidateJwtToken(token);
            }

            await _next(context);
        }
    }
}
