﻿using Google.Apis.Auth;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using OpenSportsPlatform.Lib.Core;
using OpenSportsPlatform.Lib.Services.Contract;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace OpenSportsPlatform.Lib.Services.Impl;

public class JwtTokenService : IJwtTokenService
{
    private readonly ILogger _logger;
    private readonly IConfiguration _configuration;

    public JwtTokenService(
        ILogger<JwtTokenService> logger,
        IConfiguration configuration)
    {
        _logger = logger;
        _configuration = configuration;
    }

    public string GenerateJwtToken(string userId)
    {
        var issuer = this._configuration.GetValue<string>("jwtIssuer");

        var securityKey = new SymmetricSecurityKey(GetSecretKey());
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        //Create a List of Claims, Keep claims name short    
        var permClaims = new List<Claim>();
        permClaims.Add(new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()));
        permClaims.Add(new Claim("userid", userId));
        permClaims.Add(new Claim(ClaimTypes.Name, userId));
            

        //Create Security Token object by giving required parameters    
        var token = new JwtSecurityToken(issuer,   
            issuer,  //Audience    
            permClaims,
            expires: DateTime.Now.AddDays(100),
            signingCredentials: credentials);
        var jwt_token = new JwtSecurityTokenHandler().WriteToken(token);
        return jwt_token;
    }

    public async Task<GoogleJsonWebSignature.Payload> ValidateGoogleTokenAndGetUserId(string token)
    {
        string googleClientId = _configuration.GetValue<string>("googleClientId") ?? throw new ConfigurationException("googleClientId");
        GoogleJsonWebSignature.ValidationSettings settings = new GoogleJsonWebSignature.ValidationSettings();
        settings.Audience = new List<string>() { googleClientId };
        settings.ExpirationTimeClockTolerance = new TimeSpan(1, 0, 0);
        settings.IssuedAtClockTolerance = new TimeSpan(1, 0, 0);
        GoogleJsonWebSignature.Payload payload = await GoogleJsonWebSignature.ValidateAsync(token, settings);
        return payload;
    }

    public string? ValidateJwtToken(string token)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        byte[] key = GetSecretKey();
        try
        {
            tokenHandler.ValidateToken(token, new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateIssuer = false,
                ValidateAudience = false,

                // set clockskew to zero so tokens expire exactly at token expiration time (instead of 5 minutes later)
                ClockSkew = TimeSpan.Zero
            }, out SecurityToken validatedToken);

            JwtSecurityToken jwtToken = (JwtSecurityToken)validatedToken;
            var userId = jwtToken.Claims.First(x => x.Type == "userid").Value;

            return userId;
        }
        catch(Exception ex)
        {
            _logger.LogError("Exception. {ex}", ex);
            return null;
        }
    }

    private byte[] GetSecretKey()
    {
        string secret = _configuration.GetValue<string>("jwtSecret") ?? throw new ConfigurationException("jwtSecret");
        var key = Encoding.ASCII.GetBytes(secret);
        return key;
    }
}