using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using OpenSportsPlatform.Lib.Database;
using OpenSportsPlatform.Lib.Model;
using OpenSportsPlatform.Lib.Model.Dtos;
using OpenSportsPlatform.Lib.Model.Entities;
using OpenSportsPlatform.Lib.Services.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenSportsPlatform.Lib.Services.Impl
{

    

    public class UserProfileService : IUserProfileService
    {
        private readonly ILogger _logger;
        private readonly OpenSportsPlatformDbContext _dbContext;
        private readonly ISecurityService _securityService;
        private readonly IJwtTokenService _jwtTokenService;

        public UserProfileService(
            ILogger<UserProfileService> logger,
            ISecurityService securityService,
            IJwtTokenService jwtTokenService,
            OpenSportsPlatformDbContext dbContext)
        {
            _logger = logger;
            _securityService = securityService;
            _jwtTokenService = jwtTokenService;
            _dbContext = dbContext;
        }

        public async Task<string> ExchangeToken(ExchangeTokenDto token)
        {
            _logger.LogDebug($"Exchanging token");
            var payload = await _jwtTokenService.ValidateGoogelTokenAndGetUserId(token.IdToken ?? throw new ArgumentNullException(nameof(ExchangeTokenDto.IdToken)));

            // Test if user exists.
            UserProfile? user = await _dbContext.UserProfile.Where(x => x.UserId == payload.Email)
                .FirstOrDefaultAsync();

            if(user == null)
            {
                user = new UserProfile()
                {
                    UserId = payload.Email,                  
                };
                await _dbContext.AddAsync(user);               
            }

            user.Name = payload.Name;
            await _dbContext.SaveChangesAsync();

            return _jwtTokenService.GenerateJwtToken(payload.Email);
        }

        public async Task<ShortUserProfileDto> GetShortUserProfile()
        {
            string currentUserId = _securityService.GetCurrentUserid();
            
            var user = await _dbContext.UserProfile.Where(x => x.UserId == currentUserId)
                .Select(x => new ShortUserProfileDto()
                {
                    UserId = currentUserId,
                    Name = x.Name,
                    Authenticated = true,                   
                })
               .FirstOrDefaultAsync();

            if(user == null)
            {
                return new ShortUserProfileDto()
                {
                    Name = string.Empty,
                    Authenticated = false,
                };
            } 
            else
            {
                user.Roles = new string[] { Role.User.ToString(), };
            }

            return user;
        }
    }
}
