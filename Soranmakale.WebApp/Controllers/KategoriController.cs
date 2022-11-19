using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Soranmakale.Entities;
using Soranmakale.WebApp.Models;
using Soranmakale.BusinessLayer;
using Soranmakale.Entities.ErrorMessageObject;

namespace Soranmakale.WebApp.Controllers
{
    public class KategoriController : Controller
    {
        private KategoriManager km = new KategoriManager();
        private MakaleManager mm = new MakaleManager();
        private AnketManager anm = new AnketManager();
        private SoruManager sm = new SoruManager();

        public ActionResult Index(string aranan)
        {

            if(aranan == null)
            {
                List<Kategori> kategoriler = km.List();

                return View(km.List());
            }
            else
            {
                Kategori kategori = km.Find(x => x.KategoriAdi == aranan);

                if(kategori == null)
                {
                    ViewBag.mesaj = "Böyle bir kategori bulunmamaktadır!";

                    return View();
                }

                ViewBag.Aranan = kategori;

                return View();
            }
            
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Kategori kategori)
        {
            kategori.OlusturulmaTarihi = DateTime.Now;

            if (ModelState.IsValid)
            {
                km.Insert(kategori);
                km.Save();

                return RedirectToAction("Index");
            }

            return View(kategori);
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Kategori kategori = km.Find(x => x.ID == id.Value);
            if (kategori == null)
            {
                return HttpNotFound();
            }

            return View(kategori);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Kategori kategori)
        {
            kategori.OlusturulmaTarihi = DateTime.Now;

            if (ModelState.IsValid)
            {
                Kategori db_kat = km.Find(x => x.ID == kategori.ID);
                db_kat.KategoriAdi = kategori.KategoriAdi;
                db_kat.OlusturulmaTarihi = kategori.OlusturulmaTarihi;

                km.Update(db_kat);

                return RedirectToAction("Index");
            }
            return View(kategori);
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Kategori kategori = km.Find(x => x.ID == id.Value);

            if (kategori == null)
            {
                return HttpNotFound();
            }
            return View(kategori);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            List<Makale> makaleler = mm.List();
            List<Anket> anketler = anm.List();
            List<Soru> sorular = sm.List();
            bool sil = true;

            ErrorMessageObj error = new ErrorMessageObj()
            {
                Message = "Soruları, anketleri ve makaleleri olan kategoriyi silemezsiniz!"
            };

            foreach (Makale m in makaleler)
            {
                if(m.KategoriID == id)
                {
                    sil = false;
                }
            }

            if(sil)
            {
                Kategori db_kat = km.Find(x => x.ID == id);
                km.Delete(db_kat);
            }
            else
            {
                ViewBag.Hata = "Soruları, anketleri ve makaleleri olan kategoriyi silemezsiniz!";
                return View("Uyari");
            }

            return RedirectToAction("Index");
        }
    }
}
