using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using Models.EF;

namespace MobileStore.Areas.Admin.Controllers
{
    public class ProductsController : BaseController
    {
        private MobileStoreContext db = new MobileStoreContext();

        // GET: Admin/Products
        public ActionResult Index()
        {
            var products = db.Products.Include(p => p.BrandType).Include(p => p.ProductType);
            return View(products.ToList());
        }

        // GET: Admin/Products/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // GET: Admin/Products/Create
        public ActionResult Create()
        {
            ViewBag.BrandTypeId = new SelectList(db.BrandTypes, "BrandTypeId", "BrandName");
            ViewBag.ProductTypeId = new SelectList(db.ProductTypes, "ProductTypeId", "ProductTypeName");
            return View();
        }

        // POST: Admin/Products/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ProductId,ProductName,FileImage,BrandTypeId,ProductTypeId,RealeaseDate")] Product product, HttpPostedFileBase FileImage)
        {
            if (ModelState.IsValid)
            {
                int productId = db.Products.Select(m => m.ProductId).Max() + 1;
                product.FileImage = "/Assets/user/images/" + productId+".jpg";
                db.Products.Add(product);
                db.SaveChanges();
                if (FileImage != null)
                {
                    //Resize Image
                    WebImage img = new WebImage(FileImage.InputStream);
                    img.Resize(160, 212);

                    var filePathOriginal = Server.MapPath("/Assets/user/images");
                    var fileName = productId + ".jpg";
                    string savedFileName = Path.Combine(filePathOriginal, fileName);
                    img.Save(savedFileName);
                }
                
                return RedirectToAction("Index");
            }

            ViewBag.BrandTypeId = new SelectList(db.BrandTypes, "BrandTypeId", "BrandName", product.BrandTypeId);
            ViewBag.ProductTypeId = new SelectList(db.ProductTypes, "ProductTypeId", "ProductTypeName", product.ProductTypeId);
            return View(product);
        }

        // GET: Admin/Products/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            ViewBag.BrandTypeId = new SelectList(db.BrandTypes, "BrandTypeId", "BrandName", product.BrandTypeId);
            ViewBag.ProductTypeId = new SelectList(db.ProductTypes, "ProductTypeId", "ProductTypeName", product.ProductTypeId);
            return View(product);
        }

        // POST: Admin/Products/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ProductId,ProductName,FileImage,BrandTypeId,ProductTypeId,RealeaseDate")] Product product)
        {
            if (ModelState.IsValid)
            {
                db.Entry(product).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.BrandTypeId = new SelectList(db.BrandTypes, "BrandTypeId", "BrandName", product.BrandTypeId);
            ViewBag.ProductTypeId = new SelectList(db.ProductTypes, "ProductTypeId", "ProductTypeName", product.ProductTypeId);
            return View(product);
        }

        // GET: Admin/Products/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // POST: Admin/Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Product product = db.Products.Find(id);
            db.Products.Remove(product);
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
