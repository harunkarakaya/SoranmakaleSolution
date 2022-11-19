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
    public class AnketController : Controller
    {
        private AnketManager anm = new AnketManager();
        private KategoriManager km = new KategoriManager();
        private AnketinCevaplariManager acm = new AnketinCevaplariManager();

        public ActionResult Index(string aranan)
        {
            if (aranan == null)
            {
                List<Anket> anketler = anm.List();
                anketler.Reverse();

                return View(anketler);
            }
            else
            {
                Anket anket = anm.Find(x => x.Baslik == aranan);

                if (anket == null)
                {
                    ViewBag.mesaj = "Böyle bir anket bulunmamaktadır!";

                    return View();
                }

                ViewBag.Aranan = anket;

                return View();
            }

        }

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Anket anket = anm.Find(x => x.ID == id.Value);

            if (anket == null)
            {
                return HttpNotFound();
            }
            return View(anket);
        }

        public ActionResult Create()
        {
            List<Kategori> kategoriler = km.List();

            ViewBag.KategoriID = new SelectList(kategoriler, "ID", "KategoriAdi");
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Anket anket,string secenek1, string secenek2, string secenek3, string secenek4)
        {
            List<Kategori> kategoriler = km.List();

            string[] secenekler = new string[4];

            int bosSecenek = 0;


            secenekler[0] = secenek1;
            secenekler[1] = secenek2;
            secenekler[2] = secenek3;
            secenekler[3] = secenek4;

            for (int j = 0; j < secenekler.Length; j++)
            {
                if(secenekler[j].Length == 0)
                {
                    secenekler[j] = null;
                    bosSecenek++;
                }
            }

            for (int i = 0; i < (secenekler.Length - bosSecenek) ; i++)
            {
                AnketinCevabi ac = new AnketinCevabi()
                {
                    Anketi = anket,
                    AnketinAdi = anket.Baslik,
                    SecenekAdi = secenekler[i]

                };

                anket.Cevaplari.Add(ac);
            }



            anket.OlusturulmaTarihi = DateTime.Now;
            anket.SonOylamaTarihi = DateTime.Now.AddMonths(1);
            anket.KatilimSayisi = 0;
            anket.GoruntulenmeSayisi = 0;


            if (ModelState.IsValid)
            {
                anket.Sahibi = CurrentSession.kullanici;
                anm.Insert(anket);

                return RedirectToAction("YeniIcerikler", "Home");
            }

            ViewBag.KategoriID = new SelectList(kategoriler, "ID", "KategoriAdi", anket.KategoriID);
            return View(anket);
        }


        //public ActionResult Edit(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Anket anket = db.Ankets.Find(id);
        //    if (anket == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    ViewBag.KategoriID = new SelectList(db.Kategoris, "ID", "KategoriAdi", anket.KategoriID);
        //    return View(anket);
        //}


        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Edit(Anket anket)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.Entry(anket).State = EntityState.Modified;
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }
        //    ViewBag.KategoriID = new SelectList(db.Kategoris, "ID", "KategoriAdi", anket.KategoriID);
        //    return View(anket);
        //}


        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Anket anket = anm.Find(x => x.ID == id.Value);

            if (anket == null)
            {
                return HttpNotFound();
            }
            return View(anket);
        }


        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Anket db_anket = anm.Find(x => x.ID == id);

            List<AnketinCevabi> db_anketinCevabi = acm.IQueryableList().Where(x => x.Anketi.ID == id).ToList();

            foreach (AnketinCevabi ac in db_anketinCevabi)
            {
                acm.Delete(ac);
            }

            anm.Delete(db_anket);

            return RedirectToAction("Index");
        }

    }
}
