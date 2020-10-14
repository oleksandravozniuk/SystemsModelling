using BaseAlgo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hospital
{
    public class HospitalCreate : Element
    {
        public EmergencyRoom NextElement { get; set; }

        public List<PatientType> PatientTypes { get; set; }

        private Random random = new Random();

        public static int CreateQuantity { get; set; }

        //Statistics
        public static int PatientType1 { get; set; }
        public static int PatientType2 { get; set; }
        public static int PatientType3 { get; set; }

        

        public HospitalCreate(double delay) : base(delay)
        {

        }

        override public void OutAct()
        {
            base.OutAct();
            CreateQuantity++;

            base.TNext = base.TCurr + base.GetDelay();

            PatientType patientType = FindPatientType();

            Patient patient = new Patient()
            {
                Index = CreateQuantity,
                startTime = TCurr,
                PatientType = new PatientType
                {
                    AvRegisterTime = patientType.AvRegisterTime,
                    Name = patientType.Name,
                    Frequency = patientType.Frequency
                }
            };

            NextElement.CurrentPatient = patient;

            NextElement.InAct();
        }

        private PatientType FindPatientType()
        {
            double a = random.NextDouble();
            
            if (a < PatientTypes[0].Frequency)
            {
                PatientType2++;
                return PatientTypes[0];
                
            }
            else
            {
                if (a >= PatientTypes[0].Frequency && a < PatientTypes[0].Frequency + PatientTypes[1].Frequency)
                {
                    PatientType3++;
                    return PatientTypes[1];
                    
                }
                else
                {
                    if (a >= PatientTypes[0].Frequency + PatientTypes[1].Frequency && a < PatientTypes[0].Frequency+PatientTypes[1].Frequency+PatientTypes[2].Frequency)
                    {
                        PatientType1++;
                        return PatientTypes[2];
                    }
                }
            }

            return PatientTypes[0];
        }
    }
    
}
