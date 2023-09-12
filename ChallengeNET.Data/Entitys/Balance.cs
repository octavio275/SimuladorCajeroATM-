using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ChallengeNET.DataAccess.Entitys
{
    public class Balance
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int balance_id { get; set; }
        public int tarjeta_id { get; set; }
        [ForeignKey("tarjeta_id")]
        public virtual Tarjeta Tarjeta { get; set; }
        public double saldo { get; set; }
    }
}
