using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PayRoll.BLL
{
    public class ChangeNameTransaction:ChangeEmployeeTransaction
    {
        private readonly string name;

        public ChangeNameTransaction(int empId, string name):base(empId)
        {
            this.name = name;
        }

        protected override void Change(Employee emp)
        {
            emp.Name = name;
        }
    }
}
