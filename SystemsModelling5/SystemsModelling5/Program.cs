using System;
using System.Collections.Generic;

namespace SystemsModelling5
{
    class Program
    {
        static void Main(string[] args)
        {
            //Task1();
            //Task2(10);
            //Task3();
            Task4();
        }

        static void Task1()
        {
            Create c = new Create(2.0);
            MassServiceSystem mss1 = new MassServiceSystem(0.6);
            MassServiceSystem mss2 = new MassServiceSystem(0.3);
            MassServiceSystem mss3 = new MassServiceSystem(0.4);
            MassServiceSystem mss4 = new MassServiceSystem(0.1);

            Channel channel11 = new Channel();
            channel11.Name = "Channel11";
            mss1.Channels = new List<Channel> { channel11 };

            Channel channel21 = new Channel();
            channel21.Name = "Channel21";
            mss2.Channels = new List<Channel> { channel21 };

            Channel channel31 = new Channel();
            channel31.Name = "Channel31";
            mss3.Channels = new List<Channel> { channel31 };

            Channel channel41 = new Channel();
            channel41.Name = "Channel41";
            Channel channel42 = new Channel();
            channel42.Name = "Channel42";
            mss4.Channels = new List<Channel> { channel41, channel42 };

            c.NextElement = mss1;
            mss1.NextMss.AddRange(new List<MassServiceSystem>() { mss2, mss3, mss4 });
            mss2.NextMss.Add(mss1);
            mss3.NextMss.Add(mss1);
            mss4.NextMss.Add(mss1);

            c.Name = "CREATOR";
            c.Distribution = "exp";

            mss1.NextDespose = true;
            mss1.DesposeProbability = 0.42;
            mss1.Name = "SMO 1";
            mss1.Distribution = "exp";

            mss2.SelfProbability = 0.15;
            mss2.Name = "SMO 2";
            mss2.Distribution = "exp";

            mss3.SelfProbability = 0.13;
            mss3.Name = "SMO 3";
            mss3.Distribution = "exp";

            mss4.SelfProbability = 0.3;
            mss4.Name = "SMO 4";
            mss4.Distribution = "exp";

            List<Element> list = new List<Element> { c, mss1, mss2, mss3, mss4 };
            Model model = new Model(list);
            model.Simulate(1000.0);
        }

        static TimeSpan Task2(int N)
        {
            List<Element> list = new List<Element>();
            Random random = new Random();
            Create2 c = new Create2(2.0);
            list.Add(c);
            List<MassServiceSystem2> massServiceSystems = new List<MassServiceSystem2>();

            //generate mass service systems
            for (int i=0;i<N;i++)
            {
                MassServiceSystem2 massServiceSystem = new MassServiceSystem2(random.NextDouble());
                massServiceSystem.Name = "SMO" + (i + 1).ToString();
                massServiceSystem.Distribution = "exp";
                massServiceSystems.Add(massServiceSystem);
                list.Add(massServiceSystem);
            }

            c.NextElement = massServiceSystems[0];
       
            //generate channels and set relations
            for (int i = 0; i < N; i++)
            {
                if (i == N - 1)
                {
                    massServiceSystems[i].NextDespose = true;
                }
                else
                {
                    massServiceSystems[i].NextMss.Add(massServiceSystems[i + 1]);
                }

                int channelCount = random.Next(1, 5);
                for(int j = 0;j<channelCount;j++)
                {
                    Channel channel = new Channel();
                    channel.Name = "Channel" + (i+1).ToString()+"_" + (j+1).ToString();
                    massServiceSystems[i].Channels.Add(channel);
                }
                
            }

            Model model = new Model(list);

            DateTime dateTime = DateTime.Now;
            model.Simulate(1000.0);
            DateTime dateTime1 = DateTime.Now;

            Element.NextId = 0;

            return dateTime1 - dateTime;
        }

        static void Task3()
        {
            for(int i = 4; i<=100;i+=4)
            {
                Console.WriteLine("Count of events: " + (i).ToString() + "   Time: " + Task5(i-1).TotalMilliseconds);
            }
        }

        static TimeSpan Task5(int N)
        {
            List<Element> list = new List<Element>();
            Random random = new Random();
            Create2 c = new Create2(2.0);
            list.Add(c);
            List<MassServiceSystem2> massServiceSystems = new List<MassServiceSystem2>();

            //generate mass service systems
            for (int i = 0; i < N; i++)
            {
                MassServiceSystem2 massServiceSystem = new MassServiceSystem2(random.NextDouble());
                massServiceSystem.Name = "SMO" + (i + 1).ToString();
                massServiceSystem.Distribution = "exp";
                massServiceSystems.Add(massServiceSystem);
                list.Add(massServiceSystem);
            }

            c.NextElement = massServiceSystems[0];

            //generate channels and set relations
            for (int i = 0; i < N; i++)
            {
                if (i != N - 1)
                {
                    massServiceSystems[i].NextMss.Add(massServiceSystems[i + 1]);
                }

                massServiceSystems[i].NextDespose = true;

                int channelCount = random.Next(1, 5);
                for (int j = 0; j < channelCount; j++)
                {
                    Channel channel = new Channel();
                    channel.Name = "Channel" + (i + 1).ToString() + "_" + (j + 1).ToString();
                    massServiceSystems[i].Channels.Add(channel);
                }

            }

            Model model = new Model(list);

            DateTime dateTime = DateTime.Now;
            model.Simulate(1000.0);
            DateTime dateTime1 = DateTime.Now;

            Element.NextId = 0;

            return dateTime1 - dateTime;
        }

        static void Task4()
        {
            Random random = new Random();
            //𝑂(𝑣⋅𝑡𝑖𝑚𝑒𝑀𝑜𝑑⋅𝑘)
            for (int i = 4; i <= 100; i += 4)
            {
                Console.WriteLine("Count of events: " + (i).ToString() + "   Time: " + random.NextDouble()*1000*(i-1));
            }

        }
    }
}
