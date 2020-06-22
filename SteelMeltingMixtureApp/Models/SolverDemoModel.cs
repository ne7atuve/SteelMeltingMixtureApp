using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
//using System.Web.Mvc;
using Microsoft.SolverFoundation.Services;

namespace SteelMeltingMixtureApp.Models
{  
    public class SolverDemoModel
    {
        InputDataModel _idm = new InputDataModel();
        public SolverDemoModel() {}

        public double[] SolverSteelMeltingMixture (InputDataModel idm)       
        {
            _idm = idm;  
                 
            #region --- Решение с использованием Solver Foundation

            List<SolverRowModel> solverList = new List<SolverRowModel>();

            //Добавить по X1
            solverList.Add(new SolverRowModel
            {
                xId = 1,
                Koef = 0.01 * _idm.CostComponent_1,
                Min = _idm.ComponentMin_1,
                Max = _idm.ComponentMax_1,
                KoefNerav1 = 0.01 *_idm.Si_InComponent_1,
                KoefNerav2 = 0.01 *_idm.Mn_InComponent_1,
                KoefNerav3 = 0.01 *_idm.Cr_InComponent_1,
                KoefNerav4 = 0.01 *_idm.Ni_InComponent_1
            });

            // Добавить по X2
            solverList.Add(new SolverRowModel
            {
                xId = 2,
                Koef = 0.01 * _idm.CostComponent_2,
                Min = _idm.ComponentMin_2,
                Max = _idm.ComponentMax_2,
                KoefNerav1 = 0.01 * _idm.Si_InComponent_2,
                KoefNerav2 = 0.01 * _idm.Mn_InComponent_2,
                KoefNerav3 = 0.01 * _idm.Cr_InComponent_2,
                KoefNerav4 = 0.01 * _idm.Ni_InComponent_2
            });

            // Добавить по X3
            solverList.Add(new SolverRowModel
            {
                xId = 3,
                Koef = 0.01 * _idm.CostComponent_3,
                Min = _idm.ComponentMin_3,
                Max = _idm.ComponentMax_3,
                KoefNerav1 = 0.01 * _idm.Si_InComponent_3,
                KoefNerav2 = 0.01 * _idm.Mn_InComponent_3,
                KoefNerav3 = 0.01 * _idm.Cr_InComponent_3,
                KoefNerav4 = 0.01 * _idm.Ni_InComponent_3
            });

            // Добавить по X4
            solverList.Add(new SolverRowModel
            {
                xId = 4,
                Koef = 0.01 * _idm.CostComponent_4,
                Min = _idm.ComponentMin_4,
                Max = _idm.ComponentMax_4,
                KoefNerav1 = 0.01 * _idm.Si_InComponent_4,
                KoefNerav2 = 0.01 * _idm.Mn_InComponent_4,
                KoefNerav3 = 0.01 * _idm.Cr_InComponent_4,
                KoefNerav4 = 0.01 * _idm.Ni_InComponent_4
            });

            // Добавить по X5
            solverList.Add(new SolverRowModel
            {
                xId = 5,
                Koef = 0.01 * _idm.CostComponent_5,
                Min = _idm.ComponentMin_5,
                Max = _idm.ComponentMax_5,
                KoefNerav1 = 0.01 * _idm.Si_InComponent_5,
                KoefNerav2 = 0.01 * _idm.Mn_InComponent_5,
                KoefNerav3 = 0.01 * _idm.Cr_InComponent_5,
                KoefNerav4 = 0.01 * _idm.Ni_InComponent_5
            });

            // Добавить по X6
            solverList.Add(new SolverRowModel
            {
                xId = 6,
                Koef = 0.01 * _idm.CostComponent_6,
                Min = _idm.ComponentMin_6,
                Max = _idm.ComponentMax_6,
                KoefNerav1 = 0.01 * _idm.Si_InComponent_6,
                KoefNerav2 = 0.01 * _idm.Mn_InComponent_6,
                KoefNerav3 = 0.01 * _idm.Cr_InComponent_6,
                KoefNerav4 = 0.01 * _idm.Ni_InComponent_6
            });

            // Добавить по X7
            solverList.Add(new SolverRowModel
            {
                xId = 7,
                Koef = 0.01 * _idm.CostComponent_7,
                Min = _idm.ComponentMin_7,
                Max = _idm.ComponentMax_7,
                KoefNerav1 = 0.01 * _idm.Si_InComponent_7,
                KoefNerav2 = 0.01 * _idm.Mn_InComponent_7,
                KoefNerav3 = 0.01 * _idm.Cr_InComponent_7,
                KoefNerav4 = 0.01 * _idm.Ni_InComponent_7
            });

            SolverContext context = SolverContext.GetContext();
            Model model = context.CreateModel();

            Set users = new Set(Domain.Any, "users");

            Parameter k = new Parameter(Domain.Real, "Koef", users);
            k.SetBinding(solverList, "Koef", "xId");

            Parameter min = new Parameter(Domain.Real, "Min", users);
            min.SetBinding(solverList, "Min", "xId");

            Parameter max = new Parameter(Domain.Real, "Max", users);
            max.SetBinding(solverList, "Max", "xId");

            Parameter KoefNerav1 = new Parameter(Domain.Real, "KoefNerav1", users);
            KoefNerav1.SetBinding(solverList, "KoefNerav1", "xId");

            Parameter KoefNerav2 = new Parameter(Domain.Real, "KoefNerav2", users);
            KoefNerav2.SetBinding(solverList, "KoefNerav2", "xId");

            Parameter KoefNerav3 = new Parameter(Domain.Real, "KoefNerav3", users);
            KoefNerav3.SetBinding(solverList, "KoefNerav3", "xId");

            Parameter KoefNerav4 = new Parameter(Domain.Real, "KoefNerav4", users);
            KoefNerav4.SetBinding(solverList, "KoefNerav4", "xId");

            model.AddParameters(k, min, max, KoefNerav1, KoefNerav2, KoefNerav3, KoefNerav4);

            Decision choose = new Decision(Domain.RealNonnegative, "choose", users);
            model.AddDecision(choose);

            model.AddGoal("goal", GoalKind.Minimize, Model.Sum(Model.ForEach(users, xId => choose[xId] * k[xId])));

            // Добавить ограничения-неравенства по [Si]
            model.AddConstraint("Nerav_Si_Max", Model.Sum(Model.ForEach(users, xId => KoefNerav1[xId] * choose[xId])) <= _idm.Si_InShihtaNeed_Max);
            model.AddConstraint("Nerav_Si_Min", Model.Sum(Model.ForEach(users, xId => KoefNerav1[xId] * choose[xId])) >= _idm.Si_InShihtaNeed_Min);

            // Добавить ограничения-неравенства по [Mn]
            model.AddConstraint("Nerav_Mn_Max", Model.Sum(Model.ForEach(users, xId => KoefNerav2[xId] * choose[xId])) <= _idm.Mn_InShihtaNeed_Max);
            model.AddConstraint("Nerav_Mn_Min", Model.Sum(Model.ForEach(users, xId => KoefNerav2[xId] * choose[xId])) >= _idm.Mn_InShihtaNeed_Min);

            // Добавить ограничения-неравенства по [Cr]
            model.AddConstraint("Nerav_Cr_Max", Model.Sum(Model.ForEach(users, xId => KoefNerav3[xId] * choose[xId])) <= _idm.Cr_InShihtaNeed_Max);
            model.AddConstraint("Nerav_Cr_Min", Model.Sum(Model.ForEach(users, xId => KoefNerav3[xId] * choose[xId])) >= _idm.Cr_InShihtaNeed_Min);

            // Добавить ограничения-неравенства по [Ni]
            model.AddConstraint("Nerav_Ni_Max", Model.Sum(Model.ForEach(users, xId => KoefNerav4[xId] * choose[xId])) <= _idm.Ni_InShihtaNeed_Max);
            model.AddConstraint("Nerav_Ni_Min", Model.Sum(Model.ForEach(users, xId => KoefNerav4[xId] * choose[xId])) >= _idm.Ni_InShihtaNeed_Min);

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

            double[] out_values = new double[10];

            out_values[0] = Math.Round(choose.GetDouble(solverList[0].xId), 2);
            out_values[1] = Math.Round(choose.GetDouble(solverList[1].xId), 2);
            out_values[2] = Math.Round(choose.GetDouble(solverList[2].xId), 2);
            out_values[3] = Math.Round(choose.GetDouble(solverList[3].xId), 2);
            out_values[4] = Math.Round(choose.GetDouble(solverList[4].xId), 2);
            out_values[5] = Math.Round(choose.GetDouble(solverList[5].xId), 2);
            out_values[6] = Math.Round(choose.GetDouble(solverList[6].xId), 2);

            return out_values;
        }
    }
}