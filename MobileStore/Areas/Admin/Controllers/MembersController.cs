using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Models.EF;
using Models.ViewModels;

namespace MobileStore.Areas.Admin.Controllers
{
    public class MembersController : BaseController
    {
        private MobileStoreContext db = new MobileStoreContext();

        // GET: Admin/Members
        public ActionResult Index()
        {
            var members = db.Members.Include(m => m.GenderType).Include(m => m.MemberType).Include(m => m.Store);
            return View(members.ToList());
        }

        // GET: Admin/Members/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Member member = db.Members.Find(id);
            if (member == null)
            {
                return HttpNotFound();
            }
            return View(member);
        }

        // GET: Admin/Members/Create
        public ActionResult Create()
        {
            ViewBag.GenderTypeId = new SelectList(db.GenderTypes, "GenderTypeId", "GenderName");
            ViewBag.MemberTypeId = new SelectList(db.MemberTypes, "MemberTypeId", "RoleMember");
            ViewBag.StoreId = new SelectList(db.Stores, "StoreId", "StoreName");
            return View();
        }

        // POST: Admin/Members/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MemberEmail,MemberPassword,FullName,PhoneNumber,GenderTypeId,Birthday,AddressMemberName,FileImage,StoreId,MemberTypeId")] MemberVM memberVM)
        {
            if (ModelState.IsValid)
            {
                memberVM.RegDate = System.DateTime.Now;
                Member member = new Member() { MemberEmail = memberVM.MemberEmail, MemberPassword = memberVM.MemberPassword, FullName = memberVM.FullName, PhoneNumber = memberVM.PhoneNumber, GenderTypeId = memberVM.GenderTypeId, Birthday = memberVM.Birthday, FileImage = memberVM.FileImage, StoreId = memberVM.StoreId, MemberTypeId = memberVM.MemberTypeId };
                db.Members.Add(member);
                db.AddressMembers.Add(new AddressMember() { AddressMemberName = memberVM.AddressMemberName, MemberId = member.MemberId, PriorityStatus = 1 });
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.GenderTypeId = new SelectList(db.GenderTypes, "GenderTypeId", "GenderName", memberVM.GenderTypeId);
            ViewBag.MemberTypeId = new SelectList(db.MemberTypes, "MemberTypeId", "RoleMember", memberVM.MemberTypeId);
            ViewBag.StoreId = new SelectList(db.Stores, "StoreId", "StoreName", memberVM.StoreId);
            return View(memberVM);
        }

        // GET: Admin/Members/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Member member = db.Members.Find(id);
            if (member == null)
            {
                return HttpNotFound();
            }
            ViewBag.GenderTypeId = new SelectList(db.GenderTypes, "GenderTypeId", "GenderName", member.GenderTypeId);
            ViewBag.MemberTypeId = new SelectList(db.MemberTypes, "MemberTypeId", "RoleMember", member.MemberTypeId);
            ViewBag.StoreId = new SelectList(db.Stores, "StoreId", "StoreName", member.StoreId);
            return View(member);
        }

        // POST: Admin/Members/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MemberId,MemberEmail,MemberPassword,FullName,PhoneNumber,GenderTypeId,Birthday,HomeAddress,RegDate,FileImage,StoreId,MemberTypeId")] Member member)
        {
            if (ModelState.IsValid)
            {
                db.Entry(member).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.GenderTypeId = new SelectList(db.GenderTypes, "GenderTypeId", "GenderName", member.GenderTypeId);
            ViewBag.MemberTypeId = new SelectList(db.MemberTypes, "MemberTypeId", "RoleMember", member.MemberTypeId);
            ViewBag.StoreId = new SelectList(db.Stores, "StoreId", "StoreName", member.StoreId);
            return View(member);
        }

        // GET: Admin/Members/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Member member = db.Members.Find(id);
            if (member == null)
            {
                return HttpNotFound();
            }
            return View(member);
        }

        // POST: Admin/Members/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Member member = db.Members.Find(id);
            db.Members.Remove(member);
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
