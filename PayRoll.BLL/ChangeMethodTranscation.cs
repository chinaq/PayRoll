using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PayRoll.BLL
{
    public abstract class ChangeMethodTranscation:ChangeEmployeeTransaction
    {
        public ChangeMethodTranscation(int empId)
            : base(empId)
        { }


        protected abstract PaymentMethod Method { get; }

        protected override void Change(Employee emp)
        {
            emp.Method = Method;
        }
    }
}
