using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using LiquorKeeper.Models;

namespace LiquorKeeper.Controllers
{
    public class StorePromotionsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: /StorePromotions/
        public ActionResult Index()
        {
            return View(db.StorePromotions.ToList());
        }

        // GET: /StorePromotions/Details/5
        public ActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StorePromotion storepromotion = db.StorePromotions.Find(id);
            if (storepromotion == null)
            {
                return HttpNotFound();
            }
            return View(storepromotion);
        }

        // GET: /StorePromotions/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: /StorePromotions/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="ID,Title,Description,Price,DateStart,DateEnd")] StorePromotion storepromotion)
        {
            if (ModelState.IsValid)
            {
                storepromotion.ID = Guid.NewGuid();
                db.StorePromotions.Add(storepromotion);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(storepromotion);
        }

        // GET: /StorePromotions/Edit/5
        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StorePromotion storepromotion = db.StorePromotions.Find(id);
            if (storepromotion == null)
            {
                return HttpNotFound();
            }
            return View(storepromotion);
        }

        // POST: /StorePromotions/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="ID,Title,Description,Price,DateStart,DateEnd")] StorePromotion storepromotion)
        {
            if (ModelState.IsValid)
            {
                db.Entry(storepromotion).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(storepromotion);
        }

        // GET: /StorePromotions/Delete/5
        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StorePromotion storepromotion = db.StorePromotions.Find(id);
            if (storepromotion == null)
            {
                return HttpNotFound();
            }
            return View(storepromotion);
        }

        // POST: /StorePromotions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            StorePromotion storepromotion = db.StorePromotions.Find(id);
            db.StorePromotions.Remove(storepromotion);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
