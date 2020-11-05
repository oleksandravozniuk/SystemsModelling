using System;
using System.Collections.Generic;

namespace SystemsModelling8
{
    class Program
    {
        static void Main(string[] args)
        {
            Task3(10.0);
        }

        public static void Task1()
        {
            //From A to B
            Place inputToA = new Place("INPUT TO A", 1);
            Place arrivedToA = new Place("ARRIVED TO A", 0);
            Place requstedToB = new Place("REQUESTED TO B", 0);
            Place gotPositiveAnswerFromB = new Place("GOT POSITIVE ANSWER FROM B", 0);
            Place sentToB = new Place("SENT TO B",0);
            Place gotMessageInA = new Place("GOT MESSAGE IN A", 0);
            Place countOfAllInA = new Place("COUNT OF ALL IN A", 0);
            //From B to A
            Place inputToB = new Place("INPUT TO B", 1);
            Place arrivedToB = new Place("ARRIVED TO B", 0);
            Place requstedToA = new Place("REQUESTED TO A", 0);
            Place gotPositiveAnswerFromA = new Place("GOT POSITIVE ANSWER FROM A", 0);
            Place sentToA = new Place("SENT TO A", 0);
            Place gotMessageInB = new Place("GOT MESSAGE IN B", 0);
            Place countOfAllInB = new Place("COUNT OF ALL IN B", 0);

            Place canBeSent = new Place("CAN BE SENT", 1);

            
            Transition arrivingToA = new Transition("ARRIVING TO A");
            Transition requestingToB = new Transition("REQUESTING TO B");
            Transition gettingPositiveAnswerFromB = new Transition("GETTING POSITIVE ANSWER FROM B");
            Transition sendToB = new Transition("SEND TO B");
            Transition getMessageInA = new Transition("GET MESSAGE IN A");
            Transition sendSuccessfulMessageFromA = new Transition("SEND SUCCESSFUL MESSAGE FROM A");

            Transition arrivingToB = new Transition("ARRIVING TO B");
            Transition requestingToA = new Transition("REQUESTING TO A");
            Transition gettingPositiveAnswerFromA = new Transition("GETTING POSITIVE ANSWER FROM A");
            Transition sendToA = new Transition("SEND TO A");
            Transition getMessageInB = new Transition("GET MESSAGE IN B");
            Transition sendSuccessfulMessageFromB = new Transition("SEND SUCCESSFUL MESSAGE FROM B");


            Arc arc1 = new Arc("From 'INPUT TO A' to 'ARRIVING TO A'",inputToA,arrivingToA);
            Arc arc2 = new Arc("From 'ARRIVING TO A' to 'INPUT TO A'",inputToA,arrivingToA);
            Arc arc3 = new Arc("From 'ARRIVING TO A' to 'ARRIVED TO A'", arrivedToA, arrivingToA);
            Arc arc4 = new Arc("From 'ARRIVED TO A' to 'REQUESTING TO B'", arrivedToA, requestingToB);
            Arc arc5 = new Arc("From 'REQUESTING TO B' to 'REQUESTED TO B'", requstedToB, requestingToB);
            Arc arc6 = new Arc("From 'REQUESTED TO B' to 'GETTING POSITIVE ANSWER FROM B'", requstedToB, gettingPositiveAnswerFromB);
            Arc arc7 = new Arc("From 'GETTING POSITIVE ANSWER FROM B' to 'GOT POSITIVE ANSWER FROM B'", gotPositiveAnswerFromB, gettingPositiveAnswerFromB);
            Arc arc8 = new Arc("From 'GOT POSITIVE ANSWER FROM B' to 'SEND TO B'", gotPositiveAnswerFromB, sendToB);
            Arc arc9 = new Arc("From 'SEND TO B' to 'SENT TO B'", sentToB, sendToB);
            Arc arc10 = new Arc("From 'SENT TO B' to 'GET MESSAGE IN B'", sentToB, getMessageInB);
            Arc arc11 = new Arc("From 'GET MESSAGE IN B' to 'GOT MESSAGE IN B'", gotMessageInB, getMessageInB);
            Arc arc12 = new Arc("From 'GOT MESSAGE IN B' to 'SEND SUCCESSFUL MESSAGE FROM B'", gotMessageInB, sendSuccessfulMessageFromB);
            Arc arc13 = new Arc("From 'SEND SUCCESSFUL MESSAGE FROM B' to 'COUNT OF ALL IN B'", countOfAllInB, sendSuccessfulMessageFromB);
            Arc arc27 = new Arc("From 'SEND SUCCESSFUL MESSAGE FROM B' to 'CAN BE SENT'", canBeSent, sendSuccessfulMessageFromB);
            Arc arc29 = new Arc("From 'CAN BE SENT' to 'GETTING POSITIVE ANSWER FROM B'", canBeSent, gettingPositiveAnswerFromB);

            Arc arc14 = new Arc("From 'INPUT TO B' to 'ARRIVING TO B'", inputToB, arrivingToB);
            Arc arc15 = new Arc("From 'ARRIVING TO B' to 'INPUT TO B'", inputToB, arrivingToB);
            Arc arc16 = new Arc("From 'ARRIVING TO B' to 'ARRIVED TO B'", arrivedToB, arrivingToB);
            Arc arc17 = new Arc("From 'ARRIVED TO B' to 'REQUESTING TO A'", arrivedToB, requestingToA);
            Arc arc18 = new Arc("From 'REQUESTING TO A' to 'REQUESTED TO A'", requstedToA, requestingToA);
            Arc arc19 = new Arc("From 'REQUESTED TO A' to 'GETTING POSITIVE ANSWER FROM A'", requstedToA, gettingPositiveAnswerFromA);
            Arc arc20 = new Arc("From 'GETTING POSITIVE ANSWER FROM A' to 'GOT POSITIVE ANSWER FROM A'", gotPositiveAnswerFromA, gettingPositiveAnswerFromA);
            Arc arc21 = new Arc("From 'GOT POSITIVE ANSWER FROM A' to 'SEND TO A'", gotPositiveAnswerFromA, sendToA);
            Arc arc22 = new Arc("From 'SEND TO A' to 'SENT TO A'", sentToA, sendToA);
            Arc arc23 = new Arc("From 'SENT TO A' to 'GET MESSAGE IN A'", sentToA, getMessageInA);
            Arc arc24 = new Arc("From 'GET MESSAGE IN A' to 'GOT MESSAGE IN A'", gotMessageInA, getMessageInA);
            Arc arc25 = new Arc("From 'GOT MESSAGE IN A' to 'SEND SUCCESSFUL MESSAGE FROM A'", gotMessageInA, sendSuccessfulMessageFromA);
            Arc arc26 = new Arc("From 'SEND SUCCESSFUL MESSAGE FROM A' to 'COUNT OF ALL IN A'", countOfAllInA, sendSuccessfulMessageFromA);
            Arc arc28 = new Arc("From 'SEND SUCCESSFUL MESSAGE FROM A' to 'CAN BE SENT'", canBeSent, sendSuccessfulMessageFromA);
            Arc arc30 = new Arc("From 'CAN BE SENT' to 'GETTING POSITIVE ANSWER FROM A'", canBeSent, gettingPositiveAnswerFromA);

            arrivingToA.ArcsIn.Add(arc1);
            arrivingToA.ArcsOut.Add(arc2);
            arrivingToA.ArcsOut.Add(arc3);
            requestingToB.ArcsIn.Add(arc4);
            requestingToB.ArcsOut.Add(arc5);
            gettingPositiveAnswerFromB.ArcsIn.Add(arc6);
            gettingPositiveAnswerFromB.ArcsOut.Add(arc7);
            gettingPositiveAnswerFromB.ArcsIn.Add(arc29);
            sendToB.ArcsIn.Add(arc8);
            sendToB.ArcsOut.Add(arc9);
            getMessageInB.ArcsIn.Add(arc10);
            getMessageInB.ArcsOut.Add(arc11);
            sendSuccessfulMessageFromB.ArcsIn.Add(arc12);
            sendSuccessfulMessageFromB.ArcsOut.Add(arc13);
            sendSuccessfulMessageFromB.ArcsOut.Add(arc27);

            arrivingToB.ArcsIn.Add(arc14);
            arrivingToB.ArcsOut.Add(arc15);
            arrivingToB.ArcsOut.Add(arc16);
            requestingToA.ArcsIn.Add(arc17);
            requestingToA.ArcsOut.Add(arc18);
            gettingPositiveAnswerFromA.ArcsIn.Add(arc19);
            gettingPositiveAnswerFromA.ArcsOut.Add(arc20);
            gettingPositiveAnswerFromA.ArcsIn.Add(arc30);
            sendToA.ArcsIn.Add(arc21);
            sendToA.ArcsOut.Add(arc22);
            getMessageInA.ArcsIn.Add(arc23);
            getMessageInA.ArcsOut.Add(arc24);
            sendSuccessfulMessageFromA.ArcsIn.Add(arc25);
            sendSuccessfulMessageFromA.ArcsOut.Add(arc26);
            sendSuccessfulMessageFromA.ArcsOut.Add(arc28);

            Model model = new Model(new List<Transition> 
            { 
                arrivingToA,
                requestingToB,
                gettingPositiveAnswerFromB,
                sendToB,
                getMessageInB,
                sendSuccessfulMessageFromB,
                arrivingToB,
                requestingToA,
                gettingPositiveAnswerFromA,
                sendToA,
                getMessageInA,
                sendSuccessfulMessageFromA
            }, 
            new List<Place> 
            {
                inputToA,
                arrivedToA,
                requstedToB,
                gotPositiveAnswerFromB,
                sentToB,
                gotMessageInB,
                countOfAllInB,
                inputToB,
                arrivedToB,
                requstedToA,
                gotPositiveAnswerFromA,
                sentToA,
                gotMessageInA,
                countOfAllInA,
                canBeSent
            }, 
            100);
            model.Simulate();
        }

        public static void Task2()
        {
            Place input = new Place("INPUT", 1);
            Place buffer = new Place("BUFFER", 0,5);
            Place count = new Place("COUNT", 0);

            Transition producer = new Transition("PRODUCER");
            Transition consumer = new Transition("CONSUMER");

            Arc arc1 = new Arc("From INPUT to PRODUCER",input,producer);
            Arc arc2 = new Arc("From PRODUCER to INPUT", input, producer);
            Arc arc3 = new Arc("From PRODUCER to BUFFER", buffer, producer);
            Arc arc4 = new Arc("From BUFFER to CONSUMER", buffer, consumer);
            Arc arc5 = new Arc("From CONSUMER to COUNT", count, consumer);

            producer.ArcsIn.Add(arc1);
            producer.ArcsOut.Add(arc2);
            producer.ArcsOut.Add(arc3);
            consumer.ArcsIn.Add(arc4);
            consumer.ArcsOut.Add(arc5);

            Model model = new Model(new List<Transition>() { producer, consumer }, new List<Place> { input, buffer, count },100);
            model.Simulate();
        }

        public static void Task3(double n)
        {
            Place inputType1 = new Place("INPUT TYPE 1",1);
            Place arrivedType1 = new Place("ARRIVED TYPE 1", 0);
            Place countOfType1 = new Place("COUNT OF TYPE 1", 0);

            Place inputType2 = new Place("INPUT TYPE 2", 1);
            Place arrivedType2 = new Place("ARRIVED TYPE 2", 0);
            Place countOfType2 = new Place("COUNT OF TYPE 2", 0);

            Place inputType3 = new Place("INPUT TYPE 3", 1);
            Place arrivedType3 = new Place("ARRIVED TYPE 3", 0);
            Place countOfType3 = new Place("COUNT OF TYPE 3", 0);

            Place resource = new Place("RESOURCE", n);

            Transition arrivingType1 = new Transition("ARRIVING TYPE 1");
            Transition processingType1 = new Transition("PROCESSING TYPE 1");

            Transition arrivingType2 = new Transition("ARRIVING TYPE 2");
            Transition processingType2 = new Transition("PROCESSING TYPE 2");

            Transition arrivingType3 = new Transition("ARRIVING TYPE 3");
            Transition processingType3 = new Transition("PROCESSING TYPE 3");

            Arc arc1 = new Arc("From 'INPUT TYPE 1' to 'ARRIVING TYPE 1'", inputType1, arrivingType1);
            Arc arc2 = new Arc("From 'ARRIVING TYPE 1' to 'INPUT TYPE 1'", inputType1, arrivingType1);
            Arc arc3 = new Arc("From 'ARRIVING TYPE 1' to 'ARRIVED TYPE 1'", arrivedType1, arrivingType1);
            Arc arc4 = new Arc("From 'ARRIVED TYPE 1' to 'PROCESSING TYPE 1'", arrivedType1, processingType1);
            Arc arc5 = new Arc("From 'PROCESSING TYPE 1' to 'COUNT OF TYPE 1'", countOfType1, processingType1);
            Arc arc6 = new Arc("From 'PROCESSING TYPE 1' to 'RESOURCE'", resource, processingType1,n);
            Arc arc7 = new Arc("From 'RESOURCE' to 'PROCESSING TYPE 1'", resource, processingType1,n);

            Arc arc8 = new Arc("From 'INPUT TYPE 2' to 'ARRIVING TYPE 2'", inputType2, arrivingType2);
            Arc arc9 = new Arc("From 'ARRIVING TYPE 2' to 'INPUT TYPE 2'", inputType2, arrivingType2);
            Arc arc10 = new Arc("From 'ARRIVING TYPE 2' to 'ARRIVED TYPE 2'", arrivedType2, arrivingType2);
            Arc arc11 = new Arc("From 'ARRIVED TYPE 2' to 'PROCESSING TYPE 2'", arrivedType2, processingType2);
            Arc arc12 = new Arc("From 'PROCESSING TYPE 2' to 'COUNT OF TYPE 2'", countOfType2, processingType2);
            Arc arc13 = new Arc("From 'PROCESSING TYPE 2' to 'RESOURCE'", resource, processingType2, n/3.0);
            Arc arc14 = new Arc("From 'RESOURCE' to 'PROCESSING TYPE 2'", resource, processingType1, n/3.0);

            Arc arc15 = new Arc("From 'INPUT TYPE 3' to 'ARRIVING TYPE 3'", inputType3, arrivingType3);
            Arc arc16 = new Arc("From 'ARRIVING TYPE 3' to 'INPUT TYPE 3'", inputType3, arrivingType3);
            Arc arc17 = new Arc("From 'ARRIVING TYPE 3' to 'ARRIVED TYPE 3'", arrivedType3, arrivingType3);
            Arc arc18 = new Arc("From 'ARRIVED TYPE 3' to 'PROCESSING TYPE 3'", arrivedType3, processingType3);
            Arc arc19 = new Arc("From 'PROCESSING TYPE 3' to 'COUNT OF TYPE 3'", countOfType3, processingType3);
            Arc arc20 = new Arc("From 'PROCESSING TYPE 3' to 'RESOURCE'", resource, processingType3, n / 2.0);
            Arc arc21 = new Arc("From 'RESOURCE' to 'PROCESSING TYPE 3'", resource, processingType1, n / 2.0);

            arrivingType1.ArcsIn.Add(arc1);
            arrivingType1.ArcsOut.Add(arc2);
            arrivingType1.ArcsOut.Add(arc3);
            processingType1.ArcsIn.Add(arc4);
            processingType1.ArcsOut.Add(arc5);
            processingType1.ArcsOut.Add(arc6);
            processingType1.ArcsIn.Add(arc7);

            arrivingType2.ArcsIn.Add(arc8);
            arrivingType2.ArcsOut.Add(arc9);
            arrivingType2.ArcsOut.Add(arc10);
            processingType2.ArcsIn.Add(arc11);
            processingType2.ArcsOut.Add(arc12);
            processingType2.ArcsOut.Add(arc13);
            processingType2.ArcsIn.Add(arc14);

            arrivingType3.ArcsIn.Add(arc15);
            arrivingType3.ArcsOut.Add(arc16);
            arrivingType3.ArcsOut.Add(arc17);
            processingType3.ArcsIn.Add(arc18);
            processingType3.ArcsOut.Add(arc19);
            processingType3.ArcsOut.Add(arc20);
            processingType3.ArcsIn.Add(arc21);

            Model model = new Model(new List<Transition>()
            {
                arrivingType1,processingType1,arrivingType2,processingType2,arrivingType3,processingType3
            },
            new List<Place>()
            {
                inputType1,arrivedType1,countOfType1,inputType2,arrivedType2,countOfType2,inputType3,arrivedType3,countOfType3,resource
            },
            1000
            );
            model.Simulate();
        }
    }
}
