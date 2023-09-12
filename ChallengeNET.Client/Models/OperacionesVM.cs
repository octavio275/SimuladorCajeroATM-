namespace ChallengeNET.Client.Models
{
    public class OperacionesVM
    {
        public OperacionesVM() 
        {
            Retiros = new List<RetiroVM>();
        }
        public int operacion_id { get; set; }
        public virtual TarjetaVM Tarjeta { get; set; }
        public virtual List<RetiroVM> Retiros { get; set; }
        public virtual BalanceVM Balance { get; set; }
        public string cod_operacion { get; set; }
        public DateTime fecha_operacion { get; set; }
    }
}
