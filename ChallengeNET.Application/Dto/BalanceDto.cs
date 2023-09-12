namespace ChallengeNET.Application.Dto
{
    public class BalanceDto
    {
        public int balance_id { get; set; }
        public virtual TarjetaDto Tarjeta { get; set; }
        public double saldo { get; set; }
    }
}
