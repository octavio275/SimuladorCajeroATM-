using Newtonsoft.Json;

namespace ChallengeNET.Application.Dto
{
    public class RetiroDto
    {
        public int retiro_id { get; set; }
        [JsonProperty(PropertyName = "Tarjeta")]
        public virtual TarjetaDto Tarjeta { get; set; }
        public virtual OperacionDto Operacion { get; set; }
        public DateTime fecha_retiro { get; set; }
        public double monto_retiro { get; set; }
    }
}
