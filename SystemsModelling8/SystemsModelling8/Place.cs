using System;
using System.Collections.Generic;
using System.Text;

namespace SystemsModelling8
{
    class Place
    {
        public string Name { get; set; }
        public double MarkersCount { get; set; }
        public double MaxAcceptedMarkersCount { get; set; }

        public double MaxMarkersCount { get; set; } = 0;
        public double MinMarkersCount { get; set; }
        public double MarkersSum { get; set; } = 0;
        public int iterationCount { get; set; } = 0;


        public Place(string name, double markerCount = 0.0, double maxAcceptedMarkersCount = double.MaxValue)
        {
            Name = name;
            MarkersCount = markerCount;
            MinMarkersCount = markerCount;
            MaxAcceptedMarkersCount = maxAcceptedMarkersCount;
        }

        public void DoStatistics()
        {
            if (MarkersCount > MaxMarkersCount)
            {
                MaxMarkersCount = MarkersCount;
            }

            if (MarkersCount < MinMarkersCount)
            {
                MinMarkersCount = MarkersCount;
            }

            MarkersSum += MarkersCount;
            iterationCount++;
        }
    }
}
