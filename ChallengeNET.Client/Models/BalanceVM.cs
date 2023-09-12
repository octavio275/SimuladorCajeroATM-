namespace ChallengeNET.Client.Models
{
    public class BalanceVM
    {
        public int balance_id { get; set; }
        public virtual TarjetaVM Tarjeta { get; set; }
        public double saldo { get; set; }
    }
}
