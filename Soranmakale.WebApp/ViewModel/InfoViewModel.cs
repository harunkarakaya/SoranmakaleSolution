using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Soranmakale.WebApp.ViewModel
{
    public class InfoViewModel : BilgiViewModelBase<string>
    {
        public InfoViewModel()
        {
            Icerik = "Bilgilendirme";
        }
    }
}