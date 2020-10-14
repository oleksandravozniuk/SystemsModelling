using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hospital
{
    public class EmergencyRoom : HospitalMassServiceSystem
    {
        public EmergencyRoom(double delay) : base(delay)
        {
            Queue = 0;
            NextDespose = false;
            TNext = double.MaxValue;
        }

        //statistics

        public static List<double> type1PatientsStartTime { get; set; } = new List<double>();
        public static List<double> type2PatientsStartTime { get; set; } = new List<double>();
        public static List<double> type3PatientsStartTime { get; set; } = new List<double>();

        override public void InAct()
        {
            Console.WriteLine("In " + this.Name + ", Patient number " + CurrentPatient.Index + " Type = " + CurrentPatient.PatientType.Name);
            switch(CurrentPatient.PatientType.Name)
            {
                case "PatientType1": type1PatientsStartTime.Add(CurrentPatient.startTime);break;
                case "PatientType2": type2PatientsStartTime.Add(CurrentPatient.startTime); break;
                case "PatientType3": type3PatientsStartTime.Add(CurrentPatient.startTime); break;

            };
            SetTCurrForChannels();
            if (base.State == 0)
            {
                if (CheckFreeChannels() == true)
                {
                    HospitalChannel hospitalChannel = GetFreeChannel();
                    hospitalChannel.CurrentPatient = CurrentPatient;
                    hospitalChannel.DelayMean = CurrentPatient.PatientType.AvRegisterTime;
                    hospitalChannel.InAct();
                    CurrentPatient = null;
                    if (CheckFreeChannels() == false)
                    {
                        base.State = 1;
                    }
                }

                base.TNext = GetTNext();
            }
            else
            {
                Queue++;
                PatientsInQueue.Add(CurrentPatient);
                CurrentPatient = null;
            }
        }

        override public void OutAct()
        {
            
            SetTCurrForChannels();
                Quantity++;
                HospitalChannel hospitalChannel = GetChannelByTNext();
            
                Patient patient = hospitalChannel.CurrentPatient;
            Console.WriteLine("Out " + this.Name + ", Patient number " + patient.Index + "  Type = " + patient.PatientType.Name);
            hospitalChannel.CurrentPatient = null;
                hospitalChannel.OutAct();

                base.TNext = GetTNext();

                if (CheckFreeChannels() == true)
                {
                    base.State = 0;
                }

                if (this.Queue > 0 && CheckFreeChannels() == true)
                {
                    Queue--;

                    HospitalChannel hospitalChannel1 = GetFreeChannel();
                    hospitalChannel1.CurrentPatient = GetPatientFromQueue();
                    hospitalChannel1.InAct();

                    if (CheckFreeChannels() == false)
                    {
                        base.State = 1;
                    }
                    base.TNext = GetTNext();
                }

                if (patient.PatientType.Name == "PatientType1")
                {
                    HospitalMassServiceSystem nextProcess = NextMss.Where(x => x.Name == "GO TO CHAMBER").First();
                    nextProcess.CurrentPatient = patient;
                    nextProcess.InAct();
                }
                else
                {
                    HospitalMassServiceSystem nextProcess = NextMss.Where(x => x.Name == "GO TO REGISTRATION").First();
                    nextProcess.CurrentPatient = patient;
                    nextProcess.InAct();
                }
            
            
            
        }

        private Patient GetPatientFromQueue()
        {
            Patient patientToRemove;
            foreach (var patient in PatientsInQueue)
            {
                if (patient.PatientType.Name == "PatientType1")
                {
                    patientToRemove = patient;
                    PatientsInQueue.Remove(patientToRemove);
                    return patient;
                }
            }

            patientToRemove = PatientsInQueue.First();
            PatientsInQueue.Remove(patientToRemove);
            return patientToRemove;
        }
    }
}
