using BaseAlgo;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hospital
{
    public class HospitalChannel : Channel
    {
        public Patient CurrentPatient { get; set; } = new Patient();
    }
}
