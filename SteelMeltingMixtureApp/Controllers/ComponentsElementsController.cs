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
    public class ComponentsElementsController : Controller
    {
        // Dependency Injection
        // Данные поля будут хранит ссылки на реальные репозитории или на тестовые в соответствии с параметрами переданными в конструктор
        IComponentsElementsRepository _componentsElements;
        IComponentsRepository _components;
        IElementsRepository _elements;
        IVariantsRepository _variants;
        IUserProfileRepository _users;

        public ComponentsElementsController()
            : this(new DALContext())
        {
        }

        public ComponentsElementsController(IDALContext context)
        {
            _componentsElements = context.ComponentsElements;
            _components = context.Components;
            _elements = context.Elements;
            _variants = context.Variants;
            _users = context.Users;
        }

        [HttpPost]
        [MultiButton(MatchFormKey = "action", MatchFormValue = "ClearTable")]
        public ActionResult Index(Object sender)
        {
            SteelMeltingMixtureDatabase _database = new SteelMeltingMixtureDatabase();
            _database.ComponentsElements.RemoveRange(_database.ComponentsElements.Where(o => o.Owner.ID_User == _users.CurrentUser.ID_User));
            _database.SaveChanges();

            return RedirectToAction("Index");
        }


        [HttpPost]
        [MultiButton(MatchFormKey = "action", MatchFormValue = "LoadTable")]
        public ActionResult Index(ComponentsElements componentsElements)
        {
            #region --- Ввод тестовых данных в базу данных

            // компонент 1 - "Чугун литейный хромоникелевый"
            ComponentsElements val_11 = new ComponentsElements
            {
                ID_Component = _components.All.First(p => p.NameComponent == "Чугун литейный хромоникелевый" && p.Owner.ID_User == _users.CurrentUser.ID_User).ID_Component,
                ID_Element  = _elements.All.First(p => p.NameElement == "Si" && p.Owner.ID_User == _users.CurrentUser.ID_User).ID_Element,
                ID_Variant = _variants.All.First(p => p.NameVariant == "Вариант 1" && p.Owner.ID_User == _users.CurrentUser.ID_User).ID_Variant,
                Val = 2.95,                
                IsSolve = true,
                Owner = _users.CurrentUser
            };
            _componentsElements.InsertOrUpdate(val_11);
            _componentsElements.Save();

            ComponentsElements val_12 = new ComponentsElements
            {
                ID_Component = _components.All.First(p => p.NameComponent == "Чугун литейный хромоникелевый" && p.Owner.ID_User == _users.CurrentUser.ID_User).ID_Component,
                ID_Element = _elements.All.First(p => p.NameElement == "Mn" && p.Owner.ID_User == _users.CurrentUser.ID_User).ID_Element,
                ID_Variant = _variants.All.First(p => p.NameVariant == "Вариант 1" && p.Owner.ID_User == _users.CurrentUser.ID_User).ID_Variant,
                Val = 0.55,
                IsSolve = true,
                Owner = _users.CurrentUser
            };
            _componentsElements.InsertOrUpdate(val_12);
            _componentsElements.Save();

            ComponentsElements val_13 = new ComponentsElements
            {
                ID_Component = _components.All.First(p => p.NameComponent == "Чугун литейный хромоникелевый" && p.Owner.ID_User == _users.CurrentUser.ID_User).ID_Component,
                ID_Element = _elements.All.First(p => p.NameElement == "Cr" && p.Owner.ID_User == _users.CurrentUser.ID_User).ID_Element,
                ID_Variant = _variants.All.First(p => p.NameVariant == "Вариант 1" && p.Owner.ID_User == _users.CurrentUser.ID_User).ID_Variant,
                Val = 2.45,
                IsSolve = true,
                Owner = _users.CurrentUser
            };
            _componentsElements.InsertOrUpdate(val_13);
            _componentsElements.Save();

            ComponentsElements val_14 = new ComponentsElements
            {
                ID_Component = _components.All.First(p => p.NameComponent == "Чугун литейный хромоникелевый" && p.Owner.ID_User == _users.CurrentUser.ID_User).ID_Component,
                ID_Element = _elements.All.First(p => p.NameElement == "Ni" && p.Owner.ID_User == _users.CurrentUser.ID_User).ID_Element,
                ID_Variant = _variants.All.First(p => p.NameVariant == "Вариант 1" && p.Owner.ID_User == _users.CurrentUser.ID_User).ID_Variant,
                Val = 1.20,
                IsSolve = true,
                Owner = _users.CurrentUser
            };
            _componentsElements.InsertOrUpdate(val_14);
            _componentsElements.Save();


            // компонент 2 - "Чугун литейный коксовый"
            ComponentsElements val_21 = new ComponentsElements
            {
                ID_Component = _components.All.First(p => p.NameComponent == "Чугун литейный коксовый" && p.Owner.ID_User == _users.CurrentUser.ID_User).ID_Component,
                ID_Element = _elements.All.First(p => p.NameElement == "Si" && p.Owner.ID_User == _users.CurrentUser.ID_User).ID_Element,
                ID_Variant = _variants.All.First(p => p.NameVariant == "Вариант 1" && p.Owner.ID_User == _users.CurrentUser.ID_User).ID_Variant,
                Val = 3.64,
                IsSolve = true,
                Owner = _users.CurrentUser
            };
            _componentsElements.InsertOrUpdate(val_21);
            _componentsElements.Save();

            ComponentsElements val_22 = new ComponentsElements
            {
                ID_Component = _components.All.First(p => p.NameComponent == "Чугун литейный коксовый" && p.Owner.ID_User == _users.CurrentUser.ID_User).ID_Component,
                ID_Element = _elements.All.First(p => p.NameElement == "Mn" && p.Owner.ID_User == _users.CurrentUser.ID_User).ID_Element,
                ID_Variant = _variants.All.First(p => p.NameVariant == "Вариант 1" && p.Owner.ID_User == _users.CurrentUser.ID_User).ID_Variant,
                Val = 0.78,
                IsSolve = true,
                Owner = _users.CurrentUser
            };
            _componentsElements.InsertOrUpdate(val_22);
            _componentsElements.Save();

            ComponentsElements val_23 = new ComponentsElements
            {
                ID_Component = _components.All.First(p => p.NameComponent == "Чугун литейный коксовый" && p.Owner.ID_User == _users.CurrentUser.ID_User).ID_Component,
                ID_Element = _elements.All.First(p => p.NameElement == "Cr" && p.Owner.ID_User == _users.CurrentUser.ID_User).ID_Element,
                ID_Variant = _variants.All.First(p => p.NameVariant == "Вариант 1" && p.Owner.ID_User == _users.CurrentUser.ID_User).ID_Variant,
                Val = 0.0,
                IsSolve = true,
                Owner = _users.CurrentUser
            };
            _componentsElements.InsertOrUpdate(val_23);
            _componentsElements.Save();

            ComponentsElements val_24 = new ComponentsElements
            {
                ID_Component = _components.All.First(p => p.NameComponent == "Чугун литейный коксовый" && p.Owner.ID_User == _users.CurrentUser.ID_User).ID_Component,
                ID_Element = _elements.All.First(p => p.NameElement == "Ni" && p.Owner.ID_User == _users.CurrentUser.ID_User).ID_Element,
                ID_Variant = _variants.All.First(p => p.NameVariant == "Вариант 1" && p.Owner.ID_User == _users.CurrentUser.ID_User).ID_Variant,
                Val = 0.0,
                IsSolve = true,
                Owner = _users.CurrentUser
            };
            _componentsElements.InsertOrUpdate(val_24);
            _componentsElements.Save();

            // компонент 3 - "Чугун зеркальный"
            ComponentsElements val_31 = new ComponentsElements
            {
                ID_Component = _components.All.First(p => p.NameComponent == "Чугун зеркальный" && p.Owner.ID_User == _users.CurrentUser.ID_User).ID_Component,
                ID_Element = _elements.All.First(p => p.NameElement == "Si" && p.Owner.ID_User == _users.CurrentUser.ID_User).ID_Element,
                ID_Variant = _variants.All.First(p => p.NameVariant == "Вариант 1" && p.Owner.ID_User == _users.CurrentUser.ID_User).ID_Variant,
                Val = 2.00,
                IsSolve = true,
                Owner = _users.CurrentUser
            };
            _componentsElements.InsertOrUpdate(val_31);
            _componentsElements.Save();

            ComponentsElements val_32 = new ComponentsElements
            {
                ID_Component = _components.All.First(p => p.NameComponent == "Чугун зеркальный" && p.Owner.ID_User == _users.CurrentUser.ID_User).ID_Component,
                ID_Element = _elements.All.First(p => p.NameElement == "Mn" && p.Owner.ID_User == _users.CurrentUser.ID_User).ID_Element,
                ID_Variant = _variants.All.First(p => p.NameVariant == "Вариант 1" && p.Owner.ID_User == _users.CurrentUser.ID_User).ID_Variant,
                Val = 23.4,
                IsSolve = true,
                Owner = _users.CurrentUser
            };
            _componentsElements.InsertOrUpdate(val_32);
            _componentsElements.Save();

            ComponentsElements val_33 = new ComponentsElements
            {
                ID_Component = _components.All.First(p => p.NameComponent == "Чугун зеркальный" && p.Owner.ID_User == _users.CurrentUser.ID_User).ID_Component,
                ID_Element = _elements.All.First(p => p.NameElement == "Cr" && p.Owner.ID_User == _users.CurrentUser.ID_User).ID_Element,
                ID_Variant = _variants.All.First(p => p.NameVariant == "Вариант 1" && p.Owner.ID_User == _users.CurrentUser.ID_User).ID_Variant,
                Val = 0.0,
                IsSolve = true,
                Owner = _users.CurrentUser
            };
            _componentsElements.InsertOrUpdate(val_33);
            _componentsElements.Save();

            ComponentsElements val_34 = new ComponentsElements
            {
                ID_Component = _components.All.First(p => p.NameComponent == "Чугун зеркальный" && p.Owner.ID_User == _users.CurrentUser.ID_User).ID_Component,
                ID_Element = _elements.All.First(p => p.NameElement == "Ni" && p.Owner.ID_User == _users.CurrentUser.ID_User).ID_Element,
                ID_Variant = _variants.All.First(p => p.NameVariant == "Вариант 1" && p.Owner.ID_User == _users.CurrentUser.ID_User).ID_Variant,
                Val = 0.0,
                IsSolve = true,
                Owner = _users.CurrentUser
            };
            _componentsElements.InsertOrUpdate(val_34);
            _componentsElements.Save();

            // компонент 4 - "Лом чугунный"
            ComponentsElements val_41 = new ComponentsElements
            {
                ID_Component = _components.All.First(p => p.NameComponent == "Лом чугунный" && p.Owner.ID_User == _users.CurrentUser.ID_User).ID_Component,
                ID_Element = _elements.All.First(p => p.NameElement == "Si" && p.Owner.ID_User == _users.CurrentUser.ID_User).ID_Element,
                ID_Variant = _variants.All.First(p => p.NameVariant == "Вариант 1" && p.Owner.ID_User == _users.CurrentUser.ID_User).ID_Variant,
                Val = 1.5,
                IsSolve = true,
                Owner = _users.CurrentUser
            };
            _componentsElements.InsertOrUpdate(val_41);
            _componentsElements.Save();

            ComponentsElements val_42 = new ComponentsElements
            {
                ID_Component = _components.All.First(p => p.NameComponent == "Лом чугунный" && p.Owner.ID_User == _users.CurrentUser.ID_User).ID_Component,
                ID_Element = _elements.All.First(p => p.NameElement == "Mn" && p.Owner.ID_User == _users.CurrentUser.ID_User).ID_Element,
                ID_Variant = _variants.All.First(p => p.NameVariant == "Вариант 1" && p.Owner.ID_User == _users.CurrentUser.ID_User).ID_Variant,
                Val = 0.7,
                IsSolve = true,
                Owner = _users.CurrentUser
            };
            _componentsElements.InsertOrUpdate(val_42);
            _componentsElements.Save();

            ComponentsElements val_43 = new ComponentsElements
            {
                ID_Component = _components.All.First(p => p.NameComponent == "Лом чугунный" && p.Owner.ID_User == _users.CurrentUser.ID_User).ID_Component,
                ID_Element = _elements.All.First(p => p.NameElement == "Cr" && p.Owner.ID_User == _users.CurrentUser.ID_User).ID_Element,
                ID_Variant = _variants.All.First(p => p.NameVariant == "Вариант 1" && p.Owner.ID_User == _users.CurrentUser.ID_User).ID_Variant,
                Val = 0.0,
                IsSolve = true,
                Owner = _users.CurrentUser
            };
            _componentsElements.InsertOrUpdate(val_43);
            _componentsElements.Save();

            ComponentsElements val_44 = new ComponentsElements
            {
                ID_Component = _components.All.First(p => p.NameComponent == "Лом чугунный" && p.Owner.ID_User == _users.CurrentUser.ID_User).ID_Component,
                ID_Element = _elements.All.First(p => p.NameElement == "Ni" && p.Owner.ID_User == _users.CurrentUser.ID_User).ID_Element,
                ID_Variant = _variants.All.First(p => p.NameVariant == "Вариант 1" && p.Owner.ID_User == _users.CurrentUser.ID_User).ID_Variant,
                Val = 0.0,
                IsSolve = true,
                Owner = _users.CurrentUser
            };
            _componentsElements.InsertOrUpdate(val_44);
            _componentsElements.Save();

            // компонент 5 - "Лом стальной"
            ComponentsElements val_51 = new ComponentsElements
            {
                ID_Component = _components.All.First(p => p.NameComponent == "Лом стальной" && p.Owner.ID_User == _users.CurrentUser.ID_User).ID_Component,
                ID_Element = _elements.All.First(p => p.NameElement == "Si" && p.Owner.ID_User == _users.CurrentUser.ID_User).ID_Element,
                ID_Variant = _variants.All.First(p => p.NameVariant == "Вариант 1" && p.Owner.ID_User == _users.CurrentUser.ID_User).ID_Variant,
                Val = 0.5,
                IsSolve = true,
                Owner = _users.CurrentUser
            };
            _componentsElements.InsertOrUpdate(val_51);
            _componentsElements.Save();

            ComponentsElements val_52 = new ComponentsElements
            {
                ID_Component = _components.All.First(p => p.NameComponent == "Лом стальной" && p.Owner.ID_User == _users.CurrentUser.ID_User).ID_Component,
                ID_Element = _elements.All.First(p => p.NameElement == "Mn" && p.Owner.ID_User == _users.CurrentUser.ID_User).ID_Element,
                ID_Variant = _variants.All.First(p => p.NameVariant == "Вариант 1" && p.Owner.ID_User == _users.CurrentUser.ID_User).ID_Variant,
                Val = 0.5,
                IsSolve = true,
                Owner = _users.CurrentUser
            };
            _componentsElements.InsertOrUpdate(val_52);
            _componentsElements.Save();

            ComponentsElements val_53 = new ComponentsElements
            {
                ID_Component = _components.All.First(p => p.NameComponent == "Лом стальной" && p.Owner.ID_User == _users.CurrentUser.ID_User).ID_Component,
                ID_Element = _elements.All.First(p => p.NameElement == "Cr" && p.Owner.ID_User == _users.CurrentUser.ID_User).ID_Element,
                ID_Variant = _variants.All.First(p => p.NameVariant == "Вариант 1" && p.Owner.ID_User == _users.CurrentUser.ID_User).ID_Variant,
                Val = 0.0,
                IsSolve = true,
                Owner = _users.CurrentUser
            };
            _componentsElements.InsertOrUpdate(val_53);
            _componentsElements.Save();

            ComponentsElements val_54 = new ComponentsElements
            {
                ID_Component = _components.All.First(p => p.NameComponent == "Лом стальной" && p.Owner.ID_User == _users.CurrentUser.ID_User).ID_Component,
                ID_Element = _elements.All.First(p => p.NameElement == "Ni" && p.Owner.ID_User == _users.CurrentUser.ID_User).ID_Element,
                ID_Variant = _variants.All.First(p => p.NameVariant == "Вариант 1" && p.Owner.ID_User == _users.CurrentUser.ID_User).ID_Variant,
                Val = 0.0,
                IsSolve = true,
                Owner = _users.CurrentUser
            };
            _componentsElements.InsertOrUpdate(val_54);
            _componentsElements.Save();

            // компонент 6 - "Возврат"
            ComponentsElements val_61 = new ComponentsElements
            {
                ID_Component = _components.All.First(p => p.NameComponent == "Возврат" && p.Owner.ID_User == _users.CurrentUser.ID_User).ID_Component,
                ID_Element = _elements.All.First(p => p.NameElement == "Si" && p.Owner.ID_User == _users.CurrentUser.ID_User).ID_Element,
                ID_Variant = _variants.All.First(p => p.NameVariant == "Вариант 1" && p.Owner.ID_User == _users.CurrentUser.ID_User).ID_Variant,
                Val = 0.4,
                IsSolve = true,
                Owner = _users.CurrentUser
            };
            _componentsElements.InsertOrUpdate(val_61);
            _componentsElements.Save();

            ComponentsElements val_62 = new ComponentsElements
            {
                ID_Component = _components.All.First(p => p.NameComponent == "Возврат" && p.Owner.ID_User == _users.CurrentUser.ID_User).ID_Component,
                ID_Element = _elements.All.First(p => p.NameElement == "Mn" && p.Owner.ID_User == _users.CurrentUser.ID_User).ID_Element,
                ID_Variant = _variants.All.First(p => p.NameVariant == "Вариант 1" && p.Owner.ID_User == _users.CurrentUser.ID_User).ID_Variant,
                Val = 0.65,
                IsSolve = true,
                Owner = _users.CurrentUser
            };
            _componentsElements.InsertOrUpdate(val_62);
            _componentsElements.Save();

            ComponentsElements val_63 = new ComponentsElements
            {
                ID_Component = _components.All.First(p => p.NameComponent == "Возврат" && p.Owner.ID_User == _users.CurrentUser.ID_User).ID_Component,
                ID_Element = _elements.All.First(p => p.NameElement == "Cr" && p.Owner.ID_User == _users.CurrentUser.ID_User).ID_Element,
                ID_Variant = _variants.All.First(p => p.NameVariant == "Вариант 1" && p.Owner.ID_User == _users.CurrentUser.ID_User).ID_Variant,
                Val = 0.0,
                IsSolve = true,
                Owner = _users.CurrentUser
            };
            _componentsElements.InsertOrUpdate(val_63);
            _componentsElements.Save();

            ComponentsElements val_64 = new ComponentsElements
            {
                ID_Component = _components.All.First(p => p.NameComponent == "Возврат" && p.Owner.ID_User == _users.CurrentUser.ID_User).ID_Component,
                ID_Element = _elements.All.First(p => p.NameElement == "Ni" && p.Owner.ID_User == _users.CurrentUser.ID_User).ID_Element,
                ID_Variant = _variants.All.First(p => p.NameVariant == "Вариант 1" && p.Owner.ID_User == _users.CurrentUser.ID_User).ID_Variant,
                Val = 0.0,
                IsSolve = true,
                Owner = _users.CurrentUser
            };
            _componentsElements.InsertOrUpdate(val_64);
            _componentsElements.Save();

            // компонент 7 - "Ферромарганец"
            ComponentsElements val_71 = new ComponentsElements
            {
                ID_Component = _components.All.First(p => p.NameComponent == "Ферромарганец" && p.Owner.ID_User == _users.CurrentUser.ID_User).ID_Component,
                ID_Element = _elements.All.First(p => p.NameElement == "Si" && p.Owner.ID_User == _users.CurrentUser.ID_User).ID_Element,
                ID_Variant = _variants.All.First(p => p.NameVariant == "Вариант 1" && p.Owner.ID_User == _users.CurrentUser.ID_User).ID_Variant,
                Val = 2.0,
                IsSolve = true,
                Owner = _users.CurrentUser
            };
            _componentsElements.InsertOrUpdate(val_71);
            _componentsElements.Save();

            ComponentsElements val_72 = new ComponentsElements
            {
                ID_Component = _components.All.First(p => p.NameComponent == "Ферромарганец" && p.Owner.ID_User == _users.CurrentUser.ID_User).ID_Component,
                ID_Element = _elements.All.First(p => p.NameElement == "Mn" && p.Owner.ID_User == _users.CurrentUser.ID_User).ID_Element,
                ID_Variant = _variants.All.First(p => p.NameVariant == "Вариант 1" && p.Owner.ID_User == _users.CurrentUser.ID_User).ID_Variant,
                Val = 84.5,
                IsSolve = true,
                Owner = _users.CurrentUser
            };
            _componentsElements.InsertOrUpdate(val_72);
            _componentsElements.Save();

            ComponentsElements val_73 = new ComponentsElements
            {
                ID_Component = _components.All.First(p => p.NameComponent == "Ферромарганец" && p.Owner.ID_User == _users.CurrentUser.ID_User).ID_Component,
                ID_Element = _elements.All.First(p => p.NameElement == "Cr" && p.Owner.ID_User == _users.CurrentUser.ID_User).ID_Element,
                ID_Variant = _variants.All.First(p => p.NameVariant == "Вариант 1" && p.Owner.ID_User == _users.CurrentUser.ID_User).ID_Variant,
                Val = 0.0,
                IsSolve = true,
                Owner = _users.CurrentUser
            };
            _componentsElements.InsertOrUpdate(val_73);
            _componentsElements.Save();

            ComponentsElements val_74 = new ComponentsElements
            {
                ID_Component = _components.All.First(p => p.NameComponent == "Ферромарганец" && p.Owner.ID_User == _users.CurrentUser.ID_User).ID_Component,
                ID_Element = _elements.All.First(p => p.NameElement == "Ni" && p.Owner.ID_User == _users.CurrentUser.ID_User).ID_Element,
                ID_Variant = _variants.All.First(p => p.NameVariant == "Вариант 1" && p.Owner.ID_User == _users.CurrentUser.ID_User).ID_Variant,
                Val = 0.0,
                IsSolve = true,
                Owner = _users.CurrentUser
            };
            _componentsElements.InsertOrUpdate(val_74);
            _componentsElements.Save();

            #endregion --- Ввод тестовых данных в базу данных

            return RedirectToAction("Index");
        }


        //
        // GET: /ComponentsElements/

        public ActionResult Index()
        {
            //ViewBag.ID_Variant = new SelectList(_variants.All.Where(t => t.Owner.ID_User == _users.CurrentUser.ID_User), "ID_Variant", "NameVariant");
            return View(_users.CurrentUser.ComponentsElements.ToList());
        }

        ////
        //// POST: /ComponentsElements/

        //[HttpPost]
        //public ActionResult Index(Variants variants)
        //{
        //    if (variants.ID_Variant == 0) // если выбран элемент списка "Все", который прописан при формировании выпадающего списка в представлении Index
        //    {
        //        return RedirectToAction("Index");
        //    }
        //    ViewBag.ID_Variant = new SelectList(_variants.All.Where(t => t.Owner.ID_User == _users.CurrentUser.ID_User), "ID_Variant", "NameVariant");
        //    return View(_users.CurrentUser.ComponentsElements.Where((t => t.ID_Variant == variants.ID_Variant)).ToList());
        //}

        //
        // GET: /ComponentsElements/Create

        //[Authorize(Roles = "Admin")] // К данному методу действия могут получать доступ только пользователи с ролью Admin
        public ActionResult Create()
        {
            ViewBag.ID_Component = new SelectList(_components.All.Where(t => t.Owner.ID_User == _users.CurrentUser.ID_User), "ID_Component", "NameComponent");
            ViewBag.ID_Element = new SelectList(_elements.All.Where(t => t.Owner.ID_User == _users.CurrentUser.ID_User), "ID_Element", "NameElement");
            ViewBag.ID_Variant = new SelectList(_variants.All.Where(t => t.Owner.ID_User == _users.CurrentUser.ID_User), "ID_Variant", "NameVariant");
            return View();
        }

        //
        // POST: /ComponentsElements/
        //[Authorize(Roles = "Admin")] // К данному методу действия могут получать доступ только пользователи с ролью Admin
        [HttpPost]
        public ActionResult Create(ComponentsElements componentsElements)
        {
            if (ModelState.IsValid)
            {
                componentsElements.Owner = _users.CurrentUser;
                _componentsElements.InsertOrUpdate(componentsElements);
                _componentsElements.Save();
            }
            return RedirectToAction("Index");
        }

        //
        // GET: /ComponentsElements/Edit/1
        //[Authorize(Roles = "Admin")] // К данному методу действия могут получать доступ только пользователи с ролью Admin
        public ActionResult Edit(int id)
        {
            ViewBag.ID_Component = new SelectList(_components.All.Where(t => t.Owner.ID_User == _users.CurrentUser.ID_User), "ID_Component", "NameComponent");
            ViewBag.ID_Element = new SelectList(_elements.All.Where(t => t.Owner.ID_User == _users.CurrentUser.ID_User), "ID_Element", "NameElement");
            ViewBag.ID_Variant = new SelectList(_variants.All.Where(t => t.Owner.ID_User == _users.CurrentUser.ID_User), "ID_Variant", "NameVariant");
            return View(_componentsElements.All.FirstOrDefault(t => t.ID_ComponentElements == id));
        }

        //
        // POST: /ComponentsElements/Edit/

        //[Authorize(Roles = "Admin")] // К данному методу действия могут получать доступ только пользователи с ролью Admin
        [HttpPost]
        public ActionResult Edit(ComponentsElements componentsElements)
        {
            if (ModelState.IsValid)
            {
                _componentsElements.InsertOrUpdate(componentsElements);
                _componentsElements.Save();
            }
            return RedirectToAction("Index");
        }

        //
        // Edit: /ComponentsElements/Delete/1
        // [Authorize(Roles = "Admin")] // К данному методу действия могут получать доступ только пользователи с ролью Admin
        public ActionResult Delete(int id)
        {
            return View(_componentsElements.All.FirstOrDefault(t => t.ID_ComponentElements == id));
        }

        //
        // POST: /ComponentsElements/Delete/1
        //[Authorize(Roles = "Admin")] // К данному методу действия могут получать доступ только пользователи с ролью Admin
        [HttpPost]
        [ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            _componentsElements.Remove(_componentsElements.All.FirstOrDefault(t => t.ID_ComponentElements == id));
            _componentsElements.Save();
            return RedirectToAction("Index");
        }
    }
}