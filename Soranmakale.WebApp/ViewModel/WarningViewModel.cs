using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Soranmakale.WebApp.ViewModel
{
    public class WarningViewModel : BilgiViewModelBase<string>
    {
        public WarningViewModel()
        {
            Icerik = "Uyarı!";
        }
    }
}