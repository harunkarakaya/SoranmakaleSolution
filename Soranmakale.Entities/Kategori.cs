using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Soranmakale.Entities
{
    [Table("Kategori")]
    public class Kategori
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        [Required,StringLength(30)]
        public string KategoriAdi { get; set; }

        [Required]
        public DateTime OlusturulmaTarihi { get; set; }

        public string Icon { get; set; }

        public virtual List<Makale> Makaleleri { get; set; }
        public virtual List<Soru> Sorulari { get; set; }
        public virtual List<Anket> Anketleri { get; set; }

        public Kategori()
        {
            Makaleleri = new List<Makale>();
            Sorulari = new List<Soru>();
            Anketleri = new List<Anket>();
        }
    }
}
