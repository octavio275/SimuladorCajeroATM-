using ChallengeNET.Application.Dto;

namespace ChallengeNET.Application.Services.Retiros
{
    public interface IRetiroService
    {
        void CreateRetiro(RetiroRequestDto retiro);
        RetiroDto GetRetiro(int retiro_id);
        List<RetiroDto> GetAll();
        void DeleteRetiro(int id);
    }
}