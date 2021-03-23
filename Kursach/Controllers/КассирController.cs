using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Kursach;

namespace Kursach.Controllers
{
    public class КассирController : Controller
    {
        private Entities db = new Entities();
        public ActionResult Index()
        {
            return View(db.Кассир.ToList());
        }

        public ActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Кассир кассир = db.Кассир.Find(id);

            if (кассир == null)
            {
                return HttpNotFound();
            }
            return View(кассир);
        }
        [Authorize]
        public ActionResult AddComment(Guid idCashier, string name, string comment)
        {
            if (idCashier != null && name != "" && comment != "")
            {
                Guid id = Guid.NewGuid();
                db.CommentCashier.Add(new CommentCashier()
                {
                    Id_CommentCashier = id,
                    Id_Кассира = idCashier,
                    Nickname = name,
                    Comment = comment
                });
                db.SaveChanges();
            }
            return RedirectToAction("Details",new { id = idCashier});
        }
        [Authorize]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id_Кассира,Фамилия,Имя,Отчество")] Кассир кассир)
        {
            if (ModelState.IsValid)
            {
                кассир.Id_Кассира = Guid.NewGuid();
                кассир.Принят_на_работу = DateTime.Now;
                кассир.Уволен = null;
                db.Кассир.Add(кассир);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(кассир);
        }
        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Кассир кассир = db.Кассир.Find(id);
            if (кассир == null)
            {
                return HttpNotFound();
            }
            return View(кассир);
        }
        [Authorize]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            Кассир кассир = db.Кассир.Find(id);
            кассир.Уволен = DateTime.Now;
            db.Entry(кассир).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult Employ(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Кассир кассир = db.Кассир.Find(id);
            if (кассир == null)
            {
                return HttpNotFound();
            }
            return View(кассир);
        }
        [Authorize]
        [HttpPost, ActionName("Employ")]
        [ValidateAntiForgeryToken]
        public ActionResult EmployConfirmed(Guid id)
        {
            Кассир кассир = db.Кассир.Find(id);
            кассир.Уволен = null;
            кассир.Принят_на_работу = DateTime.Now;
            db.Entry(кассир).State = EntityState.Modified;
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
