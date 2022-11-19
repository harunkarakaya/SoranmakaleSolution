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

namespace Soranmakale.WebApp.Controllers
{
    public class MakaleController : Controller
    {
        private MakaleManager mm = new MakaleManager();
        private KategoriManager km = new KategoriManager();

        public ActionResult Index(string aranan)
        {
            if (aranan == null)
            {
                List<Makale> makaleler = mm.List();
                makaleler.Reverse();

                return View(makaleler);
            }
            else
            {
                Makale makale = mm.Find(x => x.Baslik == aranan);

                if (makale == null)
                {
                    ViewBag.mesaj = "Böyle bir makale bulunmamaktadır!";

                    return View();
                }

                ViewBag.Aranan = makale;

                return View();
            }

        }

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Makale makale = mm.Find(x => x.ID == id.Value);

            if (makale == null)
            {
                return HttpNotFound();
            }
            return View(makale);
        }

        public ActionResult Create()
        {
            List<Kategori> kategoriler = km.List();

            ViewBag.KategoriID = new SelectList(kategoriler, "ID", "KategoriAdi");
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Makale makale)
        {
            List<Kategori> kategoriler = km.List();


            makale.OlusturulmaTarihi = DateTime.Now;
            makale.DuzenlenmeTarihi = DateTime.Now;
            makale.BegenilmeSayisi = 0;
            makale.GoruntulenmeSayisi = 0;
            makale.KapakFoto = "makale.png";

            if (ModelState.IsValid)
            {
                makale.Sahibi = CurrentSession.kullanici;
                mm.Insert(makale);

                return RedirectToAction("YeniIcerikler","Home");
            }

            ViewBag.KategoriID = new SelectList(kategoriler, "ID", "KategoriAdi", makale.KategoriID);
            return View(makale);
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Makale makale = mm.Find(x => x.ID == id.Value);

            if (makale == null)
            {
                return HttpNotFound();
            }

            ViewBag.KategoriID = new SelectList(km.List(), "ID", "KategoriAdi", makale.KategoriID);

            return View(makale);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Makale makale)
        {
            if (ModelState.IsValid)
            {

                Makale db_makale = mm.Find(x => x.ID == makale.ID);

                db_makale.Baslik = makale.Baslik;
                db_makale.Icerik = makale.Icerik;
                db_makale.OkunmaSuresi = makale.OkunmaSuresi;
                db_makale.KategoriID = makale.KategoriID;

                mm.Update(db_makale);

                return RedirectToAction("Profil","Home");
            }
            ViewBag.KategoriID = new SelectList(km.List(), "ID", "KategoriAdi", makale.KategoriID);

            return View(makale);
        }


        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Makale makale = mm.Find(x => x.ID == id.Value);

            if (makale == null)
            {
                return HttpNotFound();
            }
            return View(makale);
        }


        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Makale db_makale = mm.Find(x => x.ID == id);
            mm.Delete(db_makale);

            string temp = TempData["profil"] as string;

            if (temp == "profil")
            {
                return RedirectToAction("Profil", "Home");
            }

            return RedirectToAction("Index");
        }
    }
}
