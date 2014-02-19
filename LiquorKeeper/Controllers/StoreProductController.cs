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
    public class StoreProductController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: /StoreProduct/
        public ActionResult Index()
        {
            return View(db.StoreProducts.ToList());
        }

        // GET: /StoreProduct/Details/5
        public ActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StoreProduct storeproduct = db.StoreProducts.Find(id);
            if (storeproduct == null)
            {
                return HttpNotFound();
            }
            return View(storeproduct);
        }

        // GET: /StoreProduct/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: /StoreProduct/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="ID,Price")] StoreProduct storeproduct)
        {
            if (ModelState.IsValid)
            {
                storeproduct.ID = Guid.NewGuid();
                db.StoreProducts.Add(storeproduct);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(storeproduct);
        }

        // GET: /StoreProduct/Edit/5
        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StoreProduct storeproduct = db.StoreProducts.Find(id);
            if (storeproduct == null)
            {
                return HttpNotFound();
            }
            return View(storeproduct);
        }

        // POST: /StoreProduct/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="ID,Price")] StoreProduct storeproduct)
        {
            if (ModelState.IsValid)
            {
                db.Entry(storeproduct).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(storeproduct);
        }

        // GET: /StoreProduct/Delete/5
        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StoreProduct storeproduct = db.StoreProducts.Find(id);
            if (storeproduct == null)
            {
                return HttpNotFound();
            }
            return View(storeproduct);
        }

        // POST: /StoreProduct/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            StoreProduct storeproduct = db.StoreProducts.Find(id);
            db.StoreProducts.Remove(storeproduct);
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
