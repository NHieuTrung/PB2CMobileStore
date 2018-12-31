using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Models.EF;

namespace MobileStore.Areas.Admin.Controllers
{
    public class ShoppingCartsController : Controller
    {
        private MobileStoreContext db = new MobileStoreContext();

        // GET: Admin/ShoppingCarts
        public ActionResult Index()
        {
            var shoppingCarts = db.ShoppingCarts.Include(s => s.Member).Include(s => s.Member1).Include(s => s.Promotion);
            return View(shoppingCarts.ToList());
        }

        // GET: Admin/ShoppingCarts/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ShoppingCart shoppingCart = db.ShoppingCarts.Find(id);
            if (shoppingCart == null)
            {
                return HttpNotFound();
            }
            return View(shoppingCart);
        }

        // GET: Admin/ShoppingCarts/Create
        public ActionResult Create()
        {
            ViewBag.CustomerId = new SelectList(db.Members, "MemberId", "MemberEmail");
            ViewBag.EmployeeId = new SelectList(db.Members, "MemberId", "MemberEmail");
            ViewBag.PromotionId = new SelectList(db.Promotions, "PromotionId", "PromotionCode");
            return View();
        }

        // POST: Admin/ShoppingCarts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ShoppingCartId,CustomerId,OrderDate,ShippedDate,EmployeeId,PromotionId,PaymentType,StatusCart")] ShoppingCart shoppingCart)
        {
            if (ModelState.IsValid)
            {
                db.ShoppingCarts.Add(shoppingCart);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CustomerId = new SelectList(db.Members, "MemberId", "MemberEmail", shoppingCart.CustomerId);
            ViewBag.EmployeeId = new SelectList(db.Members, "MemberId", "MemberEmail", shoppingCart.EmployeeId);
            ViewBag.PromotionId = new SelectList(db.Promotions, "PromotionId", "PromotionCode", shoppingCart.PromotionId);
            return View(shoppingCart);
        }

        // GET: Admin/ShoppingCarts/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ShoppingCart shoppingCart = db.ShoppingCarts.Find(id);
            if (shoppingCart == null)
            {
                return HttpNotFound();
            }
            ViewBag.CustomerId = new SelectList(db.Members, "MemberId", "MemberEmail", shoppingCart.CustomerId);
            ViewBag.EmployeeId = new SelectList(db.Members, "MemberId", "MemberEmail", shoppingCart.EmployeeId);
            ViewBag.PromotionId = new SelectList(db.Promotions, "PromotionId", "PromotionCode", shoppingCart.PromotionId);
            return View(shoppingCart);
        }

        // POST: Admin/ShoppingCarts/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ShoppingCartId,CustomerId,OrderDate,ShippedDate,EmployeeId,PromotionId,PaymentType,StatusCart")] ShoppingCart shoppingCart)
        {
            if (ModelState.IsValid)
            {
                db.Entry(shoppingCart).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CustomerId = new SelectList(db.Members, "MemberId", "MemberEmail", shoppingCart.CustomerId);
            ViewBag.EmployeeId = new SelectList(db.Members, "MemberId", "MemberEmail", shoppingCart.EmployeeId);
            ViewBag.PromotionId = new SelectList(db.Promotions, "PromotionId", "PromotionCode", shoppingCart.PromotionId);
            return View(shoppingCart);
        }

        // GET: Admin/ShoppingCarts/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ShoppingCart shoppingCart = db.ShoppingCarts.Find(id);
            if (shoppingCart == null)
            {
                return HttpNotFound();
            }
            return View(shoppingCart);
        }

        // POST: Admin/ShoppingCarts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ShoppingCart shoppingCart = db.ShoppingCarts.Find(id);
            db.ShoppingCarts.Remove(shoppingCart);
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
