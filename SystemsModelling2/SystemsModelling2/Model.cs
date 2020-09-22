using System;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;

namespace SystemsModelling2
{
    public class Model
    {
        private double tnext;
        private double tcurr;
        private double t0, t1;
        private double delayCreate, delayProcess;
        private int numCreate, numProcess, failure;
        private int state, maxqueue, queue;
        private int nextEvent;

        private ExpDistribution expDistribution = new ExpDistribution(); 

        public Model(double delayCr, double delayPr)
        {
            delayCreate = delayCr;
            delayProcess = delayPr;
            tnext = 0.0;
            nextEvent = 0;
            tcurr = tnext;
            t0 = tcurr; t1 = Double.MaxValue;
            maxqueue = 0;
        }

        public Model(double delayCr, double delayPr, int maxQ)
        {
            delayCreate = delayCr;
            delayProcess = delayPr;
            tnext = 0.0;
            nextEvent = 0;
            tcurr = tnext;
            t0 = tcurr; t1 = Double.MaxValue;
            maxqueue = maxQ;
        }

        public void Simulate(double timeModelling)
        {
            while(tcurr<timeModelling)
            {
                tnext = t0;
                nextEvent = 0;

                if (t1 < tnext)
                {
                    tnext = t1;
                    nextEvent = 1;
                }
                tcurr = tnext;

                switch (nextEvent)
                {
                    case 0: event0(); break;
                    case 1: event1();break;
                }
                printInfo();
            }
            printStatistic();
        }

        public void event0()
        {
            t0 = tcurr + getDelayCreate();
            numCreate++;
            if (state == 0)
            {
                state = 1;
                t1 = tcurr + getDelayProcess();
            }
            else
            {
                if (queue < maxqueue) queue++;
                else failure++;
            }
        }
        public void event1()
        {
            t1 = Double.MaxValue;
            state = 0;
            if (queue > 0)
            {
                queue--;
                state = 1;
                t1 = tcurr + getDelayProcess();
            }
            numProcess++;
        }

        public void printStatistic()
        {
            Console.WriteLine(" numCreate= " + numCreate +
                      " numProcess = " + numProcess + " failure = " + failure);
        }
        public void printInfo()
        {
            Console.WriteLine(" t= " + tcurr + " state = " + state + " queue = " + queue);
        }
        private double getDelayCreate()
        {
            return expDistribution.Exp(delayCreate);
        }
        private double getDelayProcess()
        {
            return expDistribution.Exp(delayProcess);
        }
    }


}