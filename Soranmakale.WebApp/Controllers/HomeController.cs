using Soranmakale.BusinessLayer;
using Soranmakale.BusinessLayer.Result;
using Soranmakale.Entities;
using Soranmakale.Entities.ErrorMessageObject;
using Soranmakale.Entities.ViewModels;
using Soranmakale.WebApp.Models;
using Soranmakale.WebApp.ViewModel;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Soranmakale.WebApp.Controllers
{
    public class HomeController : Controller
    {
        private KategoriManager km = new KategoriManager();
        private MakaleManager mm = new MakaleManager();
        private SoruManager sm = new SoruManager();
        private AnketManager anm = new AnketManager();
        private KullaniciManager kum = new KullaniciManager();
        private SorununYorumuManager sym = new SorununYorumuManager();
        private MakaleninYorumuManager mym = new MakaleninYorumuManager();
        private AnketinYorumuManager aym = new AnketinYorumuManager();
        private MakaleninBegenisiManager mbm = new MakaleninBegenisiManager();
        private SorununYorumununBegenisiManager sybm = new SorununYorumununBegenisiManager();
        private FavArticleManager fmm = new FavArticleManager();
        private FavSurveyManager fam = new FavSurveyManager();
        private FavQuestionManager fsm = new FavQuestionManager();
        private AnketinCevabiniSecenlerManager acsm = new AnketinCevabiniSecenlerManager();
        private AnketinCevaplariManager acm = new AnketinCevaplariManager();

        // GET: Home
        public ActionResult Index(string soru,string anket,string makale)
        {
            ViewBag.IcerikTipi = "Yeni İçerikler";

            ViewBag.Soru = soru;
            ViewBag.Anket = anket;
            ViewBag.Makale = makale;

            return View();
        }

        public ActionResult Login()
        {
            

            return View();
        }

        [HttpPost]
        public ActionResult Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                BusinessLayerResult<Kullanici> res = kum.KullaniciGiris(model);

                if(res.Errors.Count > 0)
                {
                    res.Errors.ForEach(x => ModelState.AddModelError("", x.Message));
                    return View(model);
                }

                CurrentSession.Set<Kullanici>("giris", res.Result);
                return RedirectToAction("YeniIcerikler");
            }

            return View(model);
        }

        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Register(RegisterViewModel model)
        {
            if(ModelState.IsValid)
            {
                BusinessLayerResult<Kullanici> res = kum.KullaniciKayit(model);

                if(res.Errors.Count > 0)
                {
                    res.Errors.ForEach(x => ModelState.AddModelError("",x.Message));
                    return View(model);
                }

                CurrentSession.Set<Kullanici>("giris", res.Result);

                return View("KayitBasarili");
            }

            return View(model);
        }

        public ActionResult KayitBasarili()
        {
            return View();
        }

        public ActionResult Logout()
        {
            CurrentSession.Clear();

            return View("YeniIcerikler");
        }

        public ActionResult KategoriFiltreleme(int? id)
        {
            Kategori kategori = km.Find(x => x.ID == id.Value);

            ViewBag.Kategori = kategori;

            //if(TempData["IcerikTipi"] as string == "YeniIcerikler")
            //{
            //    return View("YeniIcerikler");
            //    /return RedirectToAction("YeniIcerikKategorisi");
            //}

            //1.Yol:

            return View("YeniIcerikler");
        }


        public ActionResult EnCokGoruntulenenler(string soru, string anket, string makale)
        {
            ViewBag.Soru = soru;
            ViewBag.Anket = anket;
            ViewBag.Makale = makale;

            return View();
        }


        public ActionResult EnCokBegenilenMakaleler()
        {
            return View();
        }


        public ActionResult YeniIcerikler(string soru, string anket, string makale)
        {
            TempData["IcerikTipi"] = "YeniIcerikler";

            ViewBag.Soru = soru;
            ViewBag.Anket = anket;
            ViewBag.Makale = makale;

            return View();
        }

        //Makaleyi görüntüleme
        public ActionResult Makale(int? id)
        {
            Makale makale = mm.IQueryableList().FirstOrDefault(x => x.ID == id);

            TempData["makale"] = makale;

            return View(makale);
        }

        [HttpPost]
        public ActionResult Makale(string yorum)
        {
            Makale makale = (Makale)TempData["makale"];

            MakaleninYorumu makalenin_yorumu = new MakaleninYorumu();

            makalenin_yorumu.Icerik = yorum;
            makalenin_yorumu.OlusturulmaTarihi = DateTime.Now;
            makalenin_yorumu.DuzenlenmeTarihi = DateTime.Now;
            makalenin_yorumu.Makale = makale;
            makalenin_yorumu.Sahibi = CurrentSession.kullanici;
            makalenin_yorumu.SahibiAdi = CurrentSession.kullanici.Ad;

            //Tempdata ' daki veri kaybolduğu için eklemiyor.
            //Çünkü 3. actionda kayboluyor bu yüzden View(makale) yerine makale actionresult'una yönlendirip tekrardan tempdatayı doldurup hemde makaleyi gösterebiliriz.
            makale.Yorumlari.Add(makalenin_yorumu);
            mm.Save();

            return View(makale);
        }

        //Soruyu görüntüleme
        public ActionResult Soru(int? id)
        {
            Soru soru = sm.IQueryableList().FirstOrDefault(x => x.ID == id.Value);

            TempData["soru"] = soru;

            return View(soru);
        }

        //Soruya yorum ekleme
        [HttpPost]
        public ActionResult Soru(string yorum)
        {
            Soru soru = (Soru)TempData["soru"];
            SorununYorumu sorunun_yorumu = new SorununYorumu();

            sorunun_yorumu.Icerik = yorum;
            sorunun_yorumu.OlusturulmaTarihi = DateTime.Now;
            sorunun_yorumu.DuzenlenmeTarihi = DateTime.Now;
            sorunun_yorumu.Soru = soru;
            sorunun_yorumu.Sahibi = CurrentSession.kullanici;
            sorunun_yorumu.SahibiAdi = CurrentSession.kullanici.Ad;

            soru.Yorumlari.Add(sorunun_yorumu);
            sm.Save();

            return View(soru);
        }

        //Anketi görüntüleme
        public ActionResult Anket(int? id)
        {
            Anket anket = anm.IQueryableList().Include("Kategori").FirstOrDefault(x => x.ID == id);

            TempData["anket"] = anket;

            return View(anket);
        }

        //Ankete yorum ekleme
        [HttpPost]
        public ActionResult Anket(string yorum)
        {
            Anket anket = (Anket)TempData["anket"];

            AnketinYorumu anketin_yorumu = new AnketinYorumu();

            anketin_yorumu.Icerik = yorum;
            anketin_yorumu.OlusturulmaTarihi = DateTime.Now;
            anketin_yorumu.DuzenlenmeTarihi = DateTime.Now;
            anketin_yorumu.Anketi = anket;
            anketin_yorumu.Sahibi = CurrentSession.kullanici;
            anketin_yorumu.SahibiAdi = CurrentSession.kullanici.Ad;

            anket.Yorumu.Add(anketin_yorumu);
            anm.Save();

            return View(anket);
        }

        //Her sayfanın ayrı controller'ı da yapılabilir ki o zaman her sayfanın kategori filtrelemesi ve kelime araması olabilir.Düşünmek lazım.
        public ActionResult ArananKelime(string aranan)
        {
            List<Makale> makaleler = mm.List();
            List<Soru> sorular = sm.List();
            List<Anket> anketler = anm.List();

            List<Makale> bulunan_makaleler = new List<Makale>();
            List<Soru> bulunan_sorular = new List<Soru>();
            List<Anket> bulunan_anketler = new List<Anket>();


            foreach (Makale m in makaleler)
            {
                if(m.Baslik == aranan)
                {
                    bulunan_makaleler.Add(m);
                }
            }

            foreach (Soru s in sorular)
            {
                if (s.Baslik == aranan)
                {
                    bulunan_sorular.Add(s);
                }
            }

            foreach (Anket a in anketler)
            {
                if (a.Baslik == aranan)
                {
                    bulunan_anketler.Add(a);
                }
            }

            ViewBag.bsorular = bulunan_sorular;
            ViewBag.banketler = bulunan_anketler;
            ViewBag.bmakaleler = bulunan_makaleler;

            return View("YeniIcerikler");
        }

        public ActionResult Profil()
        {
            BusinessLayerResult<Kullanici> res = kum.KullaniciGetir(CurrentSession.kullanici.ID);

            if(res.Errors.Count > 0)
            {
                ErrorViewModel errorBilgiObj = new ErrorViewModel()
                {
                    Icerik = "Hata Oluştı",
                    YonlendirilmeSuresi = 3000,
                    Items = res.Errors
                };

                return View("Error", errorBilgiObj);

            }

            //TempData["icerik"] = "profil";

            return View(res.Result);
        }

        public ActionResult ProfilDuzenleme()
        {
            BusinessLayerResult<Kullanici> res = kum.KullaniciGetir(CurrentSession.kullanici.ID);

            if (res.Errors.Count > 0)
            {
                ErrorViewModel errorBilgiObj = new ErrorViewModel()
                {
                    Icerik = "Hata Oluştı",
                    YonlendirilmeSuresi = 3000,
                    Items = res.Errors
                };

                return View("Error", errorBilgiObj);

            }

            return View(res.Result);
        }

        [HttpPost]
        public ActionResult ProfilDuzenleme(Kullanici model)
        {
            ModelState.Remove("KayitTarihi");

            if (ModelState.IsValid)
            {
                BusinessLayerResult<Kullanici> res = kum.ProfileGuncelleme(model);

                if (res.Errors.Count > 0)
                {
                    ErrorViewModel errorNotifyObj = new ErrorViewModel()
                    {
                        Items = res.Errors,
                        Icerik = "Profil Güncellenemedi.",
                        YonlendirilmeUrl = "/Home/ProfilDuzenleme"
                    };

                    return View("Error", errorNotifyObj);
                }

                // Profil güncellendiği için session güncellendi.
                CurrentSession.Set<Kullanici>("login", res.Result);

                return RedirectToAction("Profil");
            }

            return View(model);
        }

        public ActionResult soruYorumSilme(int yorumid,int soruid)
        {
            Soru soru = sm.Find(x => x.ID == soruid);

            SorununYorumu sorununYorumu = sym.Find(x => x.ID == yorumid);

            sym.Delete(sorununYorumu);

            return View("Soru",soru);
        }

        public ActionResult makaleYorumSilme(int yorumid, int makaleid)
        {
            Makale makale = mm.Find(x => x.ID == makaleid);

            MakaleninYorumu makaleninYorumu = mym.Find(x => x.ID == yorumid);

            mym.Delete(makaleninYorumu);

            return View("Makale", makale);
        }

        public ActionResult anketYorumSilme(int yorumid, int anketid)
        {
            Anket anket = anm.Find(x => x.ID == anketid);

            AnketinYorumu anketinYorumu = aym.Find(x => x.ID == yorumid);

            aym.Delete(anketinYorumu);

            return View("Anket", anket);
        }

        [HttpPost]
        public ActionResult MakaleBegenme(int m_id)
        {
            int sonuc = 0;

            if(CurrentSession.kullanici != null)
            {
                MakaleninBegenisi begenilenmakale = mbm.Find(
                 x => x.BegenenKisi.ID == CurrentSession.kullanici.ID && x.Makale.ID == m_id);


                if (begenilenmakale != null)
                {
                    sonuc = 1;
                }

            }
            return Json(new { result = sonuc });
        }

        [HttpPost]
        public ActionResult SetLikeState(int makaleid, bool liked)
        {
            int res = 0;

            if(CurrentSession.kullanici != null)
            {
                MakaleninBegenisi like = mbm.Find(x => x.Makale.ID == makaleid && x.BegenenKisi.ID == CurrentSession.kullanici.ID);

                Makale makale = mm.Find(x => x.ID == makaleid);

                if (like != null && liked == false)
                {
                    res = mbm.Delete(like);
                }
                else if (like == null && liked == true)
                {
                    res = mbm.Insert(new MakaleninBegenisi()
                    {
                        BegenenKisi = CurrentSession.kullanici,
                        Makale = makale
                    });
                }


                if (res > 0)
                {
                    if (liked)
                    {
                        makale.BegenilmeSayisi++;
                    }
                    else
                    {
                        makale.BegenilmeSayisi--;
                    }

                    res = mm.Update(makale);

                    return Json(new { hasError = false, errorMessage = string.Empty, result = makale.BegenilmeSayisi });
                }

                //İşlemin başarısız olma durumu
                return Json(new { hasError = true, errorMessage = "Beğenme işlemi gerçekleştirilemedi.", result = makale.BegenilmeSayisi });
            }

            return Json(new { hasError = true, errorMessage = "Bu makaleyi beğenmek için giriş yapmalısınız!" , result = liked });
            
        }


        [HttpPost]
        public ActionResult CommentLike(int[] c_id)
        {
            List<int> likedComment = new List<int>();

            if (CurrentSession.kullanici != null && c_id != null)
            {
                likedComment = sybm.IQueryableList().Where(x => x.BegenenKisi.ID == CurrentSession.kullanici.ID &&
                c_id.Contains(x.Yorum.ID)).Select(x => x.Yorum.ID).ToList();
            }

            return Json(new { result = likedComment });
        }


        [HttpPost]
        public ActionResult SetLikeCommentState(int yorumid, bool liked)
        {
            int res = 0;

            if (CurrentSession.kullanici != null)
            {
                SorununYorumununBegenisi like = sybm.Find(x => x.Yorum.ID == yorumid && x.BegenenKisi.ID == CurrentSession.kullanici.ID);

                SorununYorumu sorununYorumu = sym.Find(x => x.ID == yorumid);

                if (like != null && liked == false)
                {
                    res = sybm.Delete(like);
                }
                else if (like == null && liked == true)
                {
                    res = sybm.Insert(new SorununYorumununBegenisi()
                    {
                        BegenenKisi = CurrentSession.kullanici,
                        Yorum = sorununYorumu
                    });
                }


                if (res > 0)
                {
                    if (liked)
                    {
                        sorununYorumu.BegenilmeSayisi++;
                    }
                    else
                    {
                        sorununYorumu.BegenilmeSayisi--;
                    }

                    res = sym.Update(sorununYorumu);

                    return Json(new { hasError = false, errorMessage = string.Empty, result = sorununYorumu.BegenilmeSayisi });
                }

                //İşlemin başarısız olma durumu
                return Json(new { hasError = true, errorMessage = "Beğenme işlemi gerçekleştirilemedi.", result = sorununYorumu.BegenilmeSayisi });
            }

            return Json(new { hasError = true, errorMessage = "Bu yorumu beğenmek için giriş yapmalısınız!", result = liked });
        }


        public ActionResult Favoriler()
        {
            BusinessLayerResult<Kullanici> res = kum.KullaniciGetir(CurrentSession.kullanici.ID);

            if (res.Errors.Count > 0)
            {
                ErrorViewModel errorBilgiObj = new ErrorViewModel()
                {
                    Icerik = "Hata Oluştı",
                    YonlendirilmeSuresi = 3000,
                    Items = res.Errors
                };

                return View("Error", errorBilgiObj);

            }

            //TempData["icerik"] = "favoriler";

            return View(res.Result);
        }

        [HttpPost]
        public ActionResult MakaleFavlama(int m_id)
        {
            int sonuc = 0;

            if (CurrentSession.kullanici != null)
            {
                FavlananMakale favlanan_makale = fmm.Find(x => x.FavKisi.ID == CurrentSession.kullanici.ID && x.Makale.ID == m_id);

                if (favlanan_makale != null)
                {
                    sonuc = 1;
                }
            }
            return Json(new { result = sonuc });
        }

        [HttpPost]
        public ActionResult SetMakaleFavlama(int favmakid, bool fav)
        {
            int res = 0;

            if (CurrentSession.kullanici != null)
            {
                FavlananMakale favlanan_makale = fmm.Find(x => x.Makale.ID == favmakid && x.FavKisi.ID == CurrentSession.kullanici.ID);

                Makale makale = mm.Find(x => x.ID == favmakid);

                if (favlanan_makale != null && fav == false)
                {
                    res = fmm.Delete(favlanan_makale);
                }
                else if (favlanan_makale == null && fav == true)
                {
                    res = fmm.Insert(new FavlananMakale()
                    {
                        FavKisi = CurrentSession.kullanici,
                        Makale = makale
                    });
                }


                if (res > 0)
                {
                    return Json(new { hasError = false, errorMessage = string.Empty, result = favlanan_makale });
                }

                //İşlemin başarısız olma durumu
                return Json(new { hasError = true, errorMessage = "Fav işlemi gerçekleştirilemedi.", result = favlanan_makale });
            }

            return Json(new { hasError = true, errorMessage = "Bu makaleyi favorilere eklemek için giriş yapmalısınız!", result = fav });
        }


        [HttpPost]
        public ActionResult SoruFavlama(int s_id)
        {
            int sonuc = 0;

            if (CurrentSession.kullanici != null)
            {
                FavlananSoru favlanan_soru = fsm.Find(x => x.FavKisi.ID == CurrentSession.kullanici.ID && x.Soru.ID == s_id);

                if (favlanan_soru != null)
                {
                    sonuc = 1;
                }
            }
            return Json(new { result = sonuc });
        }

        [HttpPost]
        public ActionResult SetSoruFavlama(int favsoruid, bool fav)
        {

            int res = 0;

            if (CurrentSession.kullanici != null)
            {
                FavlananSoru favlanan_soru = fsm.Find(x => x.Soru.ID == favsoruid && x.FavKisi.ID == CurrentSession.kullanici.ID);

                Soru soru = sm.Find(x => x.ID == favsoruid);

                if (favlanan_soru != null && fav == false)
                {
                    res = fsm.Delete(favlanan_soru);
                }
                else if (favlanan_soru == null && fav == true)
                {
                    res = fsm.Insert(new FavlananSoru()
                    {
                        FavKisi = CurrentSession.kullanici,
                        Soru = soru
                    });
                }


                if (res > 0)
                {
                    return Json(new { hasError = false, errorMessage = string.Empty, result = favlanan_soru });
                }

                //İşlemin başarısız olma durumu
                return Json(new { hasError = true, errorMessage = "Fav işlemi gerçekleştirilemedi.", result = favlanan_soru });
            }

            return Json(new { hasError = true, errorMessage = "Bu soruyu favorilere eklemek için giriş yapmalısınız!", result = fav });
        }


        [HttpPost]
        public ActionResult AnketFavlama(int a_id)
        {
            int sonuc = 0;

            if (CurrentSession.kullanici != null)
            {
                FavlananAnket favlanan_anket = fam.Find(x => x.FavKisi.ID == CurrentSession.kullanici.ID && x.Anket.ID == a_id);

                if (favlanan_anket != null)
                {
                    sonuc = 1;
                }
            }
            return Json(new { result = sonuc });
        }

        [HttpPost]
        public ActionResult SetAnketFavlama(int favanketid, bool fav)
        {
            int res = 0;

            if (CurrentSession.kullanici != null)
            {
                FavlananAnket favlanan_anket = fam.Find(x => x.Anket.ID == favanketid && x.FavKisi.ID == CurrentSession.kullanici.ID);

                Anket anket = anm.Find(x => x.ID == favanketid);

                if (favlanan_anket != null && fav == false)
                {
                    res = fam.Delete(favlanan_anket);
                }
                else if (favlanan_anket == null && fav == true)
                {
                    res = fam.Insert(new FavlananAnket()
                    {
                        FavKisi = CurrentSession.kullanici,
                        Anket = anket
                    });
                }

                if (res > 0)
                {
                    return Json(new { hasError = false, errorMessage = string.Empty, result = favlanan_anket });
                }

                //İşlemin başarısız olma durumu
                return Json(new { hasError = true, errorMessage = "Fav işlemi gerçekleştirilemedi.", result = favlanan_anket });
            }

            return Json(new { hasError = true, errorMessage = "Bu anketi favorilere eklemek için giriş yapmalısınız!", result = fav });
        }

        [HttpPost]
        public ActionResult AnketOyVerme(int anket_secenek_id,int anket_id,bool oy_durumu)
        {

            int res = 0;

            if(CurrentSession.kullanici != null)
            {
                //O anki anketi elde ediyoruz.
                Anket anket = anm.Find(x => x.ID == anket_id);

                //O anki tıklanan anketin seçeneğini elde ediyoruz.
                AnketinCevabi anketin_cevabi = acm.Find(x => x.ID == anket_secenek_id);


                //O anki anket için oy kullanan tüm kullanıcılar tutulur.
                List<AnketinCevabiniSecenler> ankete_oy_verenler = acsm.IQueryableList().Where(x => x.Anket.ID == anket_id).ToList();

                //O anki seçeneğe oy veren tüm kullanıcılar tutulur.
                List<AnketinCevabiniSecenler> anketin_cevabini_secenler = acsm.IQueryableList().Where(x => x.AnketinCevabi.ID == anket_secenek_id).ToList();


                //Ankete oy verme durumu kontrol ediliyor
                foreach (AnketinCevabiniSecenler item in anketin_cevabini_secenler)
                {
                    //Eğer oy verdiği şıkka tıklıyorsa oyu geri alacak.
                    if(item.SecenKisi.ID == CurrentSession.kullanici.ID)
                    {
                        AnketinCevabiniSecenler silinecek_oy = acsm.Find(x => x.ID == item.ID );
                        res = acsm.Delete(silinecek_oy);

                        if(res > 0)
                        {
                            return Json(new { hasError = false, errorMessage = "Oyunuz geri alındı.", result = res });
                        }
                        else
                        {
                            return Json(new { hasError = true, errorMessage = "Oyunuzu geri alınamadı.", result = res });
                        }
                    }
                }

                foreach (AnketinCevabiniSecenler item in ankete_oy_verenler)
                {
                    //Koşul doğru olursa o anki kullanıcı bu anketn için oy kullanmıştır.
                    if (item.SecenKisi.ID == CurrentSession.kullanici.ID)
                    {
                        //Bu anket için daha önce oy kullanılmıştır.
                        return Json(new { hasError = false, errorMessage = "Bu anket için daha önce oy kullanmışsınız.", result = 0 });
                    }
                }
                    
                

                if(oy_durumu == false)
                {
                    res = acsm.Insert(new AnketinCevabiniSecenler()
                    {
                        Anket = anket,
                        AnketinCevabi = anketin_cevabi,
                        KisininAdi = CurrentSession.kullanici.Ad,
                        SecenKisi = CurrentSession.kullanici,
                        SeceneginAdi = anketin_cevabi.SecenekAdi
                    });

                    if(res > 0)
                    {
                        return Json(new { hasError = false, errorMessage = "Tebrikler bu şık için oy kullandınız.", result = res });
                    }
                    else
                    {
                        return Json(new { hasError = true, errorMessage = "Tebrikler bu şık için oy kullandınız kısmı gerçekleştirilemedi.", result = res });
                    }

                    
                }
            }

            return Json(new { hasError = true, errorMessage = "Bu ankete oy vermek için giriş yapmalısınız!", result = 0 });
        }
    }
}