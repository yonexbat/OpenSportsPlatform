using OpenSportsPlatform.Lib.Model.Dtos.Polar;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace OpenSportsPlatform.Lib.Services.Contract
{
    public interface IPolarFlowService
    {
        Task<AccessTokenResponse> GetAuthToken(string code);

        Task<RegisterUserResponse> RegisterUser(string userId, string accessCode);

        Task<TransactionResponse> CreateTransaction(string userId, string accessCode);

        Task<ListExercisesResponse> ListExercises(string userId, string transationId);
    }
}
