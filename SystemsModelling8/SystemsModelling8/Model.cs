using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;

namespace SystemsModelling8
{
    class Model
    {
        private List<Transition> transitions;
        private List<Place> places;
        private int iterationsCount;
        private Random random = new Random();

        public Model(List<Transition> transitions, List<Place> places, int iterationsCount)
        {
            this.transitions = transitions;
            this.places = places;
            this.iterationsCount = iterationsCount;
        }

        public void Simulate()
        {
            for (int i = 0; i < iterationsCount; i++)
            {
                List<Transition> availableTransitions = new List<Transition>();
                foreach (var t in transitions)
                {
                    if (t.IsAvailable())
                    {
                        availableTransitions.Add(t);
                    }
                }

                if (availableTransitions.Count == 0)
                    break;

                double probability = 1.0 / availableTransitions.Count;
                double value = random.NextDouble();

                double start = 0.0;
                double end = probability;

                foreach (var t in availableTransitions)
                {
                    if (value >= start && value < end)
                    {
                        Console.WriteLine("Transition: " + t.Name);
                        t.PerformTransition();
                        Console.WriteLine();
                        break;
                    }
                    else
                    {
                        start += probability;
                        end = start + probability;
                    }
                }

                foreach (var p in places)
                {
                    p.DoStatistics();
                }
            }

            DoStatistics();
        }

        public void DoStatistics()
        {
            List<double> typesCount = new List<double>();

            Console.WriteLine("-----------Statistics----------");
            foreach (var p in places)
            {
                Console.Write("Marker: " + p.Name + "    ");
                Console.Write("Max count: " + p.MaxMarkersCount + "    ");
                Console.Write("Avg count: " + ((double)p.MarkersSum / (double)p.iterationCount) + "    ");
                Console.Write("Min value: " + p.MinMarkersCount + "    ");
                Console.WriteLine(" ");
                if(p.Name== "COUNT OF TYPE 1" || p.Name == "COUNT OF TYPE 2" || p.Name == "COUNT OF TYPE 3")
                {
                    typesCount.Add(p.MaxMarkersCount);
                }
            }
            CountPercentage(typesCount);
        }

        private void CountPercentage(List<double> typesCount)
        {
            double sum = typesCount.Sum();
            for(int i=0;i<typesCount.Count;i++)
            {
                Console.WriteLine("Percentage type " +(i+1) + ": " + (typesCount[i] * 100.0) / sum);
            }
        }
    }
}
