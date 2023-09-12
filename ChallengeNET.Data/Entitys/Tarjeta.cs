using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace ChallengeNET.DataAccess.Entitys
{
    public class Tarjeta
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int tarjeta_id { get; set; }
        [MaxLength(16)]
        [NotNull]
        public string nro_tarjeta { get; set; }
        [MaxLength(4)]
        [NotNull]
        public string pin_tarjeta { get; set; }
        public bool tarjeta_bloqueada { get; set; }
        public DateTime vencimiento_tarjeta { get; set; }
        public virtual Balance Balance { get; set; }
        public virtual List<Retiro> Retiros { get; set; }
        public virtual List<Operacion> Operaciones { get; set; }
    }
}
