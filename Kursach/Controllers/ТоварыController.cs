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
    public class ТоварыController : Controller
    {
        private Entities db = new Entities();
        //[Authorize]
        public ActionResult Index()
        {
            ViewBag.Id_Категории = new SelectList(db.Категория, "Id_Категории", "Наименование").OrderBy(o=>o.Text).Prepend(new SelectListItem { Value = Convert.ToString(Guid.Empty), Text = "Все категории" });
            var товары = db.ProductsWithPrice.Where(o => o.Дата_окончания == null).ToList();
            return View(товары);
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index([Bind(Include = "Id_Категории")] Товары products)
        {
            List<ProductsWithPrice> товары;
            ViewBag.Id_Категории = new SelectList(db.Категория, "Id_Категории", "Наименование").OrderBy(o => o.Text).Prepend(new SelectListItem { Value = Convert.ToString(Guid.Empty), Text = "Все категории" });
            if (products.Id_Категории == Guid.Empty)
                товары = db.ProductsWithPrice.Where(o => o.Дата_окончания == null).ToList();
            else 
                товары = db.ProductsWithPrice.Where(o => o.Дата_окончания == null && o.Id_Категории == products.Id_Категории).ToList();
            return View(товары);
        }
        [Authorize]
        public ActionResult AddComment(Guid idProduct, string name, string comment)
        {
            if (idProduct != null && name != "" && comment != "")
            {
                Guid id = Guid.NewGuid();
                db.CommentProduct.Add(new CommentProduct()
                {
                    Id_CommentProduct = id,
                    Id_Товара = idProduct,
                    Name = name,
                    Comment = comment
                });
                db.SaveChanges();
            }
            return RedirectToAction("Details", new { id = idProduct });
        }
        public ActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Товары товары = db.Товары.Find(id);
            if (товары == null)
            {
                return HttpNotFound();
            }
            return View(товары);
        }
        [Authorize]
        public ActionResult Create()
        {
            ViewBag.Id_Категории = new SelectList(db.Категория, "Id_Категории", "Наименование");
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id_Товара,Наименование,Id_Категории")] Товары товары)
        {
            if (ModelState.IsValid)
            {
                товары.Id_Товара = Guid.NewGuid();
                db.Товары.Add(товары);
                db.SaveChanges();
                Guid idPrice = Guid.NewGuid();
                Guid idProduct = товары.Id_Товара;
                int price = 0;
                db.Цена.Add(new Цена()
                {
                    Id_цена = idPrice,
                    Цена1 = price,
                    Дата_установки = DateTime.Now,
                    Id_Товара = idProduct,
                }) ;
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Id_Категории = new SelectList(db.Категория, "Id_Категории", "Наименование", товары.Id_Категории);
            return View(товары);
        }

        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var товары = db.ProductsWithPrice.Where(o => o.Id_Товара == id).ToList();

            return View(товары);
        }
        [Authorize]
        [HttpPost]
        public ActionResult Edit(string product,int price)
        {
            if (price > 0)
            {
                db.ChangePrice(product, price);
            }
            //return RedirectToAction("Index");
            Guid? idProduct = db.Товары.FirstOrDefault(o => o.Наименование == product).Id_Товара;
            return RedirectToAction("Edit", new { id = idProduct });
        }
        [Authorize]
        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Товары товары = db.Товары.Find(id);
            if (товары == null)
            {
                return HttpNotFound();
            }
            return View(товары);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            Товары товары = db.Товары.Find(id);
            db.Товары.Remove(товары);
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
