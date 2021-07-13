using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MVCHomework_Oen.Models;

namespace MVCHomework_Oen.Controllers
{
    public class view_CustomerController : Controller
    {
        private CustomerProfileEntities db = new CustomerProfileEntities();

        // GET: view_Customer
        public ActionResult Index()
        {
            return View(db.view_Customer.ToList());
        }

        // GET: view_Customer/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            view_Customer view_Customer = db.view_Customer.Find(id);
            if (view_Customer == null)
            {
                return HttpNotFound();
            }
            return View(view_Customer);
        }

        // GET: view_Customer/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: view_Customer/Create
        // 若要避免過量張貼攻擊，請啟用您要繫結的特定屬性。
        // 如需詳細資料，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "客戶名稱,聯絡人數量,銀行帳戶數量")] view_Customer view_Customer)
        {
            if (ModelState.IsValid)
            {
                db.view_Customer.Add(view_Customer);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(view_Customer);
        }

        // GET: view_Customer/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            view_Customer view_Customer = db.view_Customer.Find(id);
            if (view_Customer == null)
            {
                return HttpNotFound();
            }
            return View(view_Customer);
        }

        // POST: view_Customer/Edit/5
        // 若要避免過量張貼攻擊，請啟用您要繫結的特定屬性。
        // 如需詳細資料，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "客戶名稱,聯絡人數量,銀行帳戶數量")] view_Customer view_Customer)
        {
            if (ModelState.IsValid)
            {
                db.Entry(view_Customer).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(view_Customer);
        }

        // GET: view_Customer/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            view_Customer view_Customer = db.view_Customer.Find(id);
            if (view_Customer == null)
            {
                return HttpNotFound();
            }
            return View(view_Customer);
        }

        // POST: view_Customer/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            view_Customer view_Customer = db.view_Customer.Find(id);
            db.view_Customer.Remove(view_Customer);
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
