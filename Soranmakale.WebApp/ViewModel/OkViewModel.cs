using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Soranmakale.WebApp.ViewModel
{
    public class OkViewModel : BilgiViewModelBase<string>
    {
        public OkViewModel()
        {
            Icerik = "İşlem Başarılı.";
        }
    }
}