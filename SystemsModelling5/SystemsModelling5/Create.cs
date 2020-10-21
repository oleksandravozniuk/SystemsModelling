using System;
using System.Collections.Generic;
using System.Text;

namespace SystemsModelling5
{
    public class Create : Element
    {
        public MassServiceSystem NextElement { get; set; }

        public Create(double delay) : base(delay)
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
