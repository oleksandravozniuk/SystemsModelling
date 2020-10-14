using System;
using System.Collections.Generic;

namespace BaseAlgo
{
    class Program
    {
        static void Main(string[] args)
        {
            Create c = new Create(2.0);
            MassServiceSystem mss = new MassServiceSystem(1.0);

            Channel channel1 = new Channel();
            channel1.Name = "Channel1";
            Channel channel2 = new Channel();
            channel2.Name = "Channel2";
            
            mss.Channels = new List<Channel>{ channel1, channel2 };

            //Console.WriteLine("id0 = " + c.Id + "   id1 = " + p1.Id + "  id2 = " + p2.Id + "  id3 = " + p3.Id + "  id4 = " + p4.Id);

            c.NextElements.Add(mss);
            mss.NextDespose = true;
            mss.Maxqueue = 5;


            c.Name = "CREATOR";
            mss.Name = "Smo 1";

            c.Distribution = "exp";
            mss.Distribution = "exp";

            List<Element> list = new List<Element> { c,mss  };
            Model model = new Model(list);
            model.Simulate(1000.0);
        }
    }
}
