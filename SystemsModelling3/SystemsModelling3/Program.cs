using System;
using System.Collections.Generic;

namespace SystemsModelling3
{
    class Program
    {
        static void Main(string[] args)
        {
            Create c = new Create(2.0);

            Process p1 = new Process(1.0);
            Process p2 = new Process(1.0);
            Process p3 = new Process(1.0);
            Process p4 = new Process(1.0);

            Console.WriteLine("id0 = " + c.Id + "   id1 = " + p1.Id + "  id2 = " + p2.Id + "  id3 = " + p3.Id + "  id4 = " + p4.Id);
             
            c.NextElement = p1;
            p1.NextProcesses.Add(p2);
            p1.NextProcesses.Add(p3);
            p3.NextProcesses.Add(p4);

            p1.Maxqueue = 5;
            p2.Maxqueue = 5;
            p3.Maxqueue = 5;
            p4.Maxqueue = 5;

            p2.TNext = Double.MaxValue;
            p3.TNext = Double.MaxValue;
            p4.TNext = Double.MaxValue;

            c.Name = "CREATOR";
            p1.Name = "PROCESSOR 1";
            p2.Name = "PROCESSOR 2";
            p3.Name = "PROCESSOR 3";
            p4.Name = "PROCESSOR 4";

            c.Distribution = "exp";
            p1.Distribution = "exp";
            p2.Distribution = "exp";
            p3.Distribution = "exp";
            p4.Distribution = "exp";

            List<Element> list = new List<Element>{ c, p1, p2,p3, p4 };
            Model model = new Model(list);
            model.Simulate(1000.0);

        }
    }
}
