using System;
using System.Collections.Generic;
using System.Text;

namespace SystemsModelling5
{
    public class Create2 : Element
    {
        public MassServiceSystem2 NextElement { get; set; }

        public Create2(double delay) : base(delay)
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
