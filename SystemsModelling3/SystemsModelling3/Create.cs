using System;
using System.Collections.Generic;
using System.Text;

namespace SystemsModelling3
{
    public class Create : Element
    {
        public Process NextElement { get; set; }
        public Create(double delay):base(delay)
        {
        }

        override public void OutAct()
        {
            base.OutAct();
            base.TNext = base.TCurr + base.GetDelay();
            NextElement.InAct();

        }

    } 
}

