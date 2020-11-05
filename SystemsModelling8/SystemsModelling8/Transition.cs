using System;
using System.Collections.Generic;
using System.Text;

namespace SystemsModelling8
{
    class Transition
    {
        public string Name { get; set; }
        public List<Arc> ArcsOut { get; set; } = new List<Arc>();
        public List<Arc> ArcsIn { get; set; } = new List<Arc>();

        public Transition(string name)
        {
            Name = name;
        }

        public bool IsAvailable()
        {
            foreach (var arc in ArcsIn)
            {
                if (arc.Place.MarkersCount < arc.Multiplicity)
                {
                    return false;
                }
            }

            foreach (var arc in ArcsOut)
            {
                if (arc.Multiplicity+arc.Place.MarkersCount > arc.Place.MaxAcceptedMarkersCount)
                {
                    return false;
                }
            }

            return true;
        }

        public void PerformTransition()
        {
            foreach (var arc in ArcsIn)
            {
                Console.WriteLine("Get marker from " + arc.Place.Name + " New Markers number: " + (arc.Place.MarkersCount-1));
                arc.Place.MarkersCount -= arc.Multiplicity;
            }

            foreach (var arc in ArcsOut)
            {
                Console.WriteLine("Set marker to " + arc.Place.Name + " New Markers number: " + (arc.Place.MarkersCount + 1));
                arc.Place.MarkersCount += arc.Multiplicity;
            }
        }
    }
}
