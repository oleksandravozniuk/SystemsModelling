using System;
using System.Collections.Generic;
using System.Text;

namespace BaseAlgo
{
    public class Element
    {
        public string Name { get; set; }
        public double TNext { get; set; }
        public double TCurr { get; set; }
        public double DelayMean { get; set; }
        public double DelayDev {get;set;}
        public string Distribution { get; set; }
        public int Quantity { get; set; }
        public int State { get; set; }
        public int Id { get; set; }

        private static int nextId = 0;


        public Element()
        {

            TNext = 0.0;
            DelayMean = 1.0;
            Distribution = "exp";
            TCurr = TNext;
            State = 0;
            Id = nextId;
            nextId++;
            Name = "element" + Id;
        }
        public Element(double delay)
        {
            Name = "anonymus";
            TNext = 0.0;
            DelayMean = delay;
            Distribution = "";
            TCurr = TNext;
            State = 0;
            Id = nextId;
            nextId++;
            Name = "element" + Id;
        }
        public Element(string nameOfElement, double delay)
        {
            Name = nameOfElement;
            TNext = 0.0;
            DelayMean = delay;
            Distribution = "exp";
            TCurr = TNext;
            State = 0;
            Id = nextId;
            nextId++;
            Name = "element" + Id;
        }

        public double GetDelay()
        {
            double delay = DelayMean;
            switch(Distribution)
            {
                case "exp": delay = FunRand.Exp(DelayMean);break;
                case "norm": delay = FunRand.Norm(DelayMean, DelayDev);break;
                case "unif": delay = FunRand.Unif(DelayMean, DelayDev);break;
                case "erl": delay = FunRand.Erlang(DelayMean, DelayDev); break;
                case  "": delay = DelayMean;break;
            }

            return delay;
        }

        virtual public void InAct()
        {

        }
        virtual public void OutAct()
        {
            Quantity++;
        }

        public void PrintResult()
        {
            Console.WriteLine(Name + "  quantity = " + Quantity);
        }

        virtual public void PrintInfo()
        {
            Console.WriteLine(Name + " state= " + State + " quantity = " + Quantity + " tnext= " + TNext);
        }

        virtual public void DoStatistics(double delta)
        {

        }
    }

}
