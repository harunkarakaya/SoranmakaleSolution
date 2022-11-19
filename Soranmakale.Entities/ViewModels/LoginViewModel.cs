using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Soranmakale.Entities.ViewModels
{
    public class LoginViewModel
    {
        [DisplayName("Kullanıcı Adı"),
            Required(ErrorMessage = "{0} alanı boş geçilemez."),
            StringLength(20, ErrorMessage ="{0} alanı en fazla {1} karakter olmalıdır.")]
        public string KullaniciAdi { get; set; }

        [DisplayName("Şifre"),
            Required(ErrorMessage = "{0} alanı boş geçilemez."),
            StringLength(20, ErrorMessage = "{0} alanı en fazla {1} karakter olmalıdır."),
            DataType(DataType.Password)]
        public string Sifre { get; set; }
    }
}
