using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PayRoll.BLL
{
    public class ChangeHoldTransaction:ChangeMethodTranscation
    {
        public ChangeHoldTransaction(int empId)
            : base(empId)
        { }


        protected override PaymentMethod Method
        {
            get { return new HoldMethod(); }
        }
    }
}
