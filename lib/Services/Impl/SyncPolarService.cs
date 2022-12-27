using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using OpenSportsPlatform.Lib.Database;
using OpenSportsPlatform.Lib.Model.Dtos;
using OpenSportsPlatform.Lib.Model.Dtos.Polar;
using OpenSportsPlatform.Lib.Model.Dtos.PolarOsp;
using OpenSportsPlatform.Lib.Services.Contract;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace OpenSportsPlatform.Lib.Services.Impl
{
    public class SyncPolarService : ISyncPolarService
    {

        private readonly IPolarFlowService _polarFlowService;
        private readonly ISecurityService _securityService;
        private readonly OpenSportsPlatformDbContext _dbContext;
        private readonly ITcxFileImporterService _tcxFileImporterService;
        private readonly ILogger _logger;
       

        public SyncPolarService(
            OpenSportsPlatformDbContext dbContext,
            IPolarFlowService polarFlowService,
            ISecurityService securityService,
            ITcxFileImporterService tcxFileImporterService,
            ILogger<SyncPolarService> logger
            )
        {
            _polarFlowService = polarFlowService;
            _securityService = securityService;
            _logger = logger;
            _dbContext = dbContext;
            _tcxFileImporterService = tcxFileImporterService;
        }

        public async Task ExchangeToken(PolarExchangeTokenDto dto)
        {
            string userId = _securityService.GetCurrentUserid();
            _logger.LogInformation("Exchanging token for user {0}", userId);
            var userProfile = await _dbContext.UserProfile.SingleAsync(x => x.UserId == userId);
            var res = await _polarFlowService.GetAuthToken(dto.Code);
            userProfile.PolarAccessToken = res.AccessToken;
            userProfile.PolarUserId = res.UserId.ToString();
            await _dbContext.SaveChangesAsync();
        }

        public async Task<PolarRegisterDto> RegisterData()
        {            
            string url = _polarFlowService.GetRegisterUrl();
            PolarRegisterDto polarRegisterDto = new PolarRegisterDto()
            {
                Url = url,
            };
            return polarRegisterDto;
        }

        public async Task SyncPolar()
        {
            string userId = _securityService.GetCurrentUserid();
            _logger.LogInformation("Syncing polar for user {0}", userId);

            var userProfile = await _dbContext.UserProfile.SingleAsync(x => x.UserId == userId);
            string polarUser = userProfile.PolarUserId;
            string polarAccessToken = userProfile.PolarAccessToken;

            TransactionResponse? transaction = await _polarFlowService.CreateTransaction(polarUser, polarAccessToken);

            if (transaction != null && transaction.TransactionId.HasValue)
            {
                ListExercisesResponse exercises = await _polarFlowService.ListExercises(polarUser, transaction.TransactionId.Value.ToString(), polarAccessToken);
                foreach (string exercise in exercises.Exercises)
                {
                    _logger.LogInformation("Getting exercise from polar: {0}", exercise);
                    using (Stream stream = await _polarFlowService.GetExerciseAsTcx(exercise, polarAccessToken))
                    {
                        await _tcxFileImporterService.ImportWorkout(stream);
                    }
                }
                await _polarFlowService.CommitTransaction(polarUser, transaction.TransactionId.Value, polarAccessToken);
            }
            else
            {
                _logger.LogInformation("No data to sync with polar.");
            }
        }
    }
}
