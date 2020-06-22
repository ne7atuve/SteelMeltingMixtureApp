using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Xml;
using System.Xml.Serialization;
using SteelMeltingMixture.Domain;
using SteelMeltingMixtureApp.Infrastructure;
//using System.Runtime.Serialization.Formatters.Soap;
using SteelMeltingMixtureApp.Models;
using Excel = Microsoft.Office.Interop.Excel;

namespace SteelMeltingMixtureApp.Controllers
{   
    public class HomeController : Controller
    {
        IUserProfileRepository _users;
        IComponentsRepository _components;
        IElementsRepository _elements;
        IComponentsElementsRepository  _сomponentsElements;
        IVariantsRepository _variants;

        public HomeController()
            : this(new DALContext())
        {
        }

        public HomeController(IDALContext context)
        {   
            _users = context.Users;
            _components = context.Components;
            _elements = context.Elements;
            _сomponentsElements = context.ComponentsElements;
            _variants = context.Variants;
        }

        public ActionResult Index()
        {
            return View();
        }

        [Authorize] // Запрещены анонимные обращения к данной странице
        public ActionResult Cabinet()
        {
            ViewBag.Message = "Личная страница.";

            return View();
        }

        [Authorize] // Запрещены анонимные обращения к данной странице        
        public ActionResult Demo()
        {
            InputDataModel input_val = new InputDataModel();
            return View(input_val);
        }


        [Authorize] // Запрещены анонимные обращения к данной странице        
        public ActionResult Universal()
        {            
            return View();
        }

        [Authorize] // Запрещены анонимные обращения к данной странице
        public ActionResult RezultUniversal()
        {
            SteelMeltingMixtureDatabase _database = new SteelMeltingMixtureDatabase();
     
            List<Components> components = _database.Components.Where(p => p.Owner.ID_User == _users.CurrentUser.ID_User && p.IsSolve == true).ToList();
            List<Elements> elements = _database.Elements.Where(p => p.Owner.ID_User == _users.CurrentUser.ID_User && p.IsSolve == true).ToList();
            List<ComponentsElements> componentsElements = _database.ComponentsElements.Where(p => p.Owner.ID_User == _users.CurrentUser.ID_User && p.IsSolve == true).ToList();
              
            SolverUniversalModel sm = new SolverUniversalModel(
                _users,
                components,
                elements,
                componentsElements
                );

            // максимальное количество методов раскроя заготовок
            int _maxComponents = _database.Components.Count(o => o.Owner.ID_User == _users.CurrentUser.ID_User && o.IsSolve == true);

            double[] val = new double[_maxComponents + 1];
            var db_variant = _database.Variants.Where(p => p.Owner.ID_User == _users.CurrentUser.ID_User).FirstOrDefault();

            val = sm.SolverSteelMeltingMixture(db_variant);

            // ! Save input data to Session
            //Session["input_data"] = input_data;

            List<double> lst = val.OfType<double>().ToList();
            ViewBag.Rashet = lst;

            //return View(input_data);
            return View();
        }

        [Authorize] // Запрещены анонимные обращения к данной странице
        public ActionResult RezultDemo(InputDataModel input_data)
        {
            SolverDemoModel sm = new SolverDemoModel();
            double[] val = new double[10];
            val = sm.SolverSteelMeltingMixture(input_data);

            // ! Save input data to Session
            Session["input_data"] = input_data;          

            List<double> lst = val.OfType<double>().ToList();
            ViewBag.Rashet = lst;

            return View(input_data);
        }
       

        public ActionResult About()
        {
            ViewBag.Message = "Расчет оптимального состава сталеплавильной шихты. Демонстрационный пример";
            ViewBag.Assembly = "Версия " + Assembly.GetExecutingAssembly().GetName().Version.ToString();
            
            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Лавров Владислав Васильевич";

            return View();
        }

        [Authorize] // Запрещены анонимные обращения к данной странице
        public ActionResult Excel()
        {
            ViewBag.Result = "Файл успешно сохранен!";            

            // ! Get input data from Session
            InputDataModel input_data = (InputDataModel)Session["input_data"];

            SolverDemoModel sm = new SolverDemoModel();
            double[] val = new double[10];
            val = sm.SolverSteelMeltingMixture(input_data);           

            List<double> lst = val.OfType<double>().ToList();
            ViewBag.Rashet = lst;

            try
            {
                string dataTimeNow = DateTime.Now.ToString("dd MMMM yyyy HH-mm-ss");
                ViewBag.Result = dataTimeNow;

                #region --- Формирование файла Excel с результатами решения задачи
                Excel.Application application = new Excel.Application();
                Excel.Workbook workBook = application.Workbooks.Add(System.Reflection.Missing.Value);
                Excel.Worksheet worksheet = workBook.ActiveSheet;
                worksheet.Cells[1, 1] = "Оптимизация сталеплавильной шихты";
                worksheet.Cells[2, 1] = "Дата расчета: " +  ViewBag.Result;


                worksheet.Cells[4, 1] = "Исходные данные";
                worksheet.Cells[5, 1] = "Состав и стоимость компонентов шихты";
                worksheet.Cells[6, 1] = "Компонент шихты";
                worksheet.Cells[6, 2] = "Si";
                worksheet.Cells[6, 3] = "Mn";
                worksheet.Cells[6, 4] = "Cr";
                worksheet.Cells[6, 5] = "Ni";
                worksheet.Cells[6, 6] = "Стоимость, у.е./т";
                
                worksheet.Cells[7, 1] = "Чугун литейный хромоникелевый";
                worksheet.Cells[7, 2] = input_data.Si_InComponent_1.ToString();
                worksheet.Cells[7, 3] = input_data.Mn_InComponent_1.ToString();
                worksheet.Cells[7, 4] = input_data.Cr_InComponent_1.ToString();
                worksheet.Cells[7, 5] = input_data.Ni_InComponent_1.ToString();
                worksheet.Cells[7, 6] = input_data.CostComponent_1.ToString();

                worksheet.Cells[8, 1] = "Чугун литейный коксовый";
                worksheet.Cells[8, 2] = input_data.Si_InComponent_2.ToString();
                worksheet.Cells[8, 3] = input_data.Mn_InComponent_2.ToString();
                worksheet.Cells[8, 4] = input_data.Cr_InComponent_2.ToString();
                worksheet.Cells[8, 5] = input_data.Ni_InComponent_2.ToString();
                worksheet.Cells[8, 6] = input_data.CostComponent_2.ToString();

                worksheet.Cells[9, 1] = "Чугун зеркальный";
                worksheet.Cells[9, 2] = input_data.Si_InComponent_3.ToString();
                worksheet.Cells[9, 3] = input_data.Mn_InComponent_3.ToString();
                worksheet.Cells[9, 4] = input_data.Cr_InComponent_3.ToString();
                worksheet.Cells[9, 5] = input_data.Ni_InComponent_3.ToString();
                worksheet.Cells[9, 6] = input_data.CostComponent_3.ToString();

                worksheet.Cells[10, 1] = "Лом чугунный";
                worksheet.Cells[10, 2] = input_data.Si_InComponent_4.ToString();
                worksheet.Cells[10, 3] = input_data.Mn_InComponent_4.ToString();
                worksheet.Cells[10, 4] = input_data.Cr_InComponent_4.ToString();
                worksheet.Cells[10, 5] = input_data.Ni_InComponent_4.ToString();
                worksheet.Cells[10, 6] = input_data.CostComponent_4.ToString();

                worksheet.Cells[11, 1] = "Лом стальной";
                worksheet.Cells[11, 2] = input_data.Si_InComponent_5.ToString();
                worksheet.Cells[11, 3] = input_data.Mn_InComponent_5.ToString();
                worksheet.Cells[11, 4] = input_data.Cr_InComponent_5.ToString();
                worksheet.Cells[11, 5] = input_data.Ni_InComponent_5.ToString();
                worksheet.Cells[11, 6] = input_data.CostComponent_5.ToString();

                worksheet.Cells[12, 1] = "Возврат";
                worksheet.Cells[12, 2] = input_data.Si_InComponent_6.ToString();
                worksheet.Cells[12, 3] = input_data.Mn_InComponent_6.ToString();
                worksheet.Cells[12, 4] = input_data.Cr_InComponent_6.ToString();
                worksheet.Cells[12, 5] = input_data.Ni_InComponent_6.ToString();
                worksheet.Cells[12, 6] = input_data.CostComponent_6.ToString();

                worksheet.Cells[13, 1] = "Ферромарганец";
                worksheet.Cells[13, 2] = input_data.Si_InComponent_7.ToString();
                worksheet.Cells[13, 3] = input_data.Mn_InComponent_7.ToString();
                worksheet.Cells[13, 4] = input_data.Cr_InComponent_7.ToString();
                worksheet.Cells[13, 5] = input_data.Ni_InComponent_7.ToString();
                worksheet.Cells[13, 6] = input_data.CostComponent_7.ToString();

                worksheet.Cells[14, 1] = "Ограничения на содержание компонентов шихты, % от общего веса шихты";
                worksheet.Cells[15, 1] = "Компонент шихты";
                worksheet.Cells[15, 2] = "Минимум";
                worksheet.Cells[15, 3] = "Максимум";

                worksheet.Cells[16, 1] = "Чугун литейный хромоникелевый";
                worksheet.Cells[16, 2] = input_data.ComponentMin_1.ToString();
                worksheet.Cells[16, 3] = input_data.ComponentMax_1.ToString();

                worksheet.Cells[17, 1] = "Чугун литейный коксовый";
                worksheet.Cells[17, 2] = input_data.ComponentMin_2.ToString();
                worksheet.Cells[17, 3] = input_data.ComponentMax_2.ToString();

                worksheet.Cells[18, 1] = "Чугун зеркальный";
                worksheet.Cells[18, 2] = input_data.ComponentMin_3.ToString();
                worksheet.Cells[18, 3] = input_data.ComponentMax_3.ToString();

                worksheet.Cells[19, 1] = "Лом чугунный";
                worksheet.Cells[19, 2] = input_data.ComponentMin_4.ToString();
                worksheet.Cells[19, 3] = input_data.ComponentMax_4.ToString();

                worksheet.Cells[20, 1] = "Лом стальной";
                worksheet.Cells[20, 2] = input_data.ComponentMin_5.ToString();
                worksheet.Cells[20, 3] = input_data.ComponentMax_5.ToString();

                worksheet.Cells[21, 1] = "Возврат";
                worksheet.Cells[21, 2] = input_data.ComponentMin_6.ToString();
                worksheet.Cells[21, 3] = input_data.ComponentMax_6.ToString();

                worksheet.Cells[22, 1] = "Ферромарганец";
                worksheet.Cells[22, 2] = input_data.ComponentMin_7.ToString();
                worksheet.Cells[22, 3] = input_data.ComponentMax_7.ToString();

                worksheet.Cells[23, 1] = "Ограничения на содержание химических элеметов в шихте, % масс.";
                worksheet.Cells[24, 1] = "Элемент";
                worksheet.Cells[24, 2] = "Минимум";
                worksheet.Cells[24, 3] = "Максимум";

                worksheet.Cells[25, 1] = "[Si]";
                worksheet.Cells[25, 2] = input_data.Si_InShihtaNeed_Min.ToString();
                worksheet.Cells[25, 3] = input_data.Si_InShihtaNeed_Max.ToString();

                worksheet.Cells[26, 1] = "[Mn]";
                worksheet.Cells[26, 2] = input_data.Mn_InShihtaNeed_Min.ToString();
                worksheet.Cells[26, 3] = input_data.Mn_InShihtaNeed_Max.ToString();

                worksheet.Cells[27, 1] = "[Cr]";
                worksheet.Cells[27, 2] = input_data.Cr_InShihtaNeed_Min.ToString();
                worksheet.Cells[27, 3] = input_data.Cr_InShihtaNeed_Max.ToString();

                worksheet.Cells[28, 1] = "[Ni]";
                worksheet.Cells[28, 2] = input_data.Ni_InShihtaNeed_Min.ToString();
                worksheet.Cells[28, 3] = input_data.Ni_InShihtaNeed_Max.ToString();

                worksheet.Cells[29, 1] = "Решение задачи";
                worksheet.Cells[30, 1] = "Оптимальный состав сталеплавильной шихты";
                worksheet.Cells[31, 1] = "Компонент шихты";
                worksheet.Cells[31, 2] = "Величина, % от общего веса шихты";

                worksheet.Cells[32, 1] = "Чугун литейный хромоникелевый";
                worksheet.Cells[32, 2] = Math.Round(ViewBag.Rashet[0], 2).ToString();

                worksheet.Cells[33, 1] = "Чугун литейный коксовый";
                worksheet.Cells[33, 2] = Math.Round(ViewBag.Rashet[1], 2).ToString();

                worksheet.Cells[34, 1] = "Чугун зеркальный";
                worksheet.Cells[34, 2] = Math.Round(ViewBag.Rashet[2], 2).ToString();

                worksheet.Cells[35, 1] = "Лом чугунный";
                worksheet.Cells[35, 2] = Math.Round(ViewBag.Rashet[3], 2).ToString();

                worksheet.Cells[36, 1] = "Лом стальной";
                worksheet.Cells[36, 2] = Math.Round(ViewBag.Rashet[4], 2).ToString();

                worksheet.Cells[37, 1] = "Возврат";
                worksheet.Cells[37, 2] = Math.Round(ViewBag.Rashet[5], 2).ToString();

                worksheet.Cells[38, 1] = "Ферромарганец";
                worksheet.Cells[38, 2] = Math.Round(ViewBag.Rashet[6], 2).ToString();


                String excelFileName = Server.MapPath("~/Content") + "\\SteelMeltingMixture.xlsx";

                if (System.IO.File.Exists(excelFileName))
                {
                    System.IO.File.Delete(excelFileName);
                }

                // ! Save path & filename
                workBook.SaveAs(excelFileName);

                workBook.Close(false, Type.Missing, Type.Missing);
                Marshal.ReleaseComObject(workBook);
                application.Quit();
                Marshal.FinalReleaseComObject(application);

                // ! Redirect to download file
                Response.RedirectPermanent("/Content/SteelMeltingMixture.xlsx");

                #endregion --- Формирование файла Excel с результатами решения задачи
            }
            catch (Exception e)
            {
                ViewBag.Result = "Невозможно сохранить файл (" + e.Message + ").";
            }
            return View();
        }

    }
}