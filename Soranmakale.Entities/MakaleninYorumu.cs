using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Soranmakale.Entities
{
    [Table("MakaleninYorumu")]
    public class MakaleninYorumu
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        [Required,StringLength(300)]
        public string Icerik { get; set; }

        [Required] 
        public DateTime OlusturulmaTarihi { get; set; }

        [Required] 
        public DateTime DuzenlenmeTarihi { get; set; }

        [Required] 
        public string SahibiAdi { get; set; }

        public virtual Kullanici Sahibi { get; set; }
        public virtual Makale Makale { get; set; }
    }
}
