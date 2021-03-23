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
    public class Чек_ПокупкиController : Controller
    {
        private Entities db = new Entities();

        public ActionResult CountKinds(IEnumerable<Чек_Покупки> chek)
        {
            CountKinds item = new CountKinds(db.Чек_Покупки);
            item.kind = db.Вид_оплаты;
            return View(item);
        }

        public ActionResult ProductsInCheck(Guid? id)
        {
            ViewBag.Id_Товара = new SelectList(db.Товары, "Id_Товара", "Наименование").OrderBy(i => i.Text);
            var items = db.PurchaseReceipt.Where(o => o.Id_Чека == id).ToList();
            return View(items);
        }
        [Authorize]
        public ActionResult AddProduct(Guid idPurchase, Guid Id_Товара, int number)
        {
            Товары_куплены product = db.Товары_куплены.FirstOrDefault(o => o.Id_Чека == idPurchase && o.Id_Товара == Id_Товара);
            if (product == null)
            {
                if (number > 0)
                {
                    Guid idPrice;
                    DateTime datePurchase = db.Чек_Покупки.Find(idPurchase).Дата_и_время_покупки;
                    List<Цена> цена = db.Цена.Where(o => o.Id_Товара == Id_Товара && o.Дата_установки < datePurchase).OrderByDescending(o => o.Дата_установки).ToList();
                    if (цена[0].Дата_окончания == null)
                    {
                        idPrice = цена.FirstOrDefault(o => o.Дата_окончания == null).Id_цена;
                        db.Товары_куплены.Add(new Товары_куплены()
                        {
                            Количество = number,
                            Id_Чека = idPurchase,
                            Id_цена = idPrice,
                            Id_Товара = Id_Товара
                        });
                        db.SaveChanges();
                    }
                    else
                    {
                        idPrice = цена.FirstOrDefault(o => o.Дата_окончания >= datePurchase).Id_цена;
                        db.Товары_куплены.Add(new Товары_куплены()
                        {
                            Количество = number,
                            Id_Чека = idPurchase,
                            Id_цена = idPrice,
                            Id_Товара = Id_Товара
                        });
                        db.SaveChanges();
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Введите положительное число");
                }
            }
            else
            {
                if (product.Количество + number > 0)
                {
                    product.Количество += number;
                    db.Entry(product).State = EntityState.Modified;
                    db.SaveChanges();
                }
                else
                {
                    db.Товары_куплены.Remove(product);
                    db.SaveChanges();
                }
            }
            return RedirectToAction("ProductsInCheck", new { id = idPurchase });
        }
        //[Authorize]
        public ActionResult Index()
        {
            var чек_Покупки = db.Чек_Покупки.Include(ч => ч.C__Кассы).Include(ч => ч.Вид_оплаты).Include(ч => ч.Дисконтная_карта).Include(ч => ч.Кассир);
            return View(чек_Покупки.ToList());
        }
        [Authorize]
        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Чек_Покупки чек_Покупки = db.Чек_Покупки.Find(id);
            if (чек_Покупки == null)
            {
                return HttpNotFound();
            }
            return View(чек_Покупки);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            Чек_Покупки чек_Покупки = db.Чек_Покупки.Find(id);
            db.Чек_Покупки.Remove(чек_Покупки);
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
