using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hospital
{
    public class GoToEmergencyRoom : HospitalMassServiceSystem
    {
        public GoToEmergencyRoom(double delay) : base(delay)
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

            HospitalMassServiceSystem nextProcess = NextMss.Where(x => x.Name == "EMERGENCY ROOM").First();

            HospitalCreate.CreateQuantity++;
            patient.Index = HospitalCreate.CreateQuantity;
            patient.startTime = TCurr;

            nextProcess.CurrentPatient = patient;
        }

        private Patient GetPatientFromQueue()
        {
            Patient patientToRemove = PatientsInQueue.First();
            PatientsInQueue.Remove(patientToRemove);
            return patientToRemove;
        }
    }
}
