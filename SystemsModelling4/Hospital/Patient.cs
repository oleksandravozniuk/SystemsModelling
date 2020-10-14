using System;
using System.Collections.Generic;
using System.Text;

namespace Hospital
{
    public class Patient
    {
        public PatientType PatientType { get; set; }
        public int Index {get;set;}

        public double startTime { get; set; }
        public double finishTime { get; set; }
        public double timeInterval { get; set; }
    }
}
