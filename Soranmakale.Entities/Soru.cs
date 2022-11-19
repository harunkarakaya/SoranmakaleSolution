using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Soranmakale.Entities
{
    [Table("Soru")]
    public class Soru
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        [DisplayName("Sorunun Başlığı"),Required,StringLength(50)]
        public string Baslik { get; set; }

        [DisplayName("Sorunuz"),Required]
        public string Icerik { get; set; }

        [Required] 
        public DateTime OlusturulmaTarihi { get; set; }

        [Required] 
        public DateTime DuzenlenmeTarihi { get; set; }
        public int GoruntulenmeSayisi { get; set; }
        public int KategoriID { get; set; }

        public virtual Kullanici Sahibi { get; set; }
        public virtual List<SorununYorumu> Yorumlari { get; set; }
        public virtual Kategori Kategori { get; set; }
        public virtual List<FavlananSoru> FavlananSoru { get; set; }

        public Soru()
        {
            Yorumlari = new List<SorununYorumu>();
            FavlananSoru = new List<FavlananSoru>();
        }
    }
}
