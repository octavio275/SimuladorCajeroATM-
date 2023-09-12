using ChallengeNET.Application.Dto;

namespace ChallengeNET.Application.Services.Operaciones
{
    public interface IOperacionService
    {
        OperacionDto CreateOperacion(OperacionRequestDto operacion);
        OperacionDto GetOperacion(int operacion_id);
        List<OperacionDto> GetAll();
        List<OperacionDto> GetAllWithCardNumber(string nro_tarjeta);
        void DeleteOperacion(int id);
    }
}