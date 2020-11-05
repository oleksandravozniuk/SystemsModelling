using System;
using System.Collections.Generic;
using System.Text;

namespace SystemsModelling8
{
    class Arc
    {
        public string Name { get; set; }
        public Place Place { get; set; }
        public Transition Transition { get; set; }
        public double Multiplicity { get; set; }

        public Arc(string name, Place place, Transition transition, double multiplicity = 1.0)
        {
            Name = name;
            Place = place;
            Transition = transition;
            Multiplicity = multiplicity;
        }
    }
}
