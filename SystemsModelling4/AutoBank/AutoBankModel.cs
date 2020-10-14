using BaseAlgo;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace AutoBank
{
    public class AutoBankModel
    {
        private List<Element> list = new List<Element>();
        private double tnext, tcurr;
        private int _event;

        private double systemChangeCount;
        private double changeLineCount;

        public AutoBankModel(List<Element> elements)
        {
            list = elements;
            tnext = 0;
            _event = 0;
            tcurr = tnext;

            systemChangeCount = 0.0;
            changeLineCount = 0.0;
        }

        public void Simulate(double time)
        {
            while (tcurr < time)
            {
                tnext = double.MaxValue;
                foreach(Element e in list)
                {
                    if (e.TNext < tnext)
                    {
                        tnext = e.TNext;
                        _event = e.Id;

                    }
                }

                Console.WriteLine("\nIt's time for event in " + list[_event].Name +", time =   " + tnext);
                foreach(Element e in list)
                {
                    e.DoStatistics(tnext - tcurr);
                }
                tcurr = tnext;
                foreach(Element e in list)
                {
                    e.TCurr = tcurr;
                }
                list[_event].OutAct();
                foreach (Element e in list)
                {
                    if (e.TNext == tcurr)
                    {
                        e.OutAct();

                    }
                }

                ChangeWindow(list);

                systemChangeCount++;

                PrintInfo();
            }

            PrintResult();
        }

        public void PrintInfo()
        {
            foreach(Element e in list)
            {
                e.PrintInfo();
            }
        }

        public void PrintResult()
        {
            Console.WriteLine("\n-------------RESULTS-------------");
            double sumFailure = 0.0;
            double creatorQuantity = 0.0;

            foreach(Element e in list)
            {
                e.PrintResult();
                Console.WriteLine();
                if (e.GetType() == typeof(AutoBankMassServiceSystem)) 
                {
                    AutoBankMassServiceSystem p = (AutoBankMassServiceSystem)e;
                    Console.WriteLine("Name        Average loading         Dispose interval average        Average queue");
                    Console.WriteLine(p.Name + "    " + (long)p.DeltaTR / p.FinishTime + "       " + p.DisposeIntervalSum/p.Quantity + "         " + p.SumQueue/systemChangeCount);
                    Console.WriteLine();
                    sumFailure = sumFailure + p.Failure;
                }
                else
                {
                    creatorQuantity = e.Quantity;
                }
            }
            Console.WriteLine("Average bank clients count: " + GetAverageClientsCount(list) / systemChangeCount);
            Console.WriteLine("Percent of failure: " + (sumFailure * 100) / creatorQuantity);
            Console.WriteLine("Change line count: " + changeLineCount);
        }

        public void ChangeWindow(List<Element> elements)
        {
            List<AutoBankMassServiceSystem> massServiceSystems = new List<AutoBankMassServiceSystem>();
            foreach(var element in elements)
                if(element.GetType() == typeof(AutoBankMassServiceSystem))
                {
                    massServiceSystems.Add((AutoBankMassServiceSystem)element);
                }

            if((massServiceSystems[0].Queue == 2 && massServiceSystems[1].Queue == 0) 
                ||(massServiceSystems[0].Queue == 3 && massServiceSystems[1].Queue == 0)
                ||(massServiceSystems[0].Queue == 3 && massServiceSystems[1].Queue == 1))
            {
                massServiceSystems[0].Queue--;
                massServiceSystems[1].Queue++;
                changeLineCount++;
            }
            else
            {
                if((massServiceSystems[0].Queue == 0 && massServiceSystems[1].Queue == 2)
                    ||(massServiceSystems[0].Queue == 0 && massServiceSystems[1].Queue == 3)
                    ||(massServiceSystems[0].Queue == 1 && massServiceSystems[1].Queue == 3))
                {
                    massServiceSystems[0].Queue++;
                    massServiceSystems[1].Queue--;
                    changeLineCount++;
                }
            }
        }

        public double GetAverageClientsCount(List<Element> elements)
        {
            List<AutoBankMassServiceSystem> massServiceSystems = new List<AutoBankMassServiceSystem>();
            foreach (var element in elements)
                if (element.GetType() == typeof(AutoBankMassServiceSystem))
                {
                    massServiceSystems.Add((AutoBankMassServiceSystem)element);
                }

            return massServiceSystems[0].BankClientsCount + massServiceSystems[1].BankClientsCount;
        }


    }
}