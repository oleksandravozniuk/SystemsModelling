using System;
using System.Collections.Generic;
using System.Text;

namespace SystemsModelling7
{
    class Place
    {
        public string Name { get; set; }
        public int MarkersCount { get; set; } = 0;

        public int MaxMarkersCount { get; set; } = 0;
        public int MinMarkersCount { get; set; }
        public int MarkersSum { get; set; } = 0;
        public int iterationCount { get; set; } = 0;


        public Place(string name, int markerCount)
        {
            Name = name;
            MarkersCount = markerCount;
            MinMarkersCount = markerCount;
        }

        public void DoStatistics()
        {
            if(MarkersCount>MaxMarkersCount)
            {
                MaxMarkersCount = MarkersCount;
            }

            if(MarkersCount < MinMarkersCount)
            {
                MinMarkersCount = MarkersCount;
            }

            MarkersSum += MarkersCount;
            iterationCount++;
        }
    }
}
