using ChallengeNET.Application.Enum;

namespace ChallengeNET.Application.Dto
{
    public class OperacionRequestDto
    {
        public string nro_tarjeta { get; set; }
        public CodigoOperacionEnum cod_operacion { get; set; }
        public DateTime fecha_operacion { get; set; }
    }
}
