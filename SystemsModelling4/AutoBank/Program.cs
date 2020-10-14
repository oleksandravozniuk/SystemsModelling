using BaseAlgo;
using System;
using System.Collections.Generic;

namespace AutoBank
{
    class Program
    {
        static void Main(string[] args)
        {
            AutoBankCreate create = new AutoBankCreate(0.5)
            {
                Name = "Creator"            
            };

            AutoBankMassServiceSystem window1 = new AutoBankMassServiceSystem(0.3)
            {
                Name = "Window1",
                NextDespose = true,
                Maxqueue = 3,
                Distribution = "exp",
            };
            AutoBankMassServiceSystem window2 = new AutoBankMassServiceSystem(0.3)
            {
                Name = "Window2",
                NextDespose = true,
                Maxqueue = 3,
                Distribution = "exp",
            };

            window1.Channels = new List<Channel>
            {
                new Channel
                {
                    Name = "Cashier1"
                }
            };
            window2.Channels = new List<Channel>
            {
                 new Channel
                 {
                    Name = "Cashier2"
                 }
            };

            create.NextElements.Add(window1);
            create.NextElements.Add(window2);

            List<Element> list = new List<Element> { create, window1, window2 };
            AutoBankModel model = new AutoBankModel(list);
            model.Simulate(1000.0);
        }
    }
}
