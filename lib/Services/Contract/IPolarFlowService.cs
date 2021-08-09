using OpenSportsPlatform.Lib.Model.Dtos.Polar;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace OpenSportsPlatform.Lib.Services.Contract
{
    public interface IPolarFlowService
    {
        Task<AccessTokenResult> GetAuthToken(string code);
    }
}
