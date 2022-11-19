using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Soranmakale.Entities
{
    [Table("SorununYorumu")]
    public class SorununYorumu
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        [Required, StringLength(300)]
        public string Icerik { get; set; }

        public int BegenilmeSayisi { get; set; }

        [Required] 
        public DateTime OlusturulmaTarihi { get; set; }

        [Required] 
        public DateTime DuzenlenmeTarihi { get; set; }

        [Required] 
        public string SahibiAdi { get; set; }

        public virtual Kullanici Sahibi { get; set; }
        public virtual Soru Soru { get; set; }
        public virtual List<SorununYorumununBegenisi> Begenileri { get; set; }

        public SorununYorumu()
        {
            Begenileri = new List<SorununYorumununBegenisi>();
        }
    }
}
