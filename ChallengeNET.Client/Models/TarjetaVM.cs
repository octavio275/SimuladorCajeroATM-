namespace ChallengeNET.Client.Models
{
    public class TarjetaVM
    {
        public int tarjeta_id { get; set; }
        public string nro_tarjeta { get; set; }
        public string pin_tarjeta { get; set; }
        public bool tarjeta_bloqueada { get; set; }
        public DateTime vencimiento_tarjeta { get; set; }
        public List<RetiroVM> Retiros { get; set; }
        public List<OperacionesVM> Operaciones { get; set; }
    }
}
