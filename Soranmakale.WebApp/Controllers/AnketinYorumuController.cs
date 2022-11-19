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
    public class AnketinYorumuController : Controller
    {
        private AnketManager anm = new AnketManager();
        private AnketinYorumuManager aym = new AnketinYorumuManager();

        public ActionResult Index(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Anket anket = anm.IQueryableList().Include("Yorumu").FirstOrDefault(x => x.ID == id.Value);

            if (anket == null)
            {
                return HttpNotFound();
            }

            return PartialView("_PartialYonetimAnketYorumlari", anket.Yorumu);
        }

        //public ActionResult Details(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    AnketinYorumu anketinYorumu = db.AnketinYorumus.Find(id);
        //    if (anketinYorumu == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(anketinYorumu);
        //}

        //public ActionResult Create()
        //{
        //    return View();
        //}

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Create([Bind(Include = "ID,Icerik,OlusturulmaTarihi,DuzenlenmeTarihi,SahibiAdi")] AnketinYorumu anketinYorumu)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.AnketinYorumus.Add(anketinYorumu);
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }

        //    return View(anketinYorumu);
        //}

        //public ActionResult Edit(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    AnketinYorumu anketinYorumu = db.AnketinYorumus.Find(id);
        //    if (anketinYorumu == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(anketinYorumu);
        //}


        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Edit([Bind(Include = "ID,Icerik,OlusturulmaTarihi,DuzenlenmeTarihi,SahibiAdi")] AnketinYorumu anketinYorumu)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.Entry(anketinYorumu).State = EntityState.Modified;
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }
        //    return View(anketinYorumu);
        //}

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AnketinYorumu anketinYorumu = aym.Find(x => x.ID == id.Value);

            if (anketinYorumu == null)
            {
                return HttpNotFound();
            }

            return View(anketinYorumu);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            AnketinYorumu anketinYorumu = aym.Find(x => x.ID == id);

            aym.Delete(anketinYorumu);

            return RedirectToAction("Index","Anket");
        }
    }
}
