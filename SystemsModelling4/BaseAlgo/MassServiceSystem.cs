using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace BaseAlgo
{
    public class MassServiceSystem : Element
    {
        public int Queue { get; set; }
        public int Maxqueue { get; set; }
        public int Failure { get; set; }
        public List<MassServiceSystem> NextMss { get; set; } = new List<MassServiceSystem>();
        public bool NextDespose { get; set; }
        public List<Channel> Channels { get; set; } = new List<Channel>();

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
            return Channels.Where(x => x.State == 0).FirstOrDefault(); 
        }

        private double GetTNext()
        {
            return Channels.Min(x => x.TNext);
        }

        private Channel GetChannelByTNext()
        {
            return Channels.Where(x => x.TNext == TNext).FirstOrDefault();
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
                if(CheckFreeChannels() == true)
                {
                    GetFreeChannel().InAct();
                    if(CheckFreeChannels() == false)
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
                    Console.WriteLine("--------Failure----------");
                }
            }
        }

        override public void OutAct()
        {
            SetTCurrForChannels();
            base.OutAct();
            GetChannelByTNext().OutAct();

            base.TNext = GetTNext();

            if(CheckFreeChannels() == true)
            {
                base.State = 0;
            }     

            if (this.Queue > 0 && CheckFreeChannels()==true)
            {
                Queue--;
                GetFreeChannel().InAct();
                if(CheckFreeChannels()==false)
                {
                    base.State = 1;
                }
                base.TNext = GetTNext();
            }

            if(NextMss.Count>0)
            {
                Random random = new Random();
                int index = 0;
                if (NextDespose)
                {
                    index = random.Next(0, NextMss.Count+1);
                }
                else
                {
                    index = random.Next(0, NextMss.Count);
                }

                if(index==NextMss.Count)
                {
                    Console.WriteLine("--------Dispose--------");
                }
                else
                {
                    MassServiceSystem nextProcess = NextMss[index];
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
            foreach (var channel in Channels)
                channel.PrintInfo();
            Console.WriteLine("failure = " + this.Failure + " queue = " + this.Queue);
        }
        override public void DoStatistics(double delta)
        {
        }
    }
}
