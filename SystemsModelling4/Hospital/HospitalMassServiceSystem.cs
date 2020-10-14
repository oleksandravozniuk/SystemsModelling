using BaseAlgo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hospital
{
    public class HospitalMassServiceSystem : Element
    {
        public int Queue { get; set; }
        public List<HospitalMassServiceSystem> NextMss { get; set; } = new List<HospitalMassServiceSystem>();
        public bool NextDespose { get; set; }
        public List<HospitalChannel> Channels { get; set; } = new List<HospitalChannel>();
        public List<Patient> PatientsInQueue { get; set; } = new List<Patient>();
        public Patient CurrentPatient { get; set; } = new Patient();

        //statistics
        public static List<Patient> Patients = new List<Patient>();


        public HospitalMassServiceSystem(double delay) : base(delay)
        {
            Queue = 0;
            NextDespose = false;
            TNext = double.MaxValue;
        }

        public bool CheckFreeChannels()
        {
            foreach (var channel in Channels)
            {
                if (channel.State == 0)
                    return true;
            }
            return false;
        }

        public HospitalChannel GetFreeChannel()
        {
            return Channels.Where(x => x.State == 0).FirstOrDefault();
        }

        public double GetTNext()
        {
            return Channels.Min(x => x.TNext);
        }

        public HospitalChannel GetChannelByTNext()
        {
            return Channels.Where(x => x.TNext == TNext).FirstOrDefault();
        }

        public void SetTCurrForChannels()
        {
            Channels.ForEach(x => x.TCurr = TCurr);
        }

        override public void InAct()
        {
            Console.WriteLine("In " + this.Name + ", Patient number " + CurrentPatient.Index + " Type = " + CurrentPatient.PatientType.Name);
            SetTCurrForChannels();
            if (base.State == 0)
            {
                if (CheckFreeChannels() == true)
                {
                    HospitalChannel hospitalChannel = (HospitalChannel)GetFreeChannel();
                    hospitalChannel.CurrentPatient = CurrentPatient;
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
            base.OutAct();
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
                hospitalChannel1.CurrentPatient = PatientsInQueue.First();
                PatientsInQueue.Remove(hospitalChannel1.CurrentPatient);
                hospitalChannel1.InAct();

                if (CheckFreeChannels() == false)
                {
                    base.State = 1;
                }
                base.TNext = GetTNext();
            }

            if (NextMss.Count > 0)
            {
                Random random = new Random();
                int index = 0;
                if (NextDespose)
                {
                    index = random.Next(0, NextMss.Count + 1);
                }
                else
                {
                    index = random.Next(0, NextMss.Count);
                }

                if (index == NextMss.Count)
                {
                    Console.WriteLine("Patient number " + patient.Index + " is in the chamber");
                    patient.finishTime = TCurr;
                    patient.timeInterval = patient.finishTime - patient.startTime;
                    Patients.Add(new Patient() { Index = patient.Index, PatientType = patient.PatientType, startTime = patient.startTime, finishTime = patient.finishTime, timeInterval = patient.timeInterval});
                }
                else
                {
                    HospitalMassServiceSystem nextProcess = NextMss[index];
                    nextProcess.CurrentPatient = patient;
                    nextProcess.InAct();
                }

            }
            else
            {
                Console.WriteLine("Patient number " + patient.Index + " is in the chamber");
                patient.finishTime = TCurr;
                patient.timeInterval = patient.finishTime - patient.startTime;
                Patients.Add(new Patient() { Index = patient.Index, PatientType = patient.PatientType, startTime = patient.startTime, finishTime = patient.finishTime, timeInterval = patient.timeInterval });
            }


        }

        override public void PrintInfo()
        {
            base.PrintInfo();
            foreach (var channel in Channels)
                channel.PrintInfo();
            Console.WriteLine(" queue = " + this.Queue);
        }
        override public void DoStatistics(double delta)
        {

        }
    }
}
