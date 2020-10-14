using BaseAlgo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace AutoBank
{
    public class AutoBankMassServiceSystem : Element
    {
        public int Queue { get; set; }
        public int Maxqueue { get; set; }
        public int Failure { get; set; }
        public List<AutoBankMassServiceSystem> NextMss { get; set; } = new List<AutoBankMassServiceSystem>();
        public bool NextDespose { get; set; }
        public List<Channel> Channels { get; set; } = new List<Channel>();

        //---------------Statistics-----------------
        public double FinishTime { get; set; }
        public double DeltaTR { get; set; } // for average loading
        public double BankClientsCount { get; set; }
        public double LastDispose { get; set; }
        public double DisposeIntervalSum { get; set; }
        public double SumQueue { get; set; }


        public AutoBankMassServiceSystem(double delay) : base(delay)
        {
            Queue = 0;
            Maxqueue = int.MaxValue;
            NextDespose = false;
            TNext = double.MaxValue;

            FinishTime = 0.0;
            DeltaTR = 0.0;
            BankClientsCount = 0.0;
            LastDispose = 0.0;
            DisposeIntervalSum = 0.0;
            SumQueue = 0.0;
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
                    DisposeIntervalSum = DisposeIntervalSum + (TCurr - LastDispose);
                    LastDispose = TCurr;
                    Console.WriteLine("--------Dispose--------");
                }
                else
                {
                    AutoBankMassServiceSystem nextProcess = NextMss[index];
                    nextProcess.InAct();
                }
                
            }
            else
            {
                DisposeIntervalSum = DisposeIntervalSum + (TCurr - LastDispose);
                LastDispose = TCurr;
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
            if (base.TNext != double.MaxValue)
                FinishTime = TNext;
            DeltaTR += (delta * State);

            BankClientsCount = BankClientsCount + Queue + State;

            SumQueue = SumQueue + Queue;
        }
    }
}
