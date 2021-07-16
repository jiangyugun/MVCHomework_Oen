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
    public class 客戶資料Controller : Controller
    {
        private CustomerProfileEntities db = new CustomerProfileEntities();

        // GET: 客戶資料
        public ActionResult Index(string sortOrder = "客戶名稱", bool isDesc = false, string searchString = "")
        {
            var 客戶資料 = from main in db.客戶資料
                       where main.is_Delete == false
                       select main;

            ViewBag.isDesc = isDesc == false ? true : false;

            if (!string.IsNullOrEmpty(searchString))
            {
                客戶資料 = 客戶資料.Where(s => s.客戶名稱.Contains(searchString));
            }

            #region 清單排序 升冪降冪
            switch (sortOrder)
            {
                case "客戶名稱":
                    if (isDesc)
                    {
                        客戶資料 = 客戶資料.OrderByDescending(s => s.客戶名稱);
                    }
                    else
                    {
                        客戶資料 = 客戶資料.OrderBy(s => s.客戶名稱);
                    }
                    break;
                case "統一編號":
                    if (isDesc)
                    {
                        客戶資料 = 客戶資料.OrderByDescending(s => s.統一編號);
                    }
                    else
                    {
                        客戶資料 = 客戶資料.OrderBy(s => s.統一編號);
                    }
                    break;
                case "電話":
                    if (isDesc)
                    {
                        客戶資料 = 客戶資料.OrderByDescending(s => s.電話);
                    }
                    else
                    {
                        客戶資料 = 客戶資料.OrderBy(s => s.電話);
                    }
                    break;
                case "傳真":
                    if (isDesc)
                    {
                        客戶資料 = 客戶資料.OrderByDescending(s => s.傳真);
                    }
                    else
                    {
                        客戶資料 = 客戶資料.OrderBy(s => s.傳真);
                    }
                    break;
                case "地址":
                    if (isDesc)
                    {
                        客戶資料 = 客戶資料.OrderByDescending(s => s.地址);
                    }
                    else
                    {
                        客戶資料 = 客戶資料.OrderBy(s => s.地址);
                    }
                    break;
                case "Email":
                    if (isDesc)
                    {
                        客戶資料 = 客戶資料.OrderByDescending(s => s.Email);
                    }
                    else
                    {
                        客戶資料 = 客戶資料.OrderBy(s => s.Email);
                    }
                    break;
                default:
                    客戶資料 = 客戶資料.OrderBy(s => s.客戶名稱);
                    break;
            }
            #endregion

            return View(客戶資料.ToList());
        }

        // GET: 客戶資料/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            客戶資料 客戶資料 = db.客戶資料.Find(id);
            if (客戶資料 == null)
            {
                return HttpNotFound();
            }
            return View(客戶資料);
        }

        // GET: 客戶資料/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: 客戶資料/Create
        // 若要避免過量張貼攻擊，請啟用您要繫結的特定屬性。
        // 如需詳細資料，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,客戶名稱,統一編號,電話,傳真,地址,Email")] 客戶資料 客戶資料)
        {
            //檢查Email是否重複
            if (db.客戶資料.Any(x => x.Email == 客戶資料.Email))
            {
                ModelState.AddModelError("Email", "Email不可重複");
                return View(客戶資料);
            }

            if (ModelState.IsValid)
            {
                db.客戶資料.Add(客戶資料);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(客戶資料);
        }

        // GET: 客戶資料/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            客戶資料 客戶資料 = db.客戶資料.Find(id);
            if (客戶資料 == null)
            {
                return HttpNotFound();
            }
            return View(客戶資料);
        }

        // POST: 客戶資料/Edit/5
        // 若要避免過量張貼攻擊，請啟用您要繫結的特定屬性。
        // 如需詳細資料，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,客戶名稱,統一編號,電話,傳真,地址,Email")] 客戶資料 客戶資料)
        {
            //檢查Email是否重複
            if (db.客戶資料.Any(x => x.Email == 客戶資料.Email))
            {
                ModelState.AddModelError("Email", "Email不可重複");
                return View(客戶資料);
            }

            if (ModelState.IsValid)
            {
                db.Entry(客戶資料).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(客戶資料);
        }

        // GET: 客戶資料/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            客戶資料 客戶資料 = db.客戶資料.Find(id);
            if (客戶資料 == null)
            {
                return HttpNotFound();
            }
            return View(客戶資料);
        }

        // POST: 客戶資料/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            客戶資料 客戶資料 = db.客戶資料.Find(id);
            客戶資料.is_Delete = true; //刪除資料功能不能真的刪除資料庫中的資料
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
