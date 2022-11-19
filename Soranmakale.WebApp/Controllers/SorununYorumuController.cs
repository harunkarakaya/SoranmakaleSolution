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
using System.Data.Entity;

namespace Soranmakale.WebApp.Controllers
{
    public class SorununYorumuController : Controller
    {
        private SoruManager sm = new SoruManager();
        private SorununYorumuManager sym = new SorununYorumuManager();

        public ActionResult Index(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            //Soru soru = sm.Find(x => x.Id == id);
            Soru soru = sm.IQueryableList().Include("Yorumlari").FirstOrDefault(x => x.ID == id.Value);

            if (soru == null)
            {
                return HttpNotFound();
            }

            return PartialView("_PartialYonetimYorumlar", soru.Yorumlari);
        }

        //// GET: SorununYorumu/Details/5
        //public ActionResult Details(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    SorununYorumu sorununYorumu = db.SorununYorumus.Find(id);
        //    if (sorununYorumu == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(sorununYorumu);
        //}

        // GET: SorununYorumu/Create
        //public ActionResult Create()
        //{
        //    return View();
        //}

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Create(SorununYorumu sorununYorumu)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.SorununYorumus.Add(sorununYorumu);
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }

        //    return View(sorununYorumu);
        //}

        //// GET: SorununYorumu/Edit/5
        //public ActionResult Edit(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    SorununYorumu sorununYorumu = db.SorununYorumus.Find(id);
        //    if (sorununYorumu == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(sorununYorumu);
        //}

        //// POST: SorununYorumu/Edit/5
        //// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        //// more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Edit([Bind(Include = "ID,Icerik,BegenilmeSayisi,OlusturulmaTarihi,DuzenlenmeTarihi,SahibiAdi")] SorununYorumu sorununYorumu)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.Entry(sorununYorumu).State = EntityState.Modified;
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }
        //    return View(sorununYorumu);
        //}

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SorununYorumu sorununYorumu = sym.Find(x => x.ID == id.Value);

            if (sorununYorumu == null)
            {
                return HttpNotFound();
            }

            return View(sorununYorumu);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            SorununYorumu sorununYorumu = sym.Find(x => x.ID == id);

            sym.Delete(sorununYorumu);

            return RedirectToAction("Index", "Soru");
        }
    }
}
