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

            textBox2.Text = "Medium = " + Medium(xs).ToString();
            textBox3.Text = "Dispersion = " + Dispersion(xs).ToString();
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

            
            double sigma = rnd.Next(1,10);
            double a = rnd.Next(1, 20);

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

            textBox4.Text = "Medium = " + Medium(xs).ToString();
            textBox5.Text = "Dispersion = " + Dispersion(xs).ToString();
            textBox10.Text = "Hi = " + Hi1(xs, ns);
            textBox11.Text = "k = " + (ns.Count - 1).ToString();
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

            List<double> xs = new List<double>();

            for (int i = 1; i <= 10000; i++)
            {
                double z = a * zStart % c;
                double x = (double)z / c;
                xs.Add(x);
                zStart = z;

               // double fx = 1 - Math.Exp(-lambda * x);

                chart5.Series["Task3"].Points.AddXY(z, x);
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

            for (int i = 0; i < 10000; i++)
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

            textBox6.Text = "Medium = " + Medium(xs).ToString();
            textBox7.Text = "Dispersion = " + Dispersion(xs).ToString();
            textBox12.Text = "Hi = " + Hi1(xs, ns);
            textBox13.Text = "k = " + (ns.Count - 1).ToString();
        }

        public double Medium(List<double> randoms)
        {
            return randoms.Sum() / randoms.Count;
        }

        public double Dispersion(List<double> randoms)
        {
            double medium = Medium(randoms);
            double sum = 0;
            for (int i = 0; i < randoms.Count; i++)
            {
                double n = Math.Pow(randoms[i] - medium,2);
                sum += n;
            }
            return sum / randoms.Count;
        }

        public double Hi1(List<double> xs, List<double> ns)
        {
            xs = xs.OrderBy(x => x).ToList();
            double lambda = 1 / (xs.Sum() / 10000);

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
            double oldHi = 0;

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

    }
}
