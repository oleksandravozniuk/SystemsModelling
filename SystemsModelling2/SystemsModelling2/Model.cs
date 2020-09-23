﻿using System;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;

namespace SystemsModelling2
{
    public class Model
    {
        private double tnext;//наступний момент часу
        private double tcurr;//поточний момент часу
        private double t0, t1;
        private double delayCreate, delayProcess;
        private int numCreate; // кількість створених запитів
        private int numProcess; 
        private int failure; //кількість відмов
        private int state, maxqueue, queue;
        private int nextEvent;//чи є на обробці події

        private double deltaT;

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
            t0 = tcurr; 
            t1 = Double.MaxValue;
            maxqueue = maxQ;
        }

        public void Simulate(double timeModelling)
        {
            List<double> tServices = new List<double>();
            int i = 0;

            double tServiceSum = 0;



            while(tcurr<timeModelling)
            {
                tnext = t0;
                nextEvent = 0;

                if (t1 < tnext)
                {
                    tnext = t1;
                    nextEvent = 1;
                }

                deltaT = deltaT + ((tnext - tcurr) * state);

                tcurr = tnext;

                if(nextEvent==0)
                {
                    tServices.Add(tcurr);
                }

                switch (nextEvent)
                {
                    case 0: event0(); break;
                    case 1: event1();tServiceSum = tServiceSum + (tServices[i] = tcurr - tServices[i]);i++; break;
                }

               // printInfo();
                
            }
            double rAver = deltaT / tnext;//середнє завантаження пристрою
            double tNet = tServiceSum/numProcess;//середній час очікування
            double failureProbability = (double)failure /(double) numCreate;//вірогідність відмови

            Console.WriteLine(delayCreate + "            " + delayProcess + "            " + maxqueue + "          " + rAver + "                " + tNet + "        " + failureProbability);

            //printStatistic();
        }

        public void event0()//подія надходження
        {
            t0 = tcurr + getDelayCreate();//момент часу надходження наступної події на обслуговування
            numCreate++;//збільшення кількості елементі, які вже обслугувались
            if (state == 0)
            {
                state = 1;// занято
                t1 = tcurr + getDelayProcess(); //момент часу закінчення обслуговування
            }
            else
            {
                if (queue < maxqueue) queue++;//додати елемент до черги
                else failure++;//додати елемент до числа відмов
            }
        }
        public void event1()//подія закінчення обслуговування на каналі
        {
            t1 = Double.MaxValue;
            state = 0;//канал вільний
            if (queue > 0)//якщо в черзі є елементи
            {
                queue--;//зменшити чергу
                state = 1;//перевести у стан занято канал
                t1 = tcurr + getDelayProcess();//момент часу закінчення обслуговування
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
            Console.WriteLine(" t= " + tcurr + " state = " + state + " queue = " + queue );
        }
        public void printInfo2()
        {
            Console.WriteLine("tcurr = " + tcurr + " tnext = " + tnext + " t0 = " + t0 + " t1 = " + t1 + "\n");
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