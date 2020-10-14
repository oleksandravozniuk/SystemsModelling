using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hospital
{
    public class Laboratory : HospitalMassServiceSystem
    {
        public Laboratory(double delay) : base(delay)
        {
            Queue = 0;
            NextDespose = false;
            TNext = double.MaxValue;
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

                if (patient.PatientType.Name == "PatientType2")
                {
                    Console.WriteLine("Patient number " + patient.Index + " has left the hospital");
                    patient.finishTime = TCurr;
                    patient.timeInterval = patient.finishTime - patient.startTime;
                    Patients.Add(new Patient() { Index = patient.Index, PatientType = patient.PatientType, startTime = patient.startTime, finishTime = patient.finishTime, timeInterval = patient.timeInterval });
                }
                else
                {
                    HospitalMassServiceSystem nextProcess = NextMss.Where(x => x.Name == "GO TO EMERGENCY ROOM").First();
                    patient.finishTime = TCurr;
                    patient.timeInterval = patient.finishTime - patient.startTime;
                    Patients.Add(new Patient() { Index = patient.Index, PatientType = patient.PatientType, startTime = patient.startTime, finishTime = patient.finishTime, timeInterval = patient.timeInterval });

                    Patient newPatient = new Patient
                    {
                        PatientType = new PatientType
                        {
                            Name = "PatientType1",
                            Frequency = 0.5,
                            AvRegisterTime = 15
                        }
                    };
                    

                    nextProcess.CurrentPatient = newPatient;

                    nextProcess.InAct();
                }

        }

        private Patient GetPatientFromQueue()
        {
            Patient patientToRemove = PatientsInQueue.First();
            PatientsInQueue.Remove(patientToRemove);
            return patientToRemove;
        }
    }
}
