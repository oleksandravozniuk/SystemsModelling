using System;
using System.Collections.Generic;
using System.Text;

namespace SystemsModelling5
{
    public class Channel : Element
    {
        public Channel()
        {
            TNext = Double.MaxValue;
        }
        override public void InAct()
        {
            base.State = 1;
            base.TNext = base.TCurr + base.GetDelay();

        }

        override public void OutAct()
        {
            base.OutAct();
            base.TNext = double.MaxValue;
            base.State = 0;
        }
    }
}
