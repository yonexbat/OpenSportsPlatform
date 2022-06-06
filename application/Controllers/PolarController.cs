using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OpenSportsPlatform.Lib.Model.Dtos.PolarOsp;
using OpenSportsPlatform.Lib.Services.Contract;

namespace OpenSportsPlatform.Application.Controllers
{

    [ApiController]
    [Route("[controller]")]
    [Authorize]
    public class PolarController
    {

        private readonly ISyncPolarService _syncPolarService;
        private readonly ILogger _logger;

        public PolarController(ISyncPolarService syncPolarService, ILogger<PolarController> logger)
        {
            _syncPolarService = syncPolarService;
            _logger = logger;
        }

        [HttpPost]
        [Route("[action]")]
        public async Task<ActionResult> ExchangeToken(PolarExchangeTokenDto dto)
        {
            await _syncPolarService.ExchangeToken(dto);
            return new NoContentResult();
        }

        [HttpGet]
        [Route("[action]")]
        public async Task<PolarRegisterDto> RegisterData()
        {
            return await _syncPolarService.RegisterData();
        }

    }
}
