using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Soranmakale.Entities
{
    [Table("FavlananMakale")]
    public class FavlananMakale
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        public virtual Kullanici FavKisi { get; set; }
        public virtual Makale Makale { get; set; }
    }
}
