namespace ChallengeNET.Client.Models
{
    public class RetiroVM
    {
        public int retiro_id { get; set; }
        public virtual TarjetaVM Tarjeta { get; set; }
        public DateTime fecha_retiro { get; set; }
        public double monto_retiro { get; set; }
    }
}
