using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using LiquorKeeper.Models;

using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin.Security;

namespace LiquorKeeper.Controllers
{
    public class StoreProductController : Controller
    {
        //TODO: Create authentication attributes so we can take care of user and store authentication easier

        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: /StoreProduct/
        public ActionResult Index()
        {
            //we should only show products for the store they are interested in
            if (!LiquorKeeper.Library.Security.security.IsValidStore(User.Identity.GetUserId(), Guid.Parse(Request.QueryString["s"])))
                return Content("Permission Denied");

            Guid StoreID = Guid.Parse(Request.QueryString["s"]);

            return View(db.StoreProducts.Include(x => x.Product).Where(x => x.Store.ID.Equals(StoreID)).ToList());
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
            
            ViewBag.Products = db.Products.ToList();
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
                if (!LiquorKeeper.Library.Security.security.IsValidStore(User.Identity.GetUserId(), Guid.Parse(Request.Form["s"])))
                    return Content("Permission Denied");

                Guid StoreID = Guid.Parse(Request.Form["s"]);

                //TODO
                //we need to look up both store ID (and make sure it belongs to the signed in user) and the product id
                Guid ThisProductID = Guid.Parse(Request.Form["ProductID"].ToString());
                var GetProduct = db.Products.Where(x => x.ID.Equals(ThisProductID)).FirstOrDefault();

                //look up the store and user
                var ThisUserId = User.Identity.GetUserId();
                var ThisUser = db.Users.Where(x => x.Id.Equals(ThisUserId)).FirstOrDefault();

                var GetStore = db.Stores.Where(x => x.User.Id.Equals(ThisUser.Id)).Where(x => x.ID.Equals(StoreID)).FirstOrDefault();

                //if anything is empty, bail. We CAN NOT have people editing each others stores
                if (GetStore == null || GetProduct == null)
                {
                    return Content("Permission Denied");
                }

                storeproduct.Product = GetProduct;
                storeproduct.ID = Guid.NewGuid();
                storeproduct.Store = GetStore;
                db.StoreProducts.Add(storeproduct);
                db.SaveChanges();
                return RedirectToAction("Index", new { s = StoreID});
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
