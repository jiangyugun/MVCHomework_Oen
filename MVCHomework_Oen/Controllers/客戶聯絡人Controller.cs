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
    public class 客戶聯絡人Controller : Controller
    {
        private CustomerProfileEntities db = new CustomerProfileEntities();

        // GET: 客戶聯絡人
         public ActionResult Index(string sortOrder = "職稱", bool isDesc = false, string SearchString = "")
        {
            var 客戶聯絡人 = from main in db.客戶聯絡人
                        where main.is_Delete == false
                        select main;

            ViewBag.isDesc = isDesc == false ? true : false;

            if (!string.IsNullOrEmpty(SearchString))
            {
                客戶聯絡人 = 客戶聯絡人.Where(s => s.姓名.Contains(SearchString));
            }


            #region 清單排序 升冪降冪
            switch (sortOrder)
            {
                case "職稱":
                    if (isDesc)
                    {
                        客戶聯絡人 = 客戶聯絡人.OrderByDescending(s => s.職稱);
                    }
                    else
                    {
                        客戶聯絡人 = 客戶聯絡人.OrderBy(s => s.職稱);
                    }
                    break;
                case "姓名":
                    if (isDesc)
                    {
                        客戶聯絡人 = 客戶聯絡人.OrderByDescending(s => s.姓名);
                    }
                    else
                    {
                        客戶聯絡人 = 客戶聯絡人.OrderBy(s => s.姓名);
                    }
                    break;
                case "Email":
                    if (isDesc)
                    {
                        客戶聯絡人 = 客戶聯絡人.OrderByDescending(s => s.Email);
                    }
                    else
                    {
                        客戶聯絡人 = 客戶聯絡人.OrderBy(s => s.Email);
                    }
                    break;
                case "手機":
                    if (isDesc)
                    {
                        客戶聯絡人 = 客戶聯絡人.OrderByDescending(s => s.手機);
                    }
                    else
                    {
                        客戶聯絡人 = 客戶聯絡人.OrderBy(s => s.手機);
                    }
                    break;
                case "電話":
                    if (isDesc)
                    {
                        客戶聯絡人 = 客戶聯絡人.OrderByDescending(s => s.電話);
                    }
                    else
                    {
                        客戶聯絡人 = 客戶聯絡人.OrderBy(s => s.電話);
                    }
                    break;               
                default:
                    客戶聯絡人 = 客戶聯絡人.OrderBy(s => s.職稱);
                    break;
            }
            #endregion

            return View(客戶聯絡人.ToList());
        }

        // GET: 客戶聯絡人/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            客戶聯絡人 客戶聯絡人 = db.客戶聯絡人.Find(id);
            if (客戶聯絡人 == null)
            {
                return HttpNotFound();
            }
            return View(客戶聯絡人);
        }

        // GET: 客戶聯絡人/Create
        public ActionResult Create()
        {
            ViewBag.客戶Id = new SelectList(db.客戶資料, "Id", "客戶名稱");
            return View();
        }

        // POST: 客戶聯絡人/Create
        // 若要避免過量張貼攻擊，請啟用您要繫結的特定屬性。
        // 如需詳細資料，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,客戶Id,職稱,姓名,Email,手機,電話")] 客戶聯絡人 客戶聯絡人)
        {
            //檢查Email是否重複
            if (db.客戶聯絡人.Any(x => x.Email == 客戶聯絡人.Email))
            {
                ModelState.AddModelError("Email", "Email不可重複");
                ViewBag.客戶Id = new SelectList(db.客戶資料, "Id", "客戶名稱");
                return View(客戶聯絡人);
            }

            if (ModelState.IsValid)
            {
                db.客戶聯絡人.Add(客戶聯絡人);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.客戶Id = new SelectList(db.客戶資料, "Id", "客戶名稱", 客戶聯絡人.客戶Id);
            return View(客戶聯絡人);
        }

        // GET: 客戶聯絡人/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            客戶聯絡人 客戶聯絡人 = db.客戶聯絡人.Find(id);
            if (客戶聯絡人 == null)
            {
                return HttpNotFound();
            }
            ViewBag.客戶Id = new SelectList(db.客戶資料, "Id", "客戶名稱", 客戶聯絡人.客戶Id);
            return View(客戶聯絡人);
        }

        // POST: 客戶聯絡人/Edit/5
        // 若要避免過量張貼攻擊，請啟用您要繫結的特定屬性。
        // 如需詳細資料，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,客戶Id,職稱,姓名,Email,手機,電話")] 客戶聯絡人 客戶聯絡人)
        {
            //檢查Email是否重複
            if (db.客戶聯絡人.Any(x => x.Email == 客戶聯絡人.Email))
            {
                ModelState.AddModelError("Email", "Email不可重複");
                ViewBag.客戶Id = new SelectList(db.客戶資料, "Id", "客戶名稱"); //下拉選單加值
                return View(客戶聯絡人);
            }

            if (ModelState.IsValid)
            {
                db.Entry(客戶聯絡人).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.客戶Id = new SelectList(db.客戶資料, "Id", "客戶名稱", 客戶聯絡人.客戶Id);
            return View(客戶聯絡人);
        }

        // GET: 客戶聯絡人/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            客戶聯絡人 客戶聯絡人 = db.客戶聯絡人.Find(id);
            if (客戶聯絡人 == null)
            {
                return HttpNotFound();
            }
            return View(客戶聯絡人);
        }

        // POST: 客戶聯絡人/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            客戶聯絡人 客戶聯絡人 = db.客戶聯絡人.Find(id);
            客戶聯絡人.is_Delete = true; //刪除資料功能不能真的刪除資料庫中的資料
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
