namespace ChallengeNET.Application.Dto
{
    public class CreateCardDto
    {
        public string nro_tarjeta { get; set; }
        public string pin_tarjeta { get; set;}
        public DateTime vencimiento_tarjeta { get; set; }
    }
}
