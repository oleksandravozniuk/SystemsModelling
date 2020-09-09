using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SystemsModelling1
{
    public partial class Form1 : Form
    {
        const double N = 10000;

        
        public Form1()
        {
            InitializeComponent();
        }

        public List<double> GenerateRandomValues()
        {
            List<double> valList = new List<double>();
            Random rndDouble = new Random();

            for (int i = 0; i < N; i++)
            {
                valList.Add(rndDouble.NextDouble());
            }

            valList = valList.OrderBy(x => x).ToList();
            return valList;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            chart1.Series["Task1"].Points.Clear();
            chart3.Series["Task1.1"].Points.Clear();

            fillChart(GenerateRandomValues());
        }


        private void fillChart(List<double> valList)
        {
            Random random = new Random();
            
            double lambda = random.Next(1, 100);

            List<double> xs = new List<double>();

            textBox1.Text = "lambda = " + lambda;

            for (int i = 1; i <= 10000; i++)
            {
                double x = (-1 / lambda) * Math.Log(valList[i - 1]);
                xs.Add(x);

                double Fx =  1 - Math.Exp(-lambda * x);

                chart1.Series["Task1"].Points.AddXY(x, Fx);
            }

            xs = xs.OrderBy(x => x).ToList();

            double min = xs.First();
            double max = xs.Last();

            double hReal = (max - min) / 20.0;

            double h = hReal;

            double counter = 0;
            List<double> ns = new List<double>();

            for (int i = 0; i < 10000; i++)
            {
                if(xs[i]<=h)
                {
                    counter++;
                }
                else
                {
                    ns.Add(counter);
                    counter = 0;
                    h = h + hReal;
                }
            }

            int indexN = 0;
            double h2 = hReal;
            double maxValue = ns[0];

            for(int i = 0;i<10000;i++)
            {
                if(i>maxValue)
                {
                    maxValue = maxValue + ns[indexN++];
                }
                chart3.Series["Task1.1"].Points.AddXY(xs[i], ns[indexN]);
            }

            textBox2.Text = "M = " + M(xs).ToString();
            textBox3.Text = "D = " + D(xs).ToString();
            textBox8.Text = "Hi = " + Hi1(xs, ns);
            textBox9.Text = "k = " + (ns.Count-1).ToString();
        }


        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            chart2.Series["Task2"].Points.Clear();
            chart4.Series["Task2.1"].Points.Clear();
            fillChart2();
        }

        private void fillChart2()
        {
            Random rnd = new Random();


            double sigma = rnd.NextDouble();
            double a = rnd.NextDouble();

            List<double> xs = new List<double>();

            for (int i = 1; i <= 100000; i++)
            {

                double x = sigma * nu() + a;
                xs.Add(x);

                double Fx = 1.0 / (sigma * Math.Sqrt(2 * Math.PI)) * (Math.Exp((-Math.Pow(x - a, 2)) / (2 * Math.Pow(sigma, 2))));

                chart2.Series["Task2"].Points.AddXY(x, Fx);
            }

            xs = xs.OrderBy(x => x).ToList();

            double min = xs.First();
            double max = xs.Last();

            double hReal = (max - min) / 20.0;

            double h = hReal;

            double counter = 0;
            List<double> ns = new List<double>();

            for (int i = 0; i < 100000; i++)
            {

                if (xs[i] < h)
                {
                    counter++;
                }
                else
                {
                    ns.Add(counter);
                    counter = 0;
                    h = h + hReal;
                }
            }

            int indexN = 0;
            double h2 = hReal;
            double maxValue = ns[0];

            for (int i = 0; i < 100000; i++)
            {
                if (i > maxValue)
                {
                    maxValue = maxValue + ns[indexN];
                    if(indexN<ns.Count-1)
                    {
                        indexN++;
                    }
                }
                chart4.Series["Task2.1"].Points.AddXY(xs[i], ns[indexN]);
            }

            textBox4.Text = "M = " + M(xs).ToString();
            textBox5.Text = "D = " + D(xs).ToString();
            textBox10.Text = "Hi = " + Hi2(xs, ns,sigma,a);
            textBox11.Text = "k = " + (ns.Count - 1).ToString();
            textBox14.Text = "sigma=" + sigma;
            textBox15.Text = "a=" + a;
        }

        private double nu()
        {
            Random rndDouble = new Random();
            double nu = 0;
            
            for (int i=0;i<12;i++)
            {
                nu = nu + rndDouble.NextDouble();
            }

            nu = nu - 6;
            return nu;
        }

        private void button3_Click(object sender, EventArgs e)
        {


            chart5.Series["Task3"].Points.Clear();                                                                                                                                                      
            chart6.Series["Task3.1"].Points.Clear();

            fillChart3();
        }

        private void fillChart3()
        {
            Random rnd = new Random();

            double a = Math.Pow(5, 13);
            double c = Math.Pow(2, 31);
            double zStart = rnd.Next(1, 100);
            double z0 = zStart;

            List<double> xs = new List<double>();

            for (int i = 1; i <= N; i++)
            {
                double z = a * zStart % c;
                double x = (double)z / c;
                xs.Add(x);
                zStart = z;

                chart5.Series["Task3"].Points.AddXY(x, z);
            }

            xs = xs.OrderBy(x => x).ToList();

            double min = xs.First();
            double max = xs.Last();

            double hReal = (max - min) / 20.0;

            double h = hReal;

            double counter = 0;
            List<double> ns = new List<double>();

            for (int i = 0; i < N; i++)
            {
                if (xs[i] <= h)
                {
                    counter++;
                }
                else
                {
                    ns.Add(counter);
                    counter = 0;
                    h = h + hReal;
                }
            }

            int indexN = 0;
            double h2 = hReal;
            double maxValue = ns[0];

            for (int i = 0; i < N; i++)
            {
                if (i > maxValue)
                {
                    if(indexN<ns.Count-1)
                    {
                        indexN++;
                    }
                    maxValue = maxValue + ns[indexN];
                }
                chart6.Series["Task3.1"].Points.AddXY(xs[i], ns[indexN]);
            }

            textBox6.Text = "M = " + M(xs).ToString();
            textBox7.Text = "D = " + D(xs).ToString();
            textBox12.Text = "Hi = " + Hi3(xs, ns);
            textBox13.Text = "k = " + (ns.Count - 1).ToString();
            textBox16.Text = "z0 = " + z0;
        }

        public double M(List<double> xs)
        {
            return xs.Sum() / xs.Count;
        }

        public double D(List<double> xs)
        {
            double medium = M(xs);
            double sum = 0;
            for (int i = 0; i < xs.Count; i++)
            {
                double n = Math.Pow(xs[i] - medium,2);
                sum += n;
            }
            return sum / (xs.Count-1);
        }

        public double Hi1(List<double> xs, List<double> ns)
        {
            xs = xs.OrderBy(x => x).ToList();
            double lambda = 1 / (xs.Sum() / N);

            List<double> pits = new List<double>();

            double startValueInterval = 0;

            double currentN = 0;

            for(int i=0;i<ns.Count;i++)
            {
                currentN = currentN + ns[i];
                double endValueInterval = FindEndValueInterval((int)currentN, xs);
                pits.Add(Math.Exp(-lambda * startValueInterval) - Math.Exp(-lambda * endValueInterval));
                startValueInterval = endValueInterval;
            }

            double hi = 0;
            

            for(int i=0;i<ns.Count;i++)
            {
                if (pits[i] != 0)
                {
                    hi = hi + Math.Pow(ns[i] - N * pits[i], 2) / (N * pits[i]);
                }
                
            }

            return hi;
        }

        public double FindEndValueInterval(int n, List<double> xs)
        {
            return xs.ElementAt(n);
        }

        public double Hi3(List<double> xs, List<double> ns)
        {
            xs = xs.OrderBy(x => x).ToList();
            double lambda = 1 / (xs.Sum() / 10000);

            List<double> pits = new List<double>();

            double startValueInterval = 0;

            double currentN = 0;

            for (int i = 0; i < ns.Count; i++)
            {
                currentN = currentN + ns[i];
                double endValueInterval = FindEndValueInterval((int)currentN, xs);
                pits.Add((endValueInterval-startValueInterval)/(xs.Last()-xs.First()));
                startValueInterval = endValueInterval;
            }

            double hi = 0;
           

            for (int i = 0; i < ns.Count; i++)
            {
                if (pits[i] != 0)
                {
                    hi = hi + Math.Pow(ns[i] - N * pits[i], 2) / (N * pits[i]);
                }

            }

            return hi;
        }

        public double Hi2(List<double> xs, List<double> ns, double sigma, double a)
        {
            xs = xs.OrderBy(x => x).ToList();

            List<double> pits = new List<double>();

            double startValueInterval = 0;

            double currentN = 0;

            for (int i = 0; i < ns.Count; i++)
            {
                currentN = currentN + ns[i];
                double endValueInterval = FindEndValueInterval((int)currentN, xs);
                pits.Add(GetFValue((endValueInterval))-GetFValue((startValueInterval)));
                startValueInterval = endValueInterval;
            }

            double hi = 0;
           

            for (int i = 0; i < ns.Count; i++)
            {
                if (pits[i] != 0 || ns[i]!=0)
                {
                    hi = hi + Math.Pow(ns[i] -  100000*pits[i], 2) / ( 100000*pits[i]);
                }

            }

            return hi;
        }

        public List<List<double>> InitializeFValues()
        {
            List<double> fLine1 = new List<double>() { 0.5000, 0.5040, 0.5080, 0.5120, 0.5160, 0.5199, 0.5239, 0.5279, 0.5319, 0.5359 };
            List<double> fLine2 = new List<double>() { 0.5398, 0.5438, 0.5478, 0.5517, 0.5557, 0.5596, 0.5636, 0.5675, 0.5714, 0.5753 };
            List<double> fLine3 = new List<double>() { 0.5793, 0.5832, 0.5871, 0.5910, 0.5948, 0.5987, 0.6026, 0.6064, 0.6103, 0.6141 };
            List<double> fLine4 = new List<double>() { 0.6179, 0.6217, 0.6255, 0.6293, 0.6331, 0.6368, 0.6406, 0.6443, 0.6480, 0.6517 };
            List<double> fLine5 = new List<double>() { 0.6554, 0.6591, 0.6628, 0.6664, 0.6700, 0.6736, 0.6772, 0.6808, 0.6844, 0.6879 };
            List<double> fLine6 = new List<double>() { 0.6915, 0.6950, 0.6985, 0.7019, 0.7054, 0.7088, 0.7123, 0.7157, 0.7190, 0.7224 };
            List<double> fLine7 = new List<double>() { 0.7257, 0.7291, 0.7324, 0.7357, 0.7389, 0.7422, 0.7454, 0.7486, 0.7517, 0.7549 };
            List<double> fLine8 = new List<double>() { 0.7580, 0.7611, 0.7642, 0.7673, 0.7704, 0.7734, 0.7764, 0.7794, 0.7823, 0.7852 };
            List<double> fLine9 = new List<double>() { 0.7881, 0.7910, 0.7939, 0.7967, 0.7995, 0.8023, 0.8051, 0.8078, 0.8106, 0.8133 };
            List<double> fLine10 = new List<double>() { 0.8159, 0.8186, 0.8212, 0.8238, 0.8264, 0.8289, 0.8315, 0.8340, 0.8365, 0.8389 };
            List<double> fLine11 = new List<double>() { 0.8413, 0.8438, 0.8461, 0.8485, 0.8508, 0.8531, 0.8554, 0.8577, 0.8599, 0.8621 };
            List<double> fLine12 = new List<double>() { 0.8643, 0.8665, 0.8686, 0.8708, 0.8729, 0.8749, 0.8770, 0.8790, 0.8810, 0.8830 };
            List<double> fLine13 = new List<double>() { 0.8849, 0.8869, 0.8888, 0.8907, 0.8925, 0.8944, 0.8962, 0.8980, 0.8997, 0.9015 };
            List<double> fLine14 = new List<double>() { 0.9032, 0.9049, 0.9066, 0.9082, 0.9099, 0.9115, 0.9131, 0.9147, 0.9162, 0.9177 };
            List<double> fLine15 = new List<double>() { 0.9192, 0.9207, 0.9222, 0.9236, 0.9251, 0.9265, 0.9279, 0.9292, 0.9306, 0.9319 };
            List<double> fLine16 = new List<double>() { 0.9332, 0.9345, 0.9357, 0.9370, 0.9382, 0.9394, 0.9406, 0.9418, 0.9429, 0.9441 };
            List<double> fLine17 = new List<double>() { 0.9452, 0.9463, 0.9474, 0.9484, 0.9495, 0.9505, 0.9515, 0.9525, 0.9535, 0.9545 };
            List<double> fLine18 = new List<double>() { 0.9554, 0.9564, 0.9573, 0.9582, 0.9591, 0.9599, 0.9608, 0.9616, 0.9625, 0.9633 };
            List<double> fLine19 = new List<double>() { 0.9641, 0.9649, 0.9656, 0.9664, 0.9671, 0.9678, 0.9686, 0.9693, 0.9699, 0.9706 };
            List<double> fLine20 = new List<double>() { 0.9713, 0.9719, 0.9726, 0.9732, 0.9738, 0.9744, 0.9750, 0.9756, 0.9761, 0.9767 };
            List<double> fLine21 = new List<double>() { 0.9772, 0.9778, 0.9783, 0.9788, 0.9793, 0.9798, 0.9803, 0.9808, 0.9812, 0.9817 };
            List<double> fLine22 = new List<double>() { 0.9821, 0.9826, 0.9830, 0.9834, 0.9838, 0.9842, 0.9846, 0.9850, 0.9854, 0.9857 };
            List<double> fLine23 = new List<double>() { 0.9861, 0.9864, 0.9868, 0.9871, 0.9875, 0.9878, 0.9881, 0.9884, 0.9887, 0.9890 };
            List<double> fLine24 = new List<double>() { 0.9893, 0.9896, 0.9898, 0.9901, 0.9904, 0.9906, 0.9909, 0.9911, 0.9913, 0.9916 };
            List<double> fLine25 = new List<double>() { 0.9918, 0.9920, 0.9922, 0.9925, 0.9927, 0.9929, 0.9931, 0.9932, 0.9934, 0.9936 };
            List<double> fLine26 = new List<double>() { 0.9938, 0.9940, 0.9941, 0.9943, 0.9945, 0.9946, 0.9948, 0.9949, 0.9951, 0.9952 };
            List<double> fLine27 = new List<double>() { 0.9953, 0.9955, 0.9956, 0.9957, 0.9959, 0.9960, 0.9961, 0.9962, 0.9963, 0.9964 };
            List<double> fLine28 = new List<double>() { 0.9965, 0.9966, 0.9967, 0.9968, 0.9969, 0.9970, 0.9971, 0.9972, 0.9973, 0.9974 };
            List<double> fLine29 = new List<double>() { 0.9974, 0.9975, 0.9976, 0.9977, 0.9977, 0.9978, 0.9979, 0.9979, 0.9980, 0.9981 };
            List<double> fLine30 = new List<double>() { 0.9981, 0.9982, 0.9982, 0.9983, 0.9984, 0.9984, 0.9985, 0.9985, 0.9986, 0.9986 };
            List<double> fLine31 = new List<double>() { 0.9987, 0.9987, 0.9987, 0.9988, 0.9988, 0.9989, 0.9989, 0.9989, 0.9990, 0.9990 };

            List<List<double>> Fx = new List<List<double>>() 
            { 
                fLine1, fLine2, fLine3, fLine4, fLine5, fLine6, fLine7, fLine8, fLine9, fLine10, fLine11, fLine12, fLine13, fLine14,
                fLine15, fLine16, fLine17, fLine18, fLine19, fLine20, fLine21, fLine22, fLine23, fLine24, fLine25, fLine26, fLine27, fLine28,
                fLine29, fLine30, fLine31
            };

            return Fx;
        }

        public double GetFValue(double x)
        {
            bool negative = false;

            if(x<0)
            {
                negative = true;
                x = x * (-1);
            }
            double roundedX = Math.Round(x, 2);

            List <List<double>> fs = InitializeFValues();

            for(int i=0;i<fs.Count;i++)
            {
                if((int)(roundedX*10) == i)
                {
                    for (int j = 0; j < 10; j++)
                    {
                        if (j.ToString() == roundedX.ToString().Last().ToString())
                        {
                            double result = 0;

                            if (negative == true)
                                result = 1 - fs[i][j];
                            else
                                result = fs[i][j];

                            return result;
                        }
                            
                    }
                }
                
            }

            return 0;
        }

    }
}
