using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Alborz.DataLayer.Context;
using Alborz.DomainLayer.DTO;

namespace AlborzMarket.Controllers
{
    public class ProductController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Product
        public ActionResult Index()
        {
            //file:///C:/Users/Maryam/Desktop/template/Bella/category.html
            var productTbls = db.ProductTbls.Include(p => p.CategoryTbl).Include(p => p.ProductTbl2);
            return View(productTbls.ToList());
        }

        // GET: Product/Details/5
        public ActionResult Details(int? id)
        {
            //file:///C:/Users/Maryam/Desktop/template/Bella/portfolio-single.html
            //file:///C:/Users/Maryam/Desktop/template/Bella/product-details.html

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProductTbl productTbl = db.ProductTbls.Find(id);
            if (productTbl == null)
            {
                return HttpNotFound();
            }
            return View(productTbl);
        }

        // GET: Product/Create
        public ActionResult Create()
        {
            ViewBag.CategoryId = new SelectList(db.CategoryTbls, "Id", "Title");
            ViewBag.ParentId = new SelectList(db.ProductTbls, "Id", "Code");
            return View();
        }

        // POST: Product/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,StartDate,EndDate,IsBuyable,Quantity,Code,ParentId,CategoryId,Title,Brand,CreatedDate,ModifiedDate,CreatedBy,ModifiedBy,IsActive")] ProductTbl productTbl)
        {
            if (ModelState.IsValid)
            {
                db.ProductTbls.Add(productTbl);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CategoryId = new SelectList(db.CategoryTbls, "Id", "Title", productTbl.CategoryId);
            ViewBag.ParentId = new SelectList(db.ProductTbls, "Id", "Code", productTbl.ParentId);
            return View(productTbl);
        }

        // GET: Product/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProductTbl productTbl = db.ProductTbls.Find(id);
            if (productTbl == null)
            {
                return HttpNotFound();
            }
            ViewBag.CategoryId = new SelectList(db.CategoryTbls, "Id", "Title", productTbl.CategoryId);
            ViewBag.ParentId = new SelectList(db.ProductTbls, "Id", "Code", productTbl.ParentId);
            return View(productTbl);
        }

        // POST: Product/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,StartDate,EndDate,IsBuyable,Quantity,Code,ParentId,CategoryId,Title,Brand,CreatedDate,ModifiedDate,CreatedBy,ModifiedBy,IsActive")] ProductTbl productTbl)
        {
            if (ModelState.IsValid)
            {
                db.Entry(productTbl).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CategoryId = new SelectList(db.CategoryTbls, "Id", "Title", productTbl.CategoryId);
            ViewBag.ParentId = new SelectList(db.ProductTbls, "Id", "Code", productTbl.ParentId);
            return View(productTbl);
        }

        // GET: Product/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProductTbl productTbl = db.ProductTbls.Find(id);
            if (productTbl == null)
            {
                return HttpNotFound();
            }
            return View(productTbl);
        }

        // POST: Product/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ProductTbl productTbl = db.ProductTbls.Find(id);
            db.ProductTbls.Remove(productTbl);
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
