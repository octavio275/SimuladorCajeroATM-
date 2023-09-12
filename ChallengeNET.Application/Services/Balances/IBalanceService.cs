using ChallengeNET.Application.Dto;

namespace ChallengeNET.Application.Services.Balances
{
    public interface IBalanceService
    {
        void CreateBalance(BalanceRequestDto balanceDto);
        void UpdateExtractedBalance(BalanceRequestDto balanceDto);
        void UpdateAddedBalance(BalanceRequestDto balanceDto);
        BalanceDto GetBalance(string nro_tarjeta);
        void DeleteBalance(string nro_tarjeta);
    }
}