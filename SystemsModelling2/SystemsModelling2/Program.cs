using System;

namespace SystemsModelling2
{
    class Program
    {
        static void Main(string[] args)
        {
            Model model = new Model(2, 1, 5);
            model.Simulate(1000);

        }
    }
}
