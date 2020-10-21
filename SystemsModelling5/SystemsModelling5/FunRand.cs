using System;
using System.Collections.Generic;
using System.Text;

namespace SystemsModelling5
{
    public class FunRand
    {
        /**
 * Generates a random value according to an exponential distribution
 *
 * @param timeMean mean value
 * @return a random value according to an exponential distribution
 */
        public static double Exp(double timeMean)
        {
            Random random = new Random();
            double a = 0;
            while (a == 0)
            {
                a = random.NextDouble();
            }
            double res = -timeMean * Math.Log(a);

            return res;
        }

        /**
         * Generates a random value according to a uniform distribution
         *
         * @param timeMin
         * @param timeMax
         * @return a random value according to a uniform distribution
         */
        public static double Unif(double timeMin, double timeMax)
        {
            Random random = new Random();
            double a = 0;
            while (a == 0)
            {
                a = random.Next();
            }
            a = timeMin + a * (timeMax - timeMin);

            return a;
        }

        /**
         * Generates a random value according to a normal (Gauss) distribution
         *
         * @param timeMean
         * @param timeDeviation
         * @return a random value according to a normal (Gauss) distribution
         */
        public static double Norm(double timeMean, double timeDeviation)
        {
            Random r = new Random();
            double u1 = 1.0 - r.NextDouble(); //uniform(0,1] random doubles
            double u2 = 1.0 - r.NextDouble();
            double randStdNormal = Math.Sqrt(-2.0 * Math.Log(u1)) *
                         Math.Sin(2.0 * Math.PI * u2); //random normal(0,1)

            double a = timeMean + timeDeviation * randStdNormal;

            return a;
        }

        public static double Erlang(double timeMean, double timeDeviation)
        {
            double a = -1 / timeDeviation;
            double[] R = new double[] { 0.43, 0.80, 0.29, 0.67, 0.19, 0.96, 0.02, 0.73, 0.50, 0.33, 0.14, 0.71 };
            double r = 1;
            for (int i = 0; i < (int)timeMean; i++)
            {
                r *= R[i];
            }
            a *= Math.Log(r);
            return a;
        }

    }
}
