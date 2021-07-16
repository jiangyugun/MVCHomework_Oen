using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MVCHomework_Oen.Models;
using System.Linq.Dynamic;

namespace MVCHomework_Oen.Controllers
{
    public class 客戶銀行資訊Controller : Controller
    {
        private CustomerProfileEntities db = new CustomerProfileEntities();

        // GET: 客戶銀行資訊
        public ActionResult Index(string sortOrder= "銀行名稱", bool isDesc=false, string SearchString = "")
        {
            var 客戶銀行資訊 = from main in db.客戶銀行資訊
                         where main.is_Delete == false
                         select main;

            ViewBag.isDesc = isDesc == false ? true :false;

            if (!string.IsNullOrEmpty(SearchString))
            {
                客戶銀行資訊 = 客戶銀行資訊.Where(s => s.銀行名稱.Contains(SearchString));
            }

            #region 清單排序 升冪降冪
            switch (sortOrder)
            {
                case "銀行名稱":
                    if(isDesc)
                    {
                        客戶銀行資訊 = 客戶銀行資訊.OrderByDescending(s => s.銀行名稱);
                    }
                    else
                    {
                        客戶銀行資訊 = 客戶銀行資訊.OrderBy(s => s.銀行名稱);
                    }
                    break;
                case "銀行代碼":
                    if (isDesc)
                    {
                        客戶銀行資訊 = 客戶銀行資訊.OrderByDescending(s => s.銀行代碼);
                    }
                    else
                    {
                        客戶銀行資訊 = 客戶銀行資訊.OrderBy(s => s.銀行代碼);
                    }
                    break;
                case "分行代碼":
                    if (isDesc)
                    {
                        客戶銀行資訊 = 客戶銀行資訊.OrderByDescending(s => s.分行代碼);
                    }
                    else
                    {
                        客戶銀行資訊 = 客戶銀行資訊.OrderBy(s => s.分行代碼);
                    }
                    break;
                case "帳戶名稱":
                    if (isDesc)
                    {
                        客戶銀行資訊 = 客戶銀行資訊.OrderByDescending(s => s.帳戶名稱);
                    }
                    else
                    {
                        客戶銀行資訊 = 客戶銀行資訊.OrderBy(s => s.帳戶名稱);
                    }
                    break;
                case "帳戶號碼":
                    if (isDesc)
                    {
                        客戶銀行資訊 = 客戶銀行資訊.OrderByDescending(s => s.帳戶號碼);
                    }
                    else
                    {
                        客戶銀行資訊 = 客戶銀行資訊.OrderBy(s => s.帳戶號碼);
                    }
                    break;                
                default:
                    客戶銀行資訊 = 客戶銀行資訊.OrderBy(s => s.銀行名稱);
                    break;
            }
            #endregion

            return View(客戶銀行資訊.ToList());
        }

        public IQueryable<客戶銀行資訊> Sort(IQueryable<客戶銀行資訊> quizzes, string key, bool isDesc)
        {
            quizzes = quizzes.OrderBy(key + (isDesc ? " descending" : ""));
            return quizzes;
        }

        // GET: 客戶銀行資訊/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            客戶銀行資訊 客戶銀行資訊 = db.客戶銀行資訊.Find(id);
            if (客戶銀行資訊 == null)
            {
                return HttpNotFound();
            }
            return View(客戶銀行資訊);
        }

        // GET: 客戶銀行資訊/Create
        public ActionResult Create()
        {
            var 客戶資料 = from main in db.客戶資料
                       where main.is_Delete == false
                       select main;

            ViewBag.客戶Id = new SelectList(客戶資料, "Id", "客戶名稱");
            return View();
        }

        // POST: 客戶銀行資訊/Create
        // 若要避免過量張貼攻擊，請啟用您要繫結的特定屬性。
        // 如需詳細資料，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,客戶Id,銀行名稱,銀行代碼,分行代碼,帳戶名稱,帳戶號碼")] 客戶銀行資訊 客戶銀行資訊)
        {
            if (ModelState.IsValid)
            {
                db.客戶銀行資訊.Add(客戶銀行資訊);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(客戶銀行資訊);
        }

        // GET: 客戶銀行資訊/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            客戶銀行資訊 客戶銀行資訊 = db.客戶銀行資訊.Find(id);
            if (客戶銀行資訊 == null)
            {
                return HttpNotFound();
            }
            ViewBag.客戶Id = new SelectList(db.客戶銀行資訊, "Id", "客戶名稱", 客戶銀行資訊.客戶Id);
            return View(客戶銀行資訊);
        }

        // POST: 客戶銀行資訊/Edit/5
        // 若要避免過量張貼攻擊，請啟用您要繫結的特定屬性。
        // 如需詳細資料，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,客戶Id,銀行名稱,銀行代碼,分行代碼,帳戶名稱,帳戶號碼")] 客戶銀行資訊 客戶銀行資訊)
        {
            if (ModelState.IsValid)
            {
                db.Entry(客戶銀行資訊).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.客戶Id = new SelectList(db.客戶銀行資訊, "Id", "客戶名稱", 客戶銀行資訊.客戶Id);
            return View(客戶銀行資訊);
        }

        // GET: 客戶銀行資訊/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            客戶銀行資訊 客戶銀行資訊 = db.客戶銀行資訊.Find(id);
            if (客戶銀行資訊 == null)
            {
                return HttpNotFound();
            }
            return View(客戶銀行資訊);
        }

        // POST: 客戶銀行資訊/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            客戶銀行資訊 客戶銀行資訊 = db.客戶銀行資訊.Find(id);
            客戶銀行資訊.is_Delete = true; //刪除資料功能不能真的刪除資料庫中的資料
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
