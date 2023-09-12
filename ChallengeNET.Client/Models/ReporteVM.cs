namespace ChallengeNET.Client.Models
{
    public class ReporteVM
    {
        public TarjetaVM Tarjeta { get; set; }
        public List<OperacionesVM> Operaciones { get; set; }
        public BalanceVM Balance { get; set; }
        public List<RetiroVM> Retiros { get; set; }
    }
}
