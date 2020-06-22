using SteelMeltingMixture.Domain;
using SteelMeltingMixtureApp.Infrastructure;
using SteelMeltingMixtureApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SteelMeltingMixtureApp.Controllers
{
    [Authorize] // К контроллеру получают доступ только аутентифицированные пользователи.
    public class ElementsController : Controller
    {
        // Dependency Injection
        // Данные поля будут хранит ссылки на реальные репозитории или на тестовые в соответствии с параметрами переданными в конструктор
        IElementsRepository _elements;
        IUserProfileRepository _users;

        public ElementsController()
            : this(new DALContext())
        {
        }

        public ElementsController(IDALContext context)
        {
            _elements = context.Elements;
            _users = context.Users;
        }

        [HttpPost]
        [MultiButton(MatchFormKey = "action", MatchFormValue = "ClearTable")]
        public ActionResult Index(Object sender)
        {
            SteelMeltingMixtureDatabase _database = new SteelMeltingMixtureDatabase();
            _database.Elements.RemoveRange(_database.Elements.Where(o => o.Owner.ID_User == _users.CurrentUser.ID_User));
            _database.SaveChanges();

            return RedirectToAction("Index");
        }


        [HttpPost]
        [MultiButton(MatchFormKey = "action", MatchFormValue = "LoadTable")]
        public ActionResult Index(Elements elements)
        {
            #region --- Ввод тестовых данных в базу данных

            Elements element_1 = new Elements { NameElement = "Si", MinValElement = 2.30, MaxValElement = 2.60, IsSolve = true, Owner = _users.CurrentUser };
            _elements.InsertOrUpdate(element_1);
            _elements.Save();

            Elements element_2 = new Elements { NameElement = "Mn", MinValElement = 0.37, MaxValElement = 3.10, IsSolve = true, Owner = _users.CurrentUser };
            _elements.InsertOrUpdate(element_2);
            _elements.Save();

            Elements element_3 = new Elements { NameElement = "Cr", MinValElement = 1.40, MaxValElement = 3.40, IsSolve = true, Owner = _users.CurrentUser };
            _elements.InsertOrUpdate(element_3);
            _elements.Save();

            Elements element_4 = new Elements { NameElement = "Ni", MinValElement = 0.70, MaxValElement = 1.50, IsSolve = true, Owner = _users.CurrentUser };
            _elements.InsertOrUpdate(element_4);
            _elements.Save();

            #endregion --- Ввод тестовых данных в базу данных

            return RedirectToAction("Index");
        }

        //
        // GET: /Elements/

        public ActionResult Index()
        {
            return View(_users.CurrentUser.Elements.ToList());
        }

        //
        // GET: /Elements/Create

        //[Authorize(Roles = "Admin")] // К данному методу действия могут получать доступ только пользователи с ролью Admin
        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Elements/
        //[Authorize(Roles = "Admin")] // К данному методу действия могут получать доступ только пользователи с ролью Admin
        [HttpPost]
        public ActionResult Create(Elements elements)
        {
            if (ModelState.IsValid)
            {
                elements.Owner = _users.CurrentUser;
                _elements.InsertOrUpdate(elements);
                _elements.Save();
            }
            return RedirectToAction("Index");
        }

        //
        // GET: /Elements/Edit/1
        //[Authorize(Roles = "Admin")] // К данному методу действия могут получать доступ только пользователи с ролью Admin
        public ActionResult Edit(int id)
        {
            return View(_elements.All.FirstOrDefault(t => t.ID_Element == id));
        }

        //
        // POST: /Elements/Edit/

        //[Authorize(Roles = "Admin")] // К данному методу действия могут получать доступ только пользователи с ролью Admin
        [HttpPost]
        public ActionResult Edit(Elements elements)
        {
            if (ModelState.IsValid)
            {
                _elements.InsertOrUpdate(elements);
                _elements.Save();
            }
            return RedirectToAction("Index");
        }

        //
        // Edit: /Elements/Delete/1
        // [Authorize(Roles = "Admin")] // К данному методу действия могут получать доступ только пользователи с ролью Admin
        public ActionResult Delete(int id)
        {
            return View(_elements.All.FirstOrDefault(t => t.ID_Element == id));
        }

        //
        // POST: /Elements/Delete/1
        //[Authorize(Roles = "Admin")] // К данному методу действия могут получать доступ только пользователи с ролью Admin
        [HttpPost]
        [ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            _elements.Remove(_elements.All.FirstOrDefault(t => t.ID_Element == id));
            _elements.Save();
            return RedirectToAction("Index");
        }
    }
}