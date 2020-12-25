using OpenSportsPlatform.Lib.Model.Dtos;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace OpenSportsPlatform.Lib.Services.Contract
{
    public interface IUserProfileService
    {
        Task<ShortUserProfileDto> GetShortUserProfile();
    }
}
