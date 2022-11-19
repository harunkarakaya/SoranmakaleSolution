using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Soranmakale.Entities
{
    [Table("AnketinCevabiniSecenler")]
    public class AnketinCevabiniSecenler
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        public string KisininAdi { get; set; }
        public string SeceneginAdi { get; set; }  

        public virtual AnketinCevabi AnketinCevabi { get; set; }
        public virtual Kullanici SecenKisi { get; set; }
        public virtual Anket Anket { get; set; }
    }
}
