using System;
using System.Collections.Generic;
using System.Text;

namespace SystemsModelling7
{
    class Model
    {
        private List<Transition> transitions;
        private List<Place> places;
        private int iterationsCount;
        private Random random = new Random();

        public Model(List<Transition> transitions,List<Place> places, int iterationsCount)
        {
            this.transitions = transitions;
            this.places = places;
            this.iterationsCount = iterationsCount;
        }

        public void Simulate()
        {
            for(int i = 0; i<iterationsCount;i++)
            {
                List<Transition> availableTransitions = new List<Transition>();
                foreach(var t in transitions)
                {
                    if(t.IsAvailable())
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

                foreach(var t in availableTransitions)
                {
                    if(value>=start && value<end)
                    {
                        //Console.WriteLine("Transition: " + t.Name);
                        t.PerformTransition();
                        //Console.WriteLine();
                        break;
                    }
                    else
                    {
                        start += probability;
                        end = start + probability;
                    }
                }

                foreach(var p in places)
                {
                    p.DoStatistics();
                }
            }

            DoStatistics();
        }

        public void DoStatistics()
        {
            Console.WriteLine("-----------Statistics----------");
            foreach(var p in places)
            {
                Console.Write("Marker: " + p.Name + "    ");
                Console.Write("Max count: " + p.MaxMarkersCount + "    ");
                Console.Write("Avg count: " + ((double)p.MarkersSum / (double)p.iterationCount) + "    ");
                Console.Write("Min value: " + p.MinMarkersCount + "    ");
                Console.WriteLine(" ");
            }
        }
    }
}
