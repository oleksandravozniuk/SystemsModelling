using System;
using System.Collections.Generic;
using System.Text;

namespace SystemsModelling3
{
    public class Process : Element
    {
        public int Queue { get; set; }
        public int Maxqueue { get; set; }
        public int Failure { get; set; }
        public double MeanQueue { get; set; }
        public int MaxObservableQueue { get; set; }
        public double DeltaTR { get; set; }
        public double MaxDeltaTR { get; set; }
        public List<Process> NextProcesses { get; set; } = new List<Process>();
        public bool NextDespose { get; set; }

        public Process(double delay):base(delay)
        {
            Queue = 0;
            Maxqueue = int.MaxValue;
            MeanQueue = 0.0;
            MaxObservableQueue = 0;
            DeltaTR = 0.0;
            MaxDeltaTR = 0.0;
            NextDespose = false;
        }

        override public void InAct()
        {
            if (base.State == 0)
            {
                base.State = 1;
                base.TNext = base.TCurr + base.GetDelay();
            }
            else
            {
                if (this.Queue < Maxqueue)
                {
                    Queue++;
                    
                }
                else
                {
                    Failure++;
                    Console.WriteLine("--------Failure----------");
                }
            }
        }

        override public void OutAct()
        {
            base.OutAct();
            base.TNext = double.MaxValue;
            base.State = 0;

            if (this.Queue > 0)
            {
                Queue--;
                base.State = 1;
                base.TNext = (base.TCurr + base.GetDelay());
            }

            if(NextProcesses.Count>0)
            {
                Random random = new Random();
                int index = 0;
                if (NextDespose)
                {
                    index = random.Next(0, NextProcesses.Count+1);
                }
                else
                {
                    index = random.Next(0, NextProcesses.Count);
                }

                if(index==NextProcesses.Count)
                {
                    Console.WriteLine("--------Dispose--------");
                }
                else
                {
                    Process nextProcess = NextProcesses[index];
                    nextProcess.InAct();
                }
                
            }
            else
            {
                Console.WriteLine("--------Dispose--------");
            }
        }

        override public void PrintInfo()
        {
            base.PrintInfo();
            Console.WriteLine("failure = " + this.Failure);
        }
        override public void DoStatistics(double delta)
        {
            MeanQueue += this.Queue * delta;
            if (Queue > MaxObservableQueue)
            {
                MaxObservableQueue = Queue;
            }
            DeltaTR += (delta * State);
            if(delta*State>MaxDeltaTR)
            {
                MaxDeltaTR = delta * (double)State;
            }
        }
    }
}
