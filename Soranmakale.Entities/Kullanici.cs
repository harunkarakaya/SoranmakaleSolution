using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Soranmakale.Entities
{
    [Table("Kullanici")]
    public class Kullanici
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        [Required,StringLength(20)]
        public string KullaniciAdi { get; set; }

        [Required,StringLength(20)]
        public string Sifre { get; set; }

        [Required,StringLength(70)]
        public string Email { get; set; }

        [StringLength(20)] 
        public string Ad { get; set; }

        [StringLength(20)] 
        public string Soyad { get; set; }

        public DateTime DogumTarihi { get; set; }

        [Required]
        public DateTime KayitTarihi { get; set; }

        public string ProfilFotografi { get; set; }

        public bool YazarMi { get; set; }
        public bool AdminMi { get; set; }
        public int RutbePuani { get; set; }

        public virtual List<Makale> Makaleleri { get; set; }
        public virtual List<MakaleninBegenisi> BegendigiMakaleleri { get; set; }
        public virtual List<MakaleninYorumu> MakaleYorumlari { get; set; }
        public virtual List<Soru> Sorulari { get; set; }
        public virtual List<SorununYorumu> SorularinYorumlari { get; set; }
        public virtual List<SorununYorumununBegenisi> SorularinYorumlarininBegenileri { get; set; }
        public virtual List<Anket> Anketleri { get; set; }
        public virtual List<AnketinYorumu> AnketYorumlari { get; set; }
        public virtual List<AnketinCevabiniSecenler> AnketinCevabiniSecenler { get; set; }

        public Kullanici()
        {
            Sorulari = new List<Soru>();
            Makaleleri = new List<Makale>();
            Anketleri = new List<Anket>();

            BegendigiMakaleleri = new List<MakaleninBegenisi>();
            MakaleYorumlari = new List<MakaleninYorumu>();

            SorularinYorumlari = new List<SorununYorumu>();
            SorularinYorumlarininBegenileri = new List<SorununYorumununBegenisi>();

            AnketYorumlari = new List<AnketinYorumu>();
            AnketinCevabiniSecenler = new List<AnketinCevabiniSecenler>();
        }
    }
}
