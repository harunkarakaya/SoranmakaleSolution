using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Soranmakale.Entities
{
    [Table("AnketinCevabi")]
    public class AnketinCevabi  // Birde AnketinCevabiniSecenler sınıfı olarak o sınıfta hangi AnketinCevabi_ID si ile o cevabı seçen SecenKisi_ID olacak
    {                               // ve ona göre hangi seçenegi kim seçti ve kaç kişinin seçtiğini bulabilecez. Oy sayisi kısmı olmayacak
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        [Required,StringLength(100)]
        public string SecenekAdi { get; set; }
        public string AnketinAdi { get; set; }

        public virtual Anket Anketi { get; set; }
        public virtual List<AnketinCevabiniSecenler> Secenler { get; set; }

        public AnketinCevabi()
        {
            Secenler = new List<AnketinCevabiniSecenler>();
        }

    }
}
