using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PayRoll.BLL
{
    public class ChangeAddressTransaction:ChangeEmployeeTransaction
    {
        private readonly string address;

        public ChangeAddressTransaction(int empId, string address)
            : base(empId)
        {
            this.address = address;
        }

        protected override void Change(Employee emp)
        {
            emp.Address = address;
        }
    }
}
