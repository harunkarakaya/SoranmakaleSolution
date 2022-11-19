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
    public class KullaniciController : Controller
    {
        private KullaniciManager km = new KullaniciManager();

        public ActionResult Index(string aranan)
        {

            if (aranan == null)
            {
                List<Kullanici> kullanicilar = km.List();

                return View(kullanicilar);
            }
            else
            {
                Kullanici kullanici = km.Find(x => x.KullaniciAdi == aranan);

                if (kullanici == null)
                {
                    ViewBag.mesaj = "Böyle bir kullanıcı adına sahip kullanıcı bulunmamaktadır!";

                    return View();
                }

                ViewBag.Aranan = kullanici;

                return View();
            }
        }

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Kullanici kullanici = km.Find(x => x.ID == id.Value);

            if (kullanici == null)
            {
                return HttpNotFound();
            }

            return View(kullanici);
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Kullanici kullanici = km.Find(x => x.ID == id.Value);

            if (kullanici == null)
            {
                return HttpNotFound();
            }
            return View(kullanici);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Kullanici kullanici)
        {

            if (ModelState.IsValid)
            {
                Kullanici db_kullanici = km.Find(x => x.ID == kullanici.ID);

                db_kullanici.Ad = kullanici.Ad;
                db_kullanici.Soyad = kullanici.Soyad;
                db_kullanici.Sifre = kullanici.Sifre;
                db_kullanici.YazarMi = kullanici.YazarMi;
                db_kullanici.AdminMi = kullanici.AdminMi;
                db_kullanici.RutbePuani = kullanici.RutbePuani;
                db_kullanici.Email = kullanici.Email;

                km.Update(db_kullanici);

                return RedirectToAction("Index");
            }
            return View(kullanici);
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Kullanici kullanici = km.Find(x => x.ID == id.Value);

            if (kullanici == null)
            {
                return HttpNotFound();
            }

            return View(kullanici);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Kullanici kullanici = km.Find(x => x.ID == id);

            km.Delete(kullanici);

            return RedirectToAction("Index");
        }
    }
}
