using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SystemsModelling5
{
    public class MassServiceSystem : Element
    {
        public int Queue { get; set; }
        public int Maxqueue { get; set; } = int.MaxValue;
        public int Failure { get; set; }
        public List<MassServiceSystem> NextMss { get; set; } = new List<MassServiceSystem>();
        public bool NextDespose { get; set; } = false;
        public double DesposeProbability { get; set; } = 0.0;
        public List<Channel> Channels { get; set; } = new List<Channel>();
        public double SelfProbability { get; set; } = 0.0;

        //---------------Statistics-----------------
        public double DeltaTR { get; set; } // for average loading
        public double SumQueue { get; set; } // for average queue
        public double FinishTime { get; set; }


        public MassServiceSystem(double delay) : base(delay)
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
                if(Name == "SMO 1")
                {
                    Random random = new Random();
                    double a = random.NextDouble();

                        if (a < NextMss[0].SelfProbability)
                        {
                            index = 0;
                        }
                        else
                        {
                            if (a >= NextMss[0].SelfProbability && a < NextMss[0].SelfProbability + NextMss[1].SelfProbability)
                            {
                                index = 1;
                            }
                            else
                            {
                                if (a >= NextMss[0].SelfProbability + NextMss[1].SelfProbability && a < NextMss[0].SelfProbability + NextMss[1].SelfProbability + NextMss[2].SelfProbability)
                                {
                                    index = 2;
                                }
                                else
                                    index = 3;
                            }
                        }
                }

                if(index == 3)
                {
                    //Console.WriteLine("--------Dispose--------");
                }
                else
                {
                    MassServiceSystem nextProcess = NextMss[index];
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
