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
    public class ProductAccessoriesController : BaseController
    {
        private MobileStoreContext db = new MobileStoreContext();

        // GET: Admin/ProductAccessories
        public ActionResult Index()
        {
            var productAccessories = db.ProductAccessories.Include(p => p.Product);
            return View(productAccessories.ToList());
        }

        // GET: Admin/ProductAccessories/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProductAccessory productAccessory = db.ProductAccessories.Find(id);
            if (productAccessory == null)
            {
                return HttpNotFound();
            }
            return View(productAccessory);
        }

        // GET: Admin/ProductAccessories/Create
        public ActionResult Create()
        {
            ViewBag.ProductId = new SelectList(db.Products, "ProductId", "ProductName");
            return View();
        }

        // POST: Admin/ProductAccessories/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ProductAccessoryId,ProductId,Color,Price,ImageFile,Quantiny")] ProductAccessory productAccessory, HttpPostedFileBase imageFront, HttpPostedFileBase imageBack, HttpPostedFileBase imageSide)
        {
            if (ModelState.IsValid)
            {
                db.ProductAccessories.Add(productAccessory);
                db.SaveChanges();
                if (imageFront != null)
                {
                    //Resize Image
                    WebImage img = new WebImage(imageFront.InputStream);
                    img.Resize(500, 1000);

                    var filePathOriginal = Server.MapPath("/Assets/user/images");
                    var fileName = productAccessory.ProductId +"-"+productAccessory.Color+"-1"+ ".jpg";
                    string savedFileName = Path.Combine(filePathOriginal, fileName);
                    img.Save(savedFileName);
                }
                if (imageBack != null)
                {
                    //Resize Image
                    WebImage img = new WebImage(imageBack.InputStream);
                    img.Resize(500, 1000);

                    var filePathOriginal = Server.MapPath("/Assets/user/images");
                    var fileName = productAccessory.ProductId + "-" + productAccessory.Color + "-2" + ".jpg";
                    string savedFileName = Path.Combine(filePathOriginal, fileName);
                    img.Save(savedFileName);
                }
                if (imageSide != null)
                {
                    //Resize Image
                    WebImage img = new WebImage(imageSide.InputStream);
                    img.Resize(500, 1000);

                    var filePathOriginal = Server.MapPath("/Assets/user/images");
                    var fileName = productAccessory.ProductId + "-" + productAccessory.Color + "-3" + ".jpg";
                    string savedFileName = Path.Combine(filePathOriginal, fileName);
                    img.Save(savedFileName);
                }
                return RedirectToAction("Index");
            }

            ViewBag.ProductId = new SelectList(db.Products, "ProductId", "ProductName", productAccessory.ProductId);
            return View(productAccessory);
        }

        // GET: Admin/ProductAccessories/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProductAccessory productAccessory = db.ProductAccessories.Find(id);
            if (productAccessory == null)
            {
                return HttpNotFound();
            }
            ViewBag.ProductId = new SelectList(db.Products, "ProductId", "ProductName", productAccessory.ProductId);
            return View(productAccessory);
        }

        // POST: Admin/ProductAccessories/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ProductAccessoryId,ProductId,Color,Price,ImageFile,Quantiny")] ProductAccessory productAccessory)
        {
            if (ModelState.IsValid)
            {
                db.Entry(productAccessory).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ProductId = new SelectList(db.Products, "ProductId", "ProductName", productAccessory.ProductId);
            return View(productAccessory);
        }

        // GET: Admin/ProductAccessories/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProductAccessory productAccessory = db.ProductAccessories.Find(id);
            if (productAccessory == null)
            {
                return HttpNotFound();
            }
            return View(productAccessory);
        }

        // POST: Admin/ProductAccessories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ProductAccessory productAccessory = db.ProductAccessories.Find(id);
            db.ProductAccessories.Remove(productAccessory);
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
