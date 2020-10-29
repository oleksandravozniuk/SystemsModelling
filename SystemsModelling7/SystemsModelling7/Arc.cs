using System;
using System.Collections.Generic;
using System.Text;

namespace SystemsModelling7
{
    class Arc
    {
        public string Name { get; set; }
        public Place Place { get; set; }
        public Transition Transition { get; set; }
        public bool FromPlaceToTransition;

        public Arc(string name, Place place, Transition transition, bool fromPlaceToTransition = true)
        {
            Name = name;
            Place = place;
            Transition = transition;
            FromPlaceToTransition = fromPlaceToTransition;
        }
    }
}
