using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Soranmakale.Entities
{
    [Table("Makale")]
    public class Makale
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        [Required,StringLength(70)]
        public string Baslik { get; set; }

        [Required]
        public string Icerik { get; set; }

        public string KapakFoto { get; set; }

        [Required] 
        public DateTime OlusturulmaTarihi { get; set; }

        [Required] 
        public DateTime DuzenlenmeTarihi { get; set; }
        public int BegenilmeSayisi { get; set; }
        public int OkunmaSuresi { get; set; }
        public int GoruntulenmeSayisi { get; set; }
        public int KategoriID { get; set; }

        public virtual Kullanici Sahibi { get; set; }
        public virtual List<MakaleninYorumu> Yorumlari { get; set; }
        public virtual List<MakaleninBegenisi> Begenileri { get; set; }
        public virtual Kategori Kategori { get; set; }
        public virtual List<FavlananMakale> FavlananMak { get; set; }


        public Makale()
        {
            Yorumlari = new List<MakaleninYorumu>();
            Begenileri = new List<MakaleninBegenisi>();
            FavlananMak = new List<FavlananMakale>();
        }

    }
}
