using OpenSportsPlatform.Lib.Model.Dtos;

namespace OpenSportsPlatform.Lib.Services.Contract;

public interface IUserProfileService
{
    Task<ShortUserProfileDto> GetShortUserProfile();

    Task<string> ExchangeToken(ExchangeTokenDto token);
}