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
    public class SoruController : Controller
    {
        private SoruManager sm = new SoruManager();
        private KategoriManager km = new KategoriManager();
        private SorununYorumuManager sym = new SorununYorumuManager();

        public ActionResult Index(string aranan)
        {

            if (aranan == null)
            {
                List<Soru> sorular = sm.List();
                sorular.Reverse();

                return View(sorular);
            }
            else
            {
                Soru soru = sm.Find(x => x.Baslik == aranan);

                if (soru == null)
                {
                    ViewBag.mesaj = "Böyle bir soru bulunmamaktadır!";

                    return View();
                }

                ViewBag.Aranan = soru;

                return View();
            }
        }

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Soru soru = sm.Find(x => x.ID == id.Value);

            if (soru == null)
            {
                return HttpNotFound();
            }
            return View(soru);
        }

        public ActionResult Create()
        {
            List<Kategori> kategoriler = km.List();

            ViewBag.KategoriID = new SelectList(kategoriler, "ID", "KategoriAdi");

            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Soru soru)
        {
            soru.OlusturulmaTarihi = DateTime.Now;
            soru.DuzenlenmeTarihi = DateTime.Now;
            soru.GoruntulenmeSayisi = 0;

            List<Kategori> kategoriler = km.List();

            if (ModelState.IsValid)
            {
                soru.Sahibi = CurrentSession.kullanici;
                sm.Insert(soru);

                return RedirectToAction("YeniIcerikler", "Home");
            }

            ViewBag.KategoriID = new SelectList(kategoriler, "ID", "KategoriAdi", soru.KategoriID);

            return View(soru);
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Soru soru = sm.Find(x => x.ID == id.Value);

            if (soru == null)
            {
                return HttpNotFound();
            }
            ViewBag.KategoriID = new SelectList(km.List(), "ID", "KategoriAdi");
            return View(soru);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Soru soru)
        {
            if (ModelState.IsValid)
            {
                Soru db_soru = sm.Find(x => x.ID == soru.ID);

                db_soru.Icerik = soru.Icerik;
                db_soru.Baslik = soru.Baslik;
                db_soru.KategoriID = soru.KategoriID;

                sm.Update(db_soru);

                return RedirectToAction("Profil","Home");
            }

            ViewBag.KategoriID = new SelectList(km.List(), "ID", "KategoriAdi", soru.KategoriID);



            return View(soru);
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Soru soru = sm.Find(x => x.ID == id.Value);

            if (soru == null)
            {
                return HttpNotFound();
            }
            return View(soru);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {

            Soru db_soru = sm.Find(x => x.ID == id);

            List<SorununYorumu> sorununYorumlari = sym.IQueryableList().Where(x => x.Soru.ID == id).ToList();

            sm.Delete(db_soru);

            foreach (SorununYorumu item in sorununYorumlari)
            {
                sym.Delete(item);
            }

            string temp = TempData["profil"] as string;

            if(temp == "profil")
            {
                return RedirectToAction("Profil", "Home");
            }

            return RedirectToAction("Index");
        }


        //public ActionResult Yorumlar(int? id)
        //{

        //    if(id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }

        //    Soru soru = sm.IQueryableList().Include("Yorumlari").FirstOrDefault(x => x.ID == id.Value);

        //    if(soru == null)
        //    {
        //        return HttpNotFound();
        //    }

        //    ViewBag.SorununYorumlari = soru.Yorumlari;

        //    return View("Soru");
        //}
    }
}
