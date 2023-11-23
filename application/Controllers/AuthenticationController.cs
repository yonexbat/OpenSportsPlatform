using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OpenSportsPlatform.Lib.Model.Dtos;
using OpenSportsPlatform.Lib.Services.Contract;

namespace OpenSportsPlatform.Application.Controllers;

[ApiController]
[Route("[controller]")]
[Authorize]
public class AuthenticationController
{

    private readonly IUserProfileService _userProfileService;
    private readonly ILogger _logger;

    public AuthenticationController(ILogger<AuthenticationController> logger,
        IUserProfileService userProfileService)
    {
        _logger = logger;
        _userProfileService = userProfileService;
    }

    [HttpPost("[action]")]
    [AllowAnonymous]
    public async Task<JsonResult> ExchangeToken([FromBody] ExchangeTokenDto token)
    {
        string authToken = await _userProfileService.ExchangeToken(token);
        return new JsonResult(authToken);
    }

    [HttpGet("[action]")]
    [AllowAnonymous]
    public async Task<ShortUserProfileDto> GetShortUserProfile()
    {
        return await _userProfileService.GetShortUserProfile();
    }

}