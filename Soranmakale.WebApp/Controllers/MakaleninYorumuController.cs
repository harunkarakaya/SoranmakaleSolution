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
    public class MakaleninYorumuController : Controller
    {
        private MakaleManager mm = new MakaleManager();
        private MakaleninYorumuManager mym = new MakaleninYorumuManager();

        public ActionResult Index(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Makale makale = mm.IQueryableList().Include("Yorumlari").FirstOrDefault(x => x.ID == id.Value);

            if (makale == null)
            {
                return HttpNotFound();
            }

            return PartialView("_PartialYonetimMakaleYorumlari", makale.Yorumlari);
        }


        //public ActionResult Details(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    MakaleninYorumu makaleninYorumu = mym.Find(x => x.ID == id.Value);

        //    if (makaleninYorumu == null)
        //    {
        //        return HttpNotFound();
        //    }

        //    return View(makaleninYorumu);
        //}

        //// GET: MakaleninYorumu/Create
        //public ActionResult Create()
        //{
        //    return View();
        //}

        //// POST: MakaleninYorumu/Create
        //// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        //// more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Create([Bind(Include = "ID,Icerik,OlusturulmaTarihi,DuzenlenmeTarihi,SahibiAdi")] MakaleninYorumu makaleninYorumu)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.MakaleninYorumus.Add(makaleninYorumu);
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }

        //    return View(makaleninYorumu);
        //}

        //// GET: MakaleninYorumu/Edit/5
        //public ActionResult Edit(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    MakaleninYorumu makaleninYorumu = db.MakaleninYorumus.Find(id);
        //    if (makaleninYorumu == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(makaleninYorumu);
        //}

        //// POST: MakaleninYorumu/Edit/5
        //// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        //// more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Edit([Bind(Include = "ID,Icerik,OlusturulmaTarihi,DuzenlenmeTarihi,SahibiAdi")] MakaleninYorumu makaleninYorumu)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.Entry(makaleninYorumu).State = EntityState.Modified;
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }
        //    return View(makaleninYorumu);
        //}

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MakaleninYorumu makaleninYorumu = mym.Find(x => x.ID == id.Value);

            if (makaleninYorumu == null)
            {
                return HttpNotFound();
            }
            return View(makaleninYorumu);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            MakaleninYorumu makaleninYorumu = mym.Find(x => x.ID == id);

            mym.Delete(makaleninYorumu);

            return RedirectToAction("Index", "Makale");
        }
    }
}
