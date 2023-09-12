using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ChallengeNET.DataAccess.Entitys
{
    public class Operacion
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int operacion_id { get; set; }
        public int tarjeta_id { get; set; }
        [ForeignKey("tarjeta_id")]
        public virtual Tarjeta Tarjeta { get; set; }
        [MaxLength(10)]
        public string cod_operacion { get; set; }
        public DateTime fecha_operacion { get; set; }
    }
}
