using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PayRoll.BLL
{
    public abstract class ChangeAffiliationTransaction:ChangeEmployeeTransaction
    {
        public ChangeAffiliationTransaction(int empId)
            : base(empId)
        { }


        protected abstract Affiliation Affiliation { get; }
        protected abstract void RecordMembership(Employee emp);

        protected override void Change(Employee emp)
        {
            RecordMembership(emp);
            emp.Affiliation = Affiliation;
        }
    }
}
