using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Soranmakale.WebApp.ViewModel
{
    public class BilgiViewModelBase<T>
    {
        public List<T> Items { get; set; }
        public string Baslik { get; set; }
        public string Icerik { get; set; }
        public bool YonlendiriliyorMu { get; set; }
        public string YonlendirilmeUrl { get; set; }
        public int YonlendirilmeSuresi { get; set; }

        public BilgiViewModelBase()
        {
            Baslik = "Yönlendiriliyorsunuz.";
            Icerik = "Geçersiz İşlem";
            YonlendiriliyorMu = true;
            YonlendirilmeUrl = "/Home/YeniIcerikler";
            YonlendirilmeSuresi = 3000;
        }

    }
}