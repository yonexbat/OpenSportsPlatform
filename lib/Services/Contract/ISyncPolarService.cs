using OpenSportsPlatform.Lib.Model.Dtos.PolarOsp;

namespace OpenSportsPlatform.Lib.Services.Contract
{
    public interface ISyncPolarService
    {
        Task SyncPolar();

        Task ExchangeToken(PolarExchangeTokenDto dto);

        Task<PolarRegisterDto> RegisterData();
    }
}
