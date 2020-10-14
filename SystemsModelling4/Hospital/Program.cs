using BaseAlgo;
using System;
using System.Collections.Generic;
using System.Threading.Channels;

namespace Hospital
{
    class Program
    {
        static void Main(string[] args)
        {
            
            HospitalCreate c = new HospitalCreate(15)
            {
                Name = "CREATOR",
                Distribution = "exp"
            };


            EmergencyRoom emergencyRoom = new EmergencyRoom(15)
            {
                Name = "EMERGENCY ROOM",
                Distribution = "exp"
            };

            HospitalMassServiceSystem goToChamber = new HospitalMassServiceSystem(3)
            {
                DelayDev = 8,
                Name = "GO TO CHAMBER",
                Distribution = "unif",
                NextDespose = true
            };

            HospitalMassServiceSystem goToRegistration = new HospitalMassServiceSystem(2)
            {
                DelayDev = 5,
                Name = "GO TO REGISTRATION",
                Distribution = "unif"
            };

            HospitalMassServiceSystem registration = new HospitalMassServiceSystem(4.5)
            {
                DelayDev = 3,
                Name = "REGISTRATION",
                Distribution = "erl"
            };

            Laboratory laboratory = new Laboratory(4)
            {
                DelayDev = 2,
                Name = "LABORATORY",
                Distribution = "erl",
                NextDespose = true
            };

            GoToEmergencyRoom goToEmergencyRoom = new GoToEmergencyRoom(2)
            {
                DelayDev = 5,
                Name = "GO TO EMERGENCY ROOM",
                Distribution = "unif"
            };

            //add channels
            emergencyRoom.Channels = new List<HospitalChannel>
            {
                new HospitalChannel
                {
                   Name = "ER Channel 1"
                },
                new HospitalChannel
                {
                    Name = "ER Channel 2"
                }
            };

            goToChamber.Channels = new List<HospitalChannel>
            {
                new HospitalChannel
                {
                    Name = "Chamber Channel 1"
                },
                new HospitalChannel
                {
                    Name = "Chamber Channel 2"
                },
                new HospitalChannel
                {
                    Name = "Chamber Channel 3"
                }
            };

            goToRegistration.Channels = new List<HospitalChannel>
            {
                new HospitalChannel
                {
                    Name = "GoToReg Channel 1"
                },
                new HospitalChannel
                {
                    Name = "GoToReg Channel 2"
                },
                new HospitalChannel
                {
                    Name = "GoToReg Channel 3"
                },
                new HospitalChannel
                {
                    Name = "GoToReg Channel 4"
                },
                new HospitalChannel
                {
                    Name = "GoToReg Channel 5"
                }
            };

            registration.Channels = new List<HospitalChannel>
            {
                new HospitalChannel
                {
                    Name = "Reg Channel 1"
                }
            };

            laboratory.Channels = new List<HospitalChannel>
            {
                new HospitalChannel
                {
                    Name = "Lab Channel 1"
                },
                new HospitalChannel
                {
                    Name = "Lab Channel 2"
                }
            };

            goToEmergencyRoom.Channels = new List<HospitalChannel>
            {
                new HospitalChannel
                {
                    Name = "GoToER Channel 1"
                },
                new HospitalChannel
                {
                    Name = "GoToER Channel 2"
                },
                new HospitalChannel
                {
                    Name = "GoToER Channel 3"
                },
                new HospitalChannel
                {
                    Name = "GoToER Channel 4"
                },
                new HospitalChannel
                {
                    Name = "GoToER Channel 5"
                },
            };

            //add patient types
            c.PatientTypes = new List<PatientType>
            {
               
                new PatientType
                {
                    Name = "PatientType2",
                    Frequency = 0.1,
                    AvRegisterTime = 40
                },
                new PatientType
                {
                    Name = "PatientType3",
                    Frequency = 0.4,
                    AvRegisterTime = 30
                },
                 new PatientType
                {
                    Name = "PatientType1",
                    Frequency = 0.5,
                    AvRegisterTime = 15
                },
            };

            //add relations
            c.NextElement = emergencyRoom;
            emergencyRoom.NextMss.Add(goToChamber);
            emergencyRoom.NextMss.Add(goToRegistration);
            goToRegistration.NextMss.Add(registration);
            registration.NextMss.Add(laboratory);
            laboratory.NextMss.Add(goToEmergencyRoom);
            goToEmergencyRoom.NextMss.Add(emergencyRoom);

            //simulate
            List<Element> list = new List<Element> {c, emergencyRoom, goToChamber, goToRegistration, registration, laboratory, goToEmergencyRoom};
            HospitalModel model = new HospitalModel(list);
            model.Simulate(1000.0);

        }
    }
}
