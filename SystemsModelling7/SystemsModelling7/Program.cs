using System;
using System.Collections.Generic;

namespace SystemsModelling7
{
    class Program
    {
        static void Main(string[] args)
        {
            Place input = new Place("INPUT", 1);
            Place queue = new Place("QUEUE", 0);
            Place deviceIsFree = new Place("DEVICE IS FREE", 1);
            Place processed = new Place("OBJECT IS PROCESSED", 0);
            Place countOfGoodObjects = new Place("COUNT OF GOOD OBJECTS", 0);
            Place countOfBadObjects = new Place("COUNT OF BAD OBJECTS", 0);

            Transition inputing = new Transition("INPUTING");
            Transition processing = new Transition("PROCESSING");
            Transition beingGood = new Transition("BEING GOOD");
            Transition beingBad = new Transition("BEING BAD");

            Arc arc1 = new Arc("From INPUT to INPUTING", input, inputing);
            Arc arc2 = new Arc("From INPUTING to INPUT", input, inputing, false);
            Arc arc3 = new Arc("FROM INPUTING to QUEUE", queue, inputing, false);
            Arc arc4 = new Arc("From PROCESSING to DEVICE IS FREE", deviceIsFree, processing, false);
            Arc arc5 = new Arc("From DEVICE IS FREE to PROCESSING", deviceIsFree, processing);
            Arc arc6 = new Arc("From PROCESSING to PROCESSED", processed, processing, false);
            Arc arc7 = new Arc("FROM PROCESSED to BEING GOOD", processed, beingGood);
            Arc arc8 = new Arc("From PROCESSED to BEING BAD", processed, beingBad);
            Arc arc9 = new Arc("From BEING GOOD to COUNT OF GOOD OBJECTS", countOfGoodObjects, beingGood);
            Arc arc10 = new Arc("From BEING BAD to COUNT OF BAD OBJECTS", countOfBadObjects, beingBad);
            Arc arc11 = new Arc("From QUEUE to PROCESSING", queue, processing);

            inputing.ArcsIn.Add(arc1);
            inputing.ArcsOut.Add(arc2);
            inputing.ArcsOut.Add(arc3);
            processing.ArcsIn.Add(arc5);
            processing.ArcsIn.Add(arc11);
            processing.ArcsOut.Add(arc4);
            processing.ArcsOut.Add(arc6);
            beingGood.ArcsIn.Add(arc7);
            beingGood.ArcsOut.Add(arc9);
            beingBad.ArcsIn.Add(arc8);
            beingBad.ArcsOut.Add(arc10);

            Model model = new Model(new List<Transition> { inputing,processing,beingGood,beingBad },new List<Place> {input,queue,deviceIsFree,processed,countOfGoodObjects,countOfBadObjects }, 10);

            for(int i = 0;i<10;i++)
            {
                Console.WriteLine("INPUT " + 1);
                Console.WriteLine("QUEUE " + i);
                Console.WriteLine("DEVICE IS FREE " + 1);
                Console.WriteLine("OBJECT IS PROCESSED " + 0);
                Console.WriteLine("COUNT OF GOOD OBJECTS " + 0);
                Console.WriteLine("COUNT OF BAD OBJECTS " + 0);
                queue.MarkersCount = i;
                model.Simulate();
            }
           
        }
    }
}
