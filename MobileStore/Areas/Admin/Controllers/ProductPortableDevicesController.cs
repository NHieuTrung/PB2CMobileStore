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
    public class ProductPortableDevicesController : BaseController
    {
        private MobileStoreContext db = new MobileStoreContext();

        // GET: Admin/ProductPortableDevices
        public ActionResult Index()
        {
            var productPortableDevices = db.ProductPortableDevices.Include(p => p.Product);
            return View(productPortableDevices.ToList());
        }

        // GET: Admin/ProductPortableDevices/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProductPortableDevice productPortableDevice = db.ProductPortableDevices.Find(id);
            if (productPortableDevice == null)
            {
                return HttpNotFound();
            }
            return View(productPortableDevice);
        }

        // GET: Admin/ProductPortableDevices/Create
        public ActionResult Create()
        {
            ViewBag.ProductId = new SelectList(db.Products, "ProductId", "ProductName");
            return View();
        }

        // POST: Admin/ProductPortableDevices/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ProductPortableDeviceId,ProductId,Color,MemorySize,RamSize,Display,Price,FileImage,Quantiny")] ProductPortableDevice productPortableDevice, HttpPostedFileBase imageFront, HttpPostedFileBase imageBack, HttpPostedFileBase imageSide)
        {
            string productType = db.Products.Where(m => m.ProductId == productPortableDevice.ProductId).Select(m => m.ProductType.ProductTypeName).FirstOrDefault();
            if (imageFront != null)
            {
                //Resize Image
                WebImage img = new WebImage(imageFront.InputStream);
                img.Resize(500, 1000);

                var filePathOriginal = Server.MapPath("/Assets/user/images/"+productType+"/");
                var fileName = productPortableDevice.ProductId + "-" + productPortableDevice.Color + "-1" + ".jpg";
                string savedFileName = Path.Combine(filePathOriginal, fileName);
                img.Save(savedFileName);
            }
            if (imageBack != null)
            {
                //Resize Image
                WebImage img = new WebImage(imageBack.InputStream);
                img.Resize(500, 1000);

                var filePathOriginal = Server.MapPath("/Assets/user/images/" + productType + "/");
                var fileName = productPortableDevice.ProductId + "-" + productPortableDevice.Color + "-2" + ".jpg";
                string savedFileName = Path.Combine(filePathOriginal, fileName);
                img.Save(savedFileName);
            }
            if (imageSide != null)
            {
                //Resize Image
                WebImage img = new WebImage(imageSide.InputStream);
                img.Resize(500, 1000);

                var filePathOriginal = Server.MapPath("/Assets/user/images/" + productType + "/");
                var fileName = productPortableDevice.ProductId + "-" + productPortableDevice.Color + "-3" + ".jpg";
                string savedFileName = Path.Combine(filePathOriginal, fileName);
                img.Save(savedFileName);
            }
            if (ModelState.IsValid)
            {
                db.ProductPortableDevices.Add(productPortableDevice);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ProductId = new SelectList(db.Products, "ProductId", "ProductName", productPortableDevice.ProductId);
            return View(productPortableDevice);
        }

        // GET: Admin/ProductPortableDevices/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProductPortableDevice productPortableDevice = db.ProductPortableDevices.Find(id);
            if (productPortableDevice == null)
            {
                return HttpNotFound();
            }
            ViewBag.ProductId = new SelectList(db.Products, "ProductId", "ProductName", productPortableDevice.ProductId);
            return View(productPortableDevice);
        }

        // POST: Admin/ProductPortableDevices/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ProductPortableDeviceId,ProductId,Color,MemorySize,RamSize,Display,Price,FileImage,Quantiny")] ProductPortableDevice productPortableDevice)
        {
            if (ModelState.IsValid)
            {
                db.Entry(productPortableDevice).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ProductId = new SelectList(db.Products, "ProductId", "ProductName", productPortableDevice.ProductId);
            return View(productPortableDevice);
        }

        // GET: Admin/ProductPortableDevices/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProductPortableDevice productPortableDevice = db.ProductPortableDevices.Find(id);
            if (productPortableDevice == null)
            {
                return HttpNotFound();
            }
            return View(productPortableDevice);
        }

        // POST: Admin/ProductPortableDevices/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ProductPortableDevice productPortableDevice = db.ProductPortableDevices.Find(id);
            db.ProductPortableDevices.Remove(productPortableDevice);
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
