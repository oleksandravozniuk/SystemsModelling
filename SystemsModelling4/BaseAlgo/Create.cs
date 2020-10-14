using System;
using System.Collections.Generic;
using System.Text;

namespace BaseAlgo
{
    public class Create : Element
    {
        public List<MassServiceSystem> NextElements { get; set; } = new List<MassServiceSystem>();

        public Create(double delay):base(delay)
        {
        }

        override public void OutAct()
        {
            base.OutAct();
            base.TNext = base.TCurr + base.GetDelay();
            ChooseNext().InAct();
        }

        private MassServiceSystem ChooseNext()
        {
           foreach(var element in NextElements)
            {
                if (element.State == 0)
                    return element;
            }
            return NextElements[0];
        }

    } 
}

