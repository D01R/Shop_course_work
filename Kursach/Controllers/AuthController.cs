using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Kursach.Models;

namespace Kursach.Controllers
{
    public class AuthController : Controller
    {
        // GET: Auth
        public ActionResult SignIn()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SignIn(LogIn Model)
        {
            if (ModelState.IsValid)
            {
                // поиск пользователя в бд
                Кассир user = null;
                using (Entities db = new Entities())
                {
                    Кассир кассир = db.Кассир.Where(o => o.Логин == Model.login).FirstOrDefault();
                    if (кассир == null) { ModelState.AddModelError("", "Пользователя с таким логином нет"); return View(Model); }

                    string hashed = FormsAuthentication.HashPasswordForStoringInConfigFile(Model.password + кассир.Соль, "SHA1");
                    user = db.Кассир.FirstOrDefault(u => u.Логин == Model.login && u.Пароль == hashed);

                }
                if (user != null)
                {
                    FormsAuthentication.SetAuthCookie(user.Логин, true);
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("", "Пользователя с таким логином и паролем нет");
                }
            }
            return View(Model);
        }
        public ActionResult SingUp()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SingUp(Registration model)
        {
            if (ModelState.IsValid)
            {
                Кассир user = null;
                using (Entities db = new Entities())
                {
                    user = db.Кассир.FirstOrDefault(u => u.Логин == model.login);
                }
                if (user == null)
                {
                    // создаем нового пользователя
                    using (Entities db = new Entities())
                    {
                        var salt = Guid.NewGuid();
                        string hashed = FormsAuthentication.HashPasswordForStoringInConfigFile(model.password + salt, "SHA1");

                        db.Кассир.Add(new Кассир { Id_Кассира = Guid.NewGuid(), Фамилия = model.Фамилия, Имя = model.Имя, Принят_на_работу = DateTime.Now, Логин = model.login, Пароль = hashed, Соль = salt.ToString() });

                        db.SaveChanges();

                        user = db.Кассир.Where(u => u.Логин == model.login && u.Пароль == hashed).FirstOrDefault();
                    }
                    // если пользователь удачно добавлен в бд
                    if (user != null)
                    {
                        FormsAuthentication.SetAuthCookie(user.Логин, true);
                        return RedirectToAction("Index", "Home");
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Пользователь с таким логином уже существует");
                }
            }
            return View(model);
        }

        public ActionResult SingOut()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
        }
    }
}