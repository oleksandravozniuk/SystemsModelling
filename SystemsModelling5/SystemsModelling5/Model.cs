using System;
using System.Collections.Generic;
using System.Text;

namespace SystemsModelling5
{
    public class Model
    {
        private List<Element> list = new List<Element>();
        private double tnext, tcurr;
        private int _event;

        List<double> theoreticalAverageQueue = new List<double> { 1.786, 0.003, 0.004, 0.00001 };
        List<double> theoreticalLoading = new List<double> { 0.714, 0.054, 0.062, 0.036 };

        public Model(List<Element> elements)
        {
            list = elements;
            tnext = 0;
            _event = 0;
            tcurr = tnext;
        }

        public void Simulate(double time)
        {
            while (tcurr < time)
            {
                tnext = double.MaxValue;
                foreach (Element e in list)
                {
                    if (e.TNext < tnext)
                    {
                        tnext = e.TNext;
                        _event = e.Id;

                    }
                }
                

               // Console.WriteLine("\nIt's time for event in " + list[_event].Name + ", time =   " + tnext);
                foreach (Element e in list)
                {
                    e.DoStatistics(tnext - tcurr);
                }
                tcurr = tnext;
                foreach (Element e in list)
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
                //PrintInfo();
                //if (tcurr >= 1000)
                //    break;
            }

           // PrintResult();
        }

        public void PrintInfo()
        {
            foreach (Element e in list)
            {
                e.PrintInfo();
            }
        }

        public void PrintResult()
        {
            Console.WriteLine("\n-------------RESULTS-------------");

            foreach (Element e in list)
            {
                e.PrintResult();
                Console.WriteLine();
                if (e.GetType() == typeof(MassServiceSystem))
                {
                    MassServiceSystem p = (MassServiceSystem)e;
                    Console.WriteLine("Name        Theoretical average loading                Theoretical average queue");
                    Console.WriteLine(p.Name + "        " + theoreticalLoading[e.Id - 1] + "                                " + theoreticalAverageQueue[e.Id - 1]);
                    Console.WriteLine("Name        Real average loading                Real average queue");
                    Console.WriteLine(p.Name + "       " + ((long)p.DeltaTR / p.FinishTime) + "                   " + p.SumQueue / p.FinishTime);
                    Console.WriteLine("Name        Average loading fallibility               Average queue fallibility");
                    Console.WriteLine(p.Name + "       " + Math.Abs(((long)p.DeltaTR / p.FinishTime) - theoreticalLoading[e.Id-1]) + "                " + Math.Abs((p.SumQueue / p.FinishTime) - theoreticalAverageQueue[e.Id - 1]));
                    Console.WriteLine();
                }
            }
        }


    }
}
