using ChallengeNET.Application.Dto;

namespace ChallengeNET.Application.Services.TarjetaService
{
    public interface ITarjetaService
    {
        void CreateCard(CreateCardDto tarjeta);
        void UpdateStateCard(CardValidDto tarjeta);
        void DeleteCard(string nro_tarjeta);
        TarjetaDto GetCardWithNumber(string nro_tarjeta);
        void ValidateCVV(ValidateCVVRequestDto request);
        List<TarjetaDto> GetAll();
    }
}