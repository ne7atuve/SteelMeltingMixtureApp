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
    public class ComponentsController : Controller
    {
        // Dependency Injection
        // Данные поля будут хранит ссылки на реальные репозитории или на тестовые в соответствии с параметрами переданными в конструктор
        IComponentsRepository _components;
        IUserProfileRepository _users;

        public ComponentsController()
            : this(new DALContext())
        {
        }

        public ComponentsController(IDALContext context)
        {
            _components = context.Components;
            _users = context.Users;
        }

        [HttpPost]
        [MultiButton(MatchFormKey = "action", MatchFormValue = "ClearTable")]
        public ActionResult Index(Object sender)
        {
            SteelMeltingMixtureDatabase _database = new SteelMeltingMixtureDatabase();
            _database.Components.RemoveRange(_database.Components.Where(o => o.Owner.ID_User == _users.CurrentUser.ID_User));
            _database.SaveChanges();

            return RedirectToAction("Index");
        }


        [HttpPost]
        [MultiButton(MatchFormKey = "action", MatchFormValue = "LoadTable")]
        public ActionResult Index(Components components)
        {
            #region --- Ввод тестовых данных в базу данных

            Components component_1 = new Components { NameComponent = "Чугун литейный хромоникелевый", Cost = 75.5, MinValComponent = 45.0, MaxValComponent = 75.0, IsSolve = true, Owner = _users.CurrentUser };
            _components.InsertOrUpdate(component_1);
            _components.Save();

            Components component_2 = new Components { NameComponent = "Чугун литейный коксовый", Cost = 68.0, MinValComponent = 0.0, MaxValComponent = 100.0, IsSolve = true, Owner = _users.CurrentUser };
            _components.InsertOrUpdate(component_2);
            _components.Save();

            Components component_3 = new Components { NameComponent = "Чугун зеркальный", Cost = 97.0, MinValComponent = 10.0, MaxValComponent = 20.0, IsSolve = true, Owner = _users.CurrentUser };
            _components.InsertOrUpdate(component_3);
            _components.Save();

            Components component_4 = new Components { NameComponent = "Лом чугунный", Cost = 36.2, MinValComponent = 6.0, MaxValComponent = 15.0, IsSolve = true, Owner = _users.CurrentUser };
            _components.InsertOrUpdate(component_4);
            _components.Save();

            Components component_5 = new Components { NameComponent = "Лом стальной", Cost = 34.3, MinValComponent = 6.0, MaxValComponent = 15.0, IsSolve = true, Owner = _users.CurrentUser };
            _components.InsertOrUpdate(component_5);
            _components.Save();

            Components component_6 = new Components { NameComponent = "Возврат", Cost = 36.2, MinValComponent = 12.0, MaxValComponent = 40.0, IsSolve = true, Owner = _users.CurrentUser };
            _components.InsertOrUpdate(component_6);
            _components.Save();

            Components component_7 = new Components { NameComponent = "Ферромарганец", Cost = 120.0, MinValComponent = 0.0, MaxValComponent = 100.0, IsSolve = true, Owner = _users.CurrentUser };
            _components.InsertOrUpdate(component_7);
            _components.Save();

            #endregion --- Ввод тестовых данных в базу данных

            return RedirectToAction("Index");
        }


        //
        // GET: /Components/

        public ActionResult Index()
        {
            return View(_users.CurrentUser.Components.ToList());
        }

        //
        // GET: /Components/Create

        //[Authorize(Roles = "Admin")] // К данному методу действия могут получать доступ только пользователи с ролью Admin
        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Components/
        //[Authorize(Roles = "Admin")] // К данному методу действия могут получать доступ только пользователи с ролью Admin
        [HttpPost]
        public ActionResult Create(Components components)
        {
            if (ModelState.IsValid)
            {
                components.Owner = _users.CurrentUser;
                _components.InsertOrUpdate(components);
                _components.Save();
            }
            return RedirectToAction("Index");
        }

        //
        // GET: /Components/Edit/1
        //[Authorize(Roles = "Admin")] // К данному методу действия могут получать доступ только пользователи с ролью Admin
        public ActionResult Edit(int id)
        {
            return View(_components.All.FirstOrDefault(t => t.ID_Component == id));
        }

        //
        // POST: /Components/Edit/

        //[Authorize(Roles = "Admin")] // К данному методу действия могут получать доступ только пользователи с ролью Admin
        [HttpPost]
        public ActionResult Edit(Components components)
        {
            if (ModelState.IsValid)
            {
                _components.InsertOrUpdate(components);
                _components.Save();
            }
            return RedirectToAction("Index");
        }

        //
        // Edit: /Components/Delete/1
        // [Authorize(Roles = "Admin")] // К данному методу действия могут получать доступ только пользователи с ролью Admin
        public ActionResult Delete(int id)
        {
            return View(_components.All.FirstOrDefault(t => t.ID_Component == id));
        }

        //
        // POST: /Components/Delete/1
        //[Authorize(Roles = "Admin")] // К данному методу действия могут получать доступ только пользователи с ролью Admin
        [HttpPost]
        [ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            _components.Remove(_components.All.FirstOrDefault(t => t.ID_Component == id));
            _components.Save();
            return RedirectToAction("Index");
        }
    }
}