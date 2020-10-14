using BaseAlgo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hospital
{
    public class HospitalModel
    {
        private List<Element> list = new List<Element>();
        private double tnext, tcurr;
        private int _event;

        public HospitalModel(List<Element> elements)
        {
            list = elements;
            tnext = 0;
            _event = 0;
            tcurr = tnext;
        }

        public void Simulate(double time)
        {
            while (tcurr < time)
            {
                tnext = double.MaxValue;
                foreach (Element e in list)
                {
                    if (e.TNext < tnext)
                    {
                        tnext = e.TNext;
                        _event = e.Id;

                    }
                }

                Console.WriteLine("\nIt's time for event in " + list[_event].Name + ", time =   " + tnext);
                foreach (Element e in list)
                {
                    e.DoStatistics(tnext - tcurr);
                }
                tcurr = tnext;
                foreach (Element e in list)
                {
                    e.TCurr = tcurr;
                }
                list[_event].OutAct();
                foreach (Element e in list)
                {
                    if (e.TNext == tcurr)
                    {
                        e.OutAct();

                    }
                }

                PrintInfo();
            }

            PrintResult();
        }

        public void PrintInfo()
        {
            foreach (Element e in list)
            {

                    e.PrintInfo();
                
            }
        }

        public void PrintResult()
        {
            Console.WriteLine("\n-------------RESULTS-------------");
            Console.WriteLine("Count of patients: " + HospitalCreate.CreateQuantity);
            Console.WriteLine("PatientType1 Create: " + HospitalCreate.PatientType1);
            Console.WriteLine("PatientType2 Create: " + HospitalCreate.PatientType2);
            Console.WriteLine("PatientType3 Create: " + HospitalCreate.PatientType3);

            foreach (Element e in list)
            {
                e.PrintResult();
                Console.WriteLine();
                if (e.GetType() == typeof(MassServiceSystem))
                {
                    MassServiceSystem p = (MassServiceSystem)e;

                }
                
            }

            for(int i=0;i<HospitalMassServiceSystem.Patients.Count;i++)
            {
                Console.WriteLine("Patient " + HospitalMassServiceSystem.Patients[i].Index + " Type: " + HospitalMassServiceSystem.Patients[i].PatientType.Name + " time interval: " + HospitalMassServiceSystem.Patients[i].timeInterval);
            }

            double type1Interval = HospitalMassServiceSystem.Patients.Where(x => x.PatientType.Name == "PatientType1").Sum(x => x.timeInterval)/HospitalMassServiceSystem.Patients.Where(x=>x.PatientType.Name == "PatientType1").Count();
            double type2Interval = HospitalMassServiceSystem.Patients.Where(x => x.PatientType.Name == "PatientType2").Sum(x => x.timeInterval) / HospitalMassServiceSystem.Patients.Where(x => x.PatientType.Name == "PatientType2").Count();
            double type3Interval = HospitalMassServiceSystem.Patients.Where(x => x.PatientType.Name == "PatientType3").Sum(x => x.timeInterval) / HospitalMassServiceSystem.Patients.Where(x => x.PatientType.Name == "PatientType3").Count();

            Console.WriteLine("Average time interval:");
            Console.WriteLine("Type 1: " + type1Interval);
            Console.WriteLine("Type 2: " + type2Interval);
            Console.WriteLine("Type 3: " + type3Interval);

            List<double> intervals1 = new List<double>();
            for(int i = 1;i<EmergencyRoom.type1PatientsStartTime.Count;i++)
            {
                intervals1.Add(EmergencyRoom.type1PatientsStartTime[i] - EmergencyRoom.type1PatientsStartTime[i - 1]);
            }
            List<double> intervals2 = new List<double>();
            for (int i = 1; i < EmergencyRoom.type2PatientsStartTime.Count; i++)
            {
                intervals2.Add(EmergencyRoom.type2PatientsStartTime[i] - EmergencyRoom.type2PatientsStartTime[i - 1]);
            }
            List<double> intervals3 = new List<double>();
            for (int i = 1; i < EmergencyRoom.type3PatientsStartTime.Count; i++)
            {
                intervals3.Add(EmergencyRoom.type3PatientsStartTime[i] - EmergencyRoom.type3PatientsStartTime[i - 1]);
            }

            double avgInterval1 = intervals1.Sum() / intervals1.Count;
            double avgInterval2 = intervals2.Sum() / intervals2.Count;
            double avgInterval3 = intervals3.Sum() / intervals3.Count;

            Console.WriteLine("Average time interval between type1 patients come: " + avgInterval1);
            for(int i =0;i<intervals1.Count;i++)
            {
                Console.WriteLine(intervals1[i]);
            }

            Console.WriteLine("Average time interval between type2 patients come: " + avgInterval2);
            for (int i = 0; i < intervals2.Count; i++)
            {
                Console.WriteLine(intervals2[i]);
            }

            Console.WriteLine("Average time interval between type3 patients come: " + avgInterval3);
            for (int i = 0; i < intervals3.Count; i++)
            {
                Console.WriteLine(intervals3[i]);
            }

        }


    }
}
