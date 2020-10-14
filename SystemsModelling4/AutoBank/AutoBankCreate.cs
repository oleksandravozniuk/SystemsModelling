using BaseAlgo;
using System;
using System.Collections.Generic;
using System.Text;

namespace AutoBank
{
    public class AutoBankCreate : Element
    {
        public List<AutoBankMassServiceSystem> NextElements { get; set; } = new List<AutoBankMassServiceSystem>();

        public AutoBankCreate(double delay):base(delay)
        {
        }

        override public void OutAct()
        {
            base.OutAct();
            base.TNext = base.TCurr + base.GetDelay();
            ChooseNextAutoBank().InAct();
        }

        private AutoBankMassServiceSystem ChooseNextAutoBank()
        {
            if(NextElements[0].Queue == NextElements[1].Queue)
            {
                return NextElements[0]; 
            }
            else
            {
                if(NextElements[0].Queue>NextElements[1].Queue)
                {
                    return NextElements[1];
                }
                else
                {
                    return NextElements[0];
                }
            }
        }

    } 
}

