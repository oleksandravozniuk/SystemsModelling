using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace SystemsModelling3
{
    public class Model
    {
        private List<Element> list = new List<Element>();
        private double tnext, tcurr;
        private int _event;

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
                foreach(Element e in list)
                {
                    if (e.TNext < tnext)
                    {
                        tnext = e.TNext;
                        _event = e.Id;

                    }
                }

                Console.WriteLine("\nIt's time for event in " +
                list[_event].Name +", time =   " + tnext);
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
            foreach(Element e in list)
            {
                e.PrintResult();
                if (e.GetType() == typeof(Process)) 
                {
                    Process p = (Process)e;
                    Console.WriteLine("Process name = " + p.Name);
                    Console.WriteLine("mean length of queue = " + p.MeanQueue / tcurr);
                    Console.WriteLine("failure probability  = " + p.Failure / (double)p.Quantity);
                    Console.WriteLine("max observable queue number = " + p.MaxObservableQueue);
                    Console.WriteLine("average loading = " + p.DeltaTR / e.TNext);
                    Console.WriteLine("max loading = " + p.MaxDeltaTR);
                    Console.WriteLine();
                }
            }
        }


    }
}