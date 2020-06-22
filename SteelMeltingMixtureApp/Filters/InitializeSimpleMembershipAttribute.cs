using SteelMeltingMixtureApp.Infrastructure;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using WebMatrix.WebData;

namespace SteelMeltingMixtureApp.Filters
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
    public sealed class InitializeSimpleMembershipAttribute : ActionFilterAttribute
    {
        private static SimpleMembershipInitializer _initializer;
        private static object _initializerLock = new object();
        private static bool _isInitialized;

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            // Инициализация ASP.NET Simple Membership происходит один раз при старте приложения
            LazyInitializer.EnsureInitialized(ref _initializer, ref _isInitialized, ref _initializerLock);
        }

        private class SimpleMembershipInitializer
        {
            public SimpleMembershipInitializer()
            {
                Database.SetInitializer<SteelMeltingMixtureDatabase>(null);

                try
                {
                    using (var context = new SteelMeltingMixtureDatabase())
                    {
                        if (!context.Database.Exists())
                        {
                            // Создание базы данных SimpleMembership без применения миграции Entity Framework
                            ((IObjectContextAdapter)context).ObjectContext.CreateDatabase();
                        }
                    }
                    // Настройка  ASP.NET Simple Membership
                    // 1 параметр - имя строки подключения к базе данных.
                    // 2 параметр - таблица, которая содержит информацию о пользователях
                    // 3 параметр - имя колонки в таблице, которая отвечает за хранение логина
                    // 4 параметр - autoCreateTables автоматическое создание таблиц если они не существуют в базе
                    WebSecurity.InitializeDatabaseConnection("SteelMeltingMixtureConnection", "UserProfile", "ID_User", "UserName", autoCreateTables: true);

                    SimpleRoleProvider roles = (SimpleRoleProvider)Roles.Provider;
                    SimpleMembershipProvider membership = (SimpleMembershipProvider)Membership.Provider;

                    // Проверка наличия роли Admin
                    if (!roles.RoleExists("Admin"))
                    {
                        roles.CreateRole("Admin");
                    }

                    // Проверка наличия роли Operator
                    if (!roles.RoleExists("Operator"))
                    {
                        roles.CreateRole("Operator");
                    }

                    // Поиск пользователя с логином admin для роли Admin
                    if (membership.GetUser("admin", false) == null)
                    {
                        membership.CreateUserAndAccount("admin", "qwe123"); // создание пользователя
                        roles.AddUsersToRoles(new[] { "admin" }, new[] { "Admin" }); // установка роли для пользователя
                    }

                    //// Поиск пользователя с логином user1 для роли Operator
                    //if (membership.GetUser("user1", false) == null)
                    //{
                    //    membership.CreateUserAndAccount("user1", "qwe123"); // создание пользователя
                    //    roles.AddUsersToRoles(new[] { "user1" }, new[] { "Operator" }); // установка роли для пользователя
                    //}
                }
                catch (Exception ex)
                {
                    throw new InvalidOperationException("The ASP.NET Simple Membership database could not be initialized. For more information, please see http://go.microsoft.com/fwlink/?LinkId=256588", ex);
                }
            }
        }
    }
}