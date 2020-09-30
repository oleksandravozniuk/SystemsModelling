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
                    Console.WriteLine("Process name               Delay Mean         MaxQueue         Distribution        Mean lenght of queue        failure probability       max observable queue number           average loading  ");
                    Console.WriteLine(p.Name + "                       " + p.DelayMean +"           " + p.Maxqueue +"                    " +p.Distribution+"         "+ p.MeanQueue / tcurr + "                   " + p.Failure / (double)p.Quantity + "                     " + p.MaxObservableQueue + "                              " + (long)p.DeltaTR / p.TNext);
                    Console.WriteLine("max loading = " + p.MaxDeltaTR);
                    Console.WriteLine();
                }
            }
        }


    }
}