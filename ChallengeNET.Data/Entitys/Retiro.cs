using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ChallengeNET.DataAccess.Entitys
{
    public class Retiro
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int retiro_id { get; set; }
        public int tarjeta_id { get; set; }
        [ForeignKey("tarjeta_id")]
        public virtual Tarjeta Tarjeta { get; set; }
        public int  operacion_id { get; set; }
        [ForeignKey("operacion_id")]
        public virtual Operacion Operacion { get; set; }
        public DateTime fecha_retiro { get; set; }
        public double monto_retiro { get; set; }
    }
}
