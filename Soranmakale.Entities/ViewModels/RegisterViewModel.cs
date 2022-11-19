using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Soranmakale.Entities.ViewModels
{
    public class RegisterViewModel
    {
        [DisplayName("Kullanıcı Adı"),
            Required(ErrorMessage = "{0} alanı boş geçilemez."),
            StringLength(20, ErrorMessage ="{0} alanı en fazla {1} karakter olmalıdır.")]
        public string KullaniciAdi { get; set; }

        [DisplayName("Şifre"),
            Required(ErrorMessage = "{0} alanı boş geçilemez."),
            StringLength(20, ErrorMessage = "{0} alanı en fazla {1} karakter olmalıdır.")]
        public string Sifre { get; set; }

        [DisplayName("Şifre Tekrar"),
            Required(ErrorMessage = "{0} alanı boş geçilemez."),
            StringLength(20, ErrorMessage = "{0} alanı en fazla {1} karakter olmalıdır."),
            Compare("Sifre",ErrorMessage ="{1} ile {0} uyuşmuyor.")]

        public string SifreTekrar { get; set; }

        [DisplayName("E-posta"),
            Required(ErrorMessage = "{0} alanı boş geçilemez."),
            StringLength(70, ErrorMessage = "{0} alanı en fazla {1} karakter olmalıdır."),
            EmailAddress(ErrorMessage ="{0} alanı için geçerli bir e-posta adresi giriniz.")]
        public string Eposta { get; set; }
    }
}
