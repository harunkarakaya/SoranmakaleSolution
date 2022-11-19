using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Soranmakale.Entities
{
    [Table("SorununYorumununBegenisi")]
    public class SorununYorumununBegenisi
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        public virtual SorununYorumu Yorum { get; set; }
        public virtual Kullanici BegenenKisi { get; set; }
    }
}
