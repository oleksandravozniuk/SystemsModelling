using System;
using System.Collections.Generic;
using System.Text;

namespace SystemsModelling2
{
    public class ExpDistribution
    {
        public double Exp(double parametr)
        {
            Random random = new Random();

            double a = random.NextDouble();

            double result = -parametr * Math.Log(a);

            return result;
        }
    }
}
