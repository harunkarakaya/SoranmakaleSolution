using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Soranmakale.Entities
{
    [Table("Anket")]
    public class Anket
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        [Required,StringLength(50)]
        public string Baslik { get; set; }

        [Required]
        public DateTime OlusturulmaTarihi { get; set; }

        [Required]
        public DateTime SonOylamaTarihi { get; set; }

        public int KatilimSayisi { get; set; }
        public int GoruntulenmeSayisi { get; set; }
        public int KategoriID { get; set; }

        public virtual Kullanici Sahibi { get; set; }
        public virtual List<AnketinYorumu> Yorumu { get; set; }
        public virtual Kategori Kategori { get; set; }
        public virtual List<AnketinCevabi> Cevaplari { get; set; } //Anketin Şıkları
        public virtual List<FavlananAnket> FavlananAnket { get; set; }

        public Anket()
        {
            Cevaplari = new List<AnketinCevabi>();
            Yorumu = new List<AnketinYorumu>();
            FavlananAnket = new List<FavlananAnket>();
        }

    }
}
