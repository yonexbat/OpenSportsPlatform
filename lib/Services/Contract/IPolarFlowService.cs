﻿using OpenSportsPlatform.Lib.Model.Dtos.Polar;

namespace OpenSportsPlatform.Lib.Services.Contract;

public interface IPolarFlowService
{
    Task<AccessTokenResponse> GetAuthToken(string code);

    Task<RegisterUserResponse> RegisterUser(string userId, string accessCode);

    Task<TransactionResponse?> CreateTransaction(string userId, string accessCode);

    Task CommitTransaction(string userId, ulong transactionId, string accessCode);

    Task<ListExercisesResponse> ListExercises(string userId, string transationId, string accessCode);

    Task<Stream> GetExerciseAsTcx(string urlToEx, string accessCode);

    string GetRegisterUrl();
}