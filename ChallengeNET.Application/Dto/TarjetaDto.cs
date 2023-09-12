namespace ChallengeNET.Application.Dto
{
    public class TarjetaDto
    {
        public int tarjeta_id { get; set; }
        public double nro_tarjeta { get; set; }
        public int pin_tarjeta { get; set; }
        public bool tarjeta_bloqueada { get; set; }
        public DateTime vencimiento_tarjeta { get; set; }
    }
}
