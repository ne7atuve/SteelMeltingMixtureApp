using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
//using System.Web.Mvc;
using Microsoft.SolverFoundation.Services;
using SteelMeltingMixture.Domain;
using SteelMeltingMixtureApp.Infrastructure;

namespace SteelMeltingMixtureApp.Models
{
    
    public class SolverUniversalModel
    {
        InputDataModel _idm = new InputDataModel();

        private int _maxComponents; // максимальное количество компонентов шихты
        private int _maxElements; // максимальное количество химических элементов

        IUserProfileRepository _users;   
        List<Components> _components;
        List<Elements> _elements;
        List<ComponentsElements> _сomponentsElements;
        Variants _variants;


        public SolverUniversalModel() {}

        /// <summary>
        /// Перегрузили конструктор
        /// </summary>
        public SolverUniversalModel(
            IUserProfileRepository Users,
            List<Components> Components,
            List<Elements> Elements,
            List<ComponentsElements> ComponentsElements    )
        {
            _users = Users;
            _components = Components;
            _maxComponents = _components.Count();
            _elements = Elements;
            _maxElements = _elements.Count();
            _сomponentsElements = ComponentsElements;           
        }

        public double[] SolverSteelMeltingMixture (Variants variants)       
        {
           _variants = variants;

            #region --- Решение с использованием Solver Foundation

            SteelMeltingMixtureDatabase _database = new SteelMeltingMixtureDatabase();
            List<SolverRowModel> solverList = new List<SolverRowModel>();
            double[] mKoefNerav = new double[_components.Count];

            // заполнить списки
            int countItemComponents = 0;
            foreach (Components itemComponents in _components)
            {      
                int countItemElements = 0;
                foreach (Elements itemElements in _elements)
                {
                    var db_ValElement = _database.ComponentsElements
                            .Where(p => p.ID_Component == itemComponents.ID_Component
                            && p.ID_Element == itemElements.ID_Element
                            && p.ID_Variant == _variants.ID_Variant
                            && p.Owner.ID_User == _users.CurrentUser.ID_User
                            && p.IsSolve == true)
                            .FirstOrDefault();
                    mKoefNerav[countItemElements] = 0.01 * db_ValElement.Val;                     
                    countItemElements++;
                }

                var db_Components = _database.Components
                        .Where(p => p.ID_Component == itemComponents.ID_Component                       
                        && p.Owner.ID_User == _users.CurrentUser.ID_User
                        && p.IsSolve == true)
                        .FirstOrDefault();

                switch (_maxElements)
                {
                    case 2:

                        solverList.Add(new SolverRowModel
                        {
                            xId = countItemComponents + 1,
                            Koef = 0.01 * db_Components.Cost,
                            Min = db_Components.MinValComponent,
                            Max = db_Components.MaxValComponent,
                            KoefNerav1 = mKoefNerav[0],
                            KoefNerav2 = mKoefNerav[1]
                        });

                        break;

                    case 3:
                        solverList.Add(new SolverRowModel
                        {
                            xId = countItemComponents + 1,
                            Koef = 0.01 * db_Components.Cost,
                            Min = db_Components.MinValComponent,
                            Max = db_Components.MaxValComponent,
                            KoefNerav1 = mKoefNerav[0],
                            KoefNerav2 = mKoefNerav[1],
                            KoefNerav3 = mKoefNerav[2],
                        });
                        break;

                    case 4:
                        solverList.Add(new SolverRowModel
                        {
                            xId = countItemComponents + 1,
                            Koef = 0.01 * db_Components.Cost,
                            Min = db_Components.MinValComponent,
                            Max = db_Components.MaxValComponent,
                            KoefNerav1 = mKoefNerav[0],
                            KoefNerav2 = mKoefNerav[1],
                            KoefNerav3 = mKoefNerav[2],
                            KoefNerav4 = mKoefNerav[3],
                        });
                        break;

                    default:
                        break;
                }
                countItemComponents++;
            }           

            SolverContext context = SolverContext.GetContext();
            Model model = context.CreateModel();

            Set users = new Set(Domain.Any, "users");

            Parameter k = new Parameter(Domain.Real, "Koef", users);
            k.SetBinding(solverList, "Koef", "xId");

            Parameter min = new Parameter(Domain.Real, "Min", users);
            min.SetBinding(solverList, "Min", "xId");

            Parameter max = new Parameter(Domain.Real, "Max", users);
            max.SetBinding(solverList, "Max", "xId");

            Parameter[] KoefNerav = new Parameter[_maxElements];

            for (int i = 0; i < KoefNerav.Length; i++)
            {
                KoefNerav[i] = new Parameter(Microsoft.SolverFoundation.Services.Domain.Real, "KoefNerav" + (i + 1).ToString(), users);
                KoefNerav[i].SetBinding(solverList, "KoefNerav" + (i + 1).ToString(), "xId");
            }

            Parameter[] KoefNeravRows = new Parameter[3 + _maxElements];
            KoefNeravRows[0] = k;
            KoefNeravRows[1] = min;
            KoefNeravRows[2] = max;
            for (int i = 3; i < KoefNeravRows.Length; i++) // заполняем, начиная с 3 элемента 
            {
                KoefNeravRows[i] = KoefNerav[i - 3];
            }

            model.AddParameters(KoefNeravRows);

            Decision choose = new Decision(Domain.RealNonnegative, "choose", users);
            model.AddDecision(choose);

            model.AddGoal("goal", GoalKind.Minimize, Model.Sum(Model.ForEach(users, xId => choose[xId] * k[xId])));

            // Добавить ограничения-неравенства по каждому химическому элементу
            int count = 0;
            foreach (Elements itemElements in _elements)
            {
                var db_MinValElement = _database.Elements 
                    .Where(p => p.ID_Element == itemElements.ID_Element
                    && p.Owner.ID_User == _users.CurrentUser.ID_User
                    && p.IsSolve == true)
                    .FirstOrDefault();
                model.AddConstraint("Nerav_Max_" + (count + 1).ToString(), Model.Sum(Model.ForEach(users, xId => KoefNeravRows[count + 3][xId] * choose[xId])) <= db_MinValElement.MaxValElement);
                model.AddConstraint("Nerav_Min_" + (count + 1).ToString(), Model.Sum(Model.ForEach(users, xId => KoefNeravRows[count + 3][xId] * choose[xId])) >= db_MinValElement.MinValElement);
                count++;
            }           

            // Добавить прямые ограничения по каждой переменной
            model.AddConstraint("c_choose", Model.ForEach(users, xId => (min[xId] <= choose[xId] <= max[xId])));

            // Добавить ограничение по сумме всех переменных, сумма = 100 %
            model.AddConstraint("c_sum", Model.Sum(Model.ForEach(users, xId => choose[xId])) == 100);

            try
            {
                Solution solution = context.Solve();
                Report report = solution.GetReport();

                String reportStr = "";

                for (int i = 0; i < solverList.Count; i++)
                {
                    reportStr += "Значение X" + (i + 1).ToString() + ": " + choose.GetDouble(solverList[i].xId) + "\n";
                }
                reportStr += "\n" + report.ToString();

                //MessageBox.Show(reportStr);
            }
            catch (Exception ex)
            {
                //MessageBox.Show("При решении задачи оптимизации возникла исключительная ситуация");
            }

            #endregion --- Решение с использованием Solver Foundation

            #region --- Формирование массива с результатами расчета

            double[] out_values = new double[_maxComponents + 1]; // для каждого Xi + величина целевой функции 

            double SumCost = 0; // целевая функция (суммарная стоимость шихты)
            for (int i = 0; i < out_values.Length - 1; i++) 
            {                
                out_values[i] = Math.Round(choose.GetDouble(solverList[i].xId), 2);   
                SumCost = SumCost + Math.Round(solverList[i].Koef * choose.GetDouble(solverList[i].xId), 2);
            }
            out_values[_maxComponents] = Math.Round(SumCost, 2); // последний элемент массива - это целевая функция (суммарная стоимость шихты)

            #endregion --- Формирование массива с результатами расчета

            return out_values;
        }
    }
}