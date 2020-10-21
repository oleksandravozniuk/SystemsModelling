using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SystemsModelling5
{
    public class MassServiceSystem2 : Element
    {
        public int Queue { get; set; }
        public int Maxqueue { get; set; } = int.MaxValue;
        public int Failure { get; set; }
        public List<MassServiceSystem2> NextMss { get; set; } = new List<MassServiceSystem2>();
        public bool NextDespose { get; set; } = false;
        public double DesposeProbability { get; set; } = 0.0;
        public List<Channel> Channels { get; set; } = new List<Channel>();
        public double SelfProbability { get; set; } = 0.0;

        //---------------Statistics-----------------
        public double DeltaTR { get; set; } // for average loading
        public double SumQueue { get; set; } // for average queue
        public double FinishTime { get; set; }


        public MassServiceSystem2(double delay) : base(delay)
        {
            Queue = 0;
            Maxqueue = int.MaxValue;
            NextDespose = false;
            TNext = double.MaxValue;
        }

        private bool CheckFreeChannels()
        {
            foreach (var channel in Channels)
            {
                if (channel.State == 0)
                    return true;
            }
            return false;
        }

        private Channel GetFreeChannel()
        {
            return Channels.Where(x => x.State == 0).First();
        }

        private double GetTNext()
        {
            return Channels.Min(x => x.TNext);
        }

        private Channel GetChannelByTNext()
        {
            return Channels.Where(x => x.TNext == TNext).First();
        }

        private void SetTCurrForChannels()
        {
            Channels.ForEach(x => x.TCurr = TCurr);
        }

        override public void InAct()
        {
            SetTCurrForChannels();
            if (base.State == 0)
            {
                if (CheckFreeChannels() == true)
                {
                    GetFreeChannel().InAct();
                    if (CheckFreeChannels() == false)
                    {
                        base.State = 1;
                    }
                }

                base.TNext = GetTNext();
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
                    //Console.WriteLine("--------Failure----------");
                }
            }
        }

        override public void OutAct()
        {
            SetTCurrForChannels();
            base.OutAct();
            GetChannelByTNext().OutAct();

            base.TNext = GetTNext();

            if (CheckFreeChannels() == true)
            {
                base.State = 0;
            }


            if (this.Queue > 0 && CheckFreeChannels() == true)
            {
                Queue--;
                GetFreeChannel().InAct();
                if (CheckFreeChannels() == false)
                {
                    base.State = 1;
                }
                base.TNext = GetTNext();
            }

            if (NextMss.Count > 0)
            {
                int index = 0;

                if(NextDespose == true)
                {
                    Random random = new Random();
                    double value = random.NextDouble();
                    if(value<0.5)
                    {
                        MassServiceSystem2 nextProcess = NextMss[index];
                    }
                    else
                    {
                        // Console.WriteLine("--------Dispose--------");
                    }
                }
                else
                {
                    MassServiceSystem2 nextProcess = NextMss[index];
                    nextProcess.InAct();
                }
            }
            else
            {
                // Console.WriteLine("--------Dispose--------");
            }


        }

        override public void PrintInfo()
        {
            base.PrintInfo();
            foreach (var channel in Channels)
                channel.PrintInfo();

            // Console.WriteLine("queue = " + this.Queue);
        }
        override public void DoStatistics(double delta)
        {
            SumQueue += this.Queue * delta;

            if (base.TNext != double.MaxValue)
                FinishTime = TNext;

            DeltaTR += (delta * State);

        }
    }
}
