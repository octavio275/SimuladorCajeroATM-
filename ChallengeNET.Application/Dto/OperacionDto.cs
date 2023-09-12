namespace ChallengeNET.Application.Dto
{
    public class OperacionDto
    {
        public OperacionDto() 
        {
            Retiros = new List<RetiroDto>();
        }
        public int operacion_id { get; set; }
        public virtual TarjetaDto Tarjeta { get; set; }
        public string cod_operacion { get; set; }
        public DateTime fecha_operacion { get; set; }
        public List<RetiroDto> Retiros { get; set; }
        public virtual BalanceDto Balance { get; set; }
    }
}
