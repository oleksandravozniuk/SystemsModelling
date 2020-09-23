using System;

namespace SystemsModelling2
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("DelayCreate " + "DelayProcess " + "MaxQueue " + "   R average " + "                    T net "+ "                  FailureProbability");
            for(int i=0;i<10;i++)
            {
                Model model = new Model(2, 1, 5);
                model.Simulate(1000);
            }

            Console.WriteLine();
            Console.WriteLine("DelayCreate " + "DelayProcess " + "MaxQueue " + "   R average " + "                    T net " + "                  FailureProbability");
            for (int i = 0; i < 10; i++)
            {
                Model model = new Model(2, 1, i+1);
                model.Simulate(1000);
            }

            Console.WriteLine();
            Console.WriteLine("DelayCreate " + "DelayProcess " + "MaxQueue " + "      R average " + "                        T net " + "                  FailureProbability");
            for (int i = 0; i < 10; i++)
            {
                Model model = new Model(2, i+1, 5);
                model.Simulate(1000);
            }

            Console.WriteLine();
            Console.WriteLine("DelayCreate " + "DelayProcess " + "MaxQueue " + "      R average " + "                        T net " + "                  FailureProbability");
            for (int i = 0; i < 10; i++)
            {
                Model model = new Model(i+1, 1, 5);
                model.Simulate(1000);
            }
        }
    }
}
