using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PayRoll.BLL
{
    public abstract class AddEmployeeTransaction:Transaction
    {
        private readonly int empid;
        private readonly string address;
        private readonly string name;

        public AddEmployeeTransaction(int empid, string name, string address)
        {
            this.empid = empid;
            this.name = name;
            this.address = address;
        }



        protected abstract PaymentClassification MakeClassification();
        protected abstract PaymentSchedule MakeSchedule();

        

        #region Transaction 成员

        public void Execute()
        {
            Employee emp = new Employee(empid, name, address);
            PaymentClassification pc = MakeClassification();
            PaymentSchedule ps = MakeSchedule();
            PaymentMethod pm = new HoldMethod();

            emp.Classification = pc;
            emp.Schedule = ps;
            emp.Method = pm;

            PayrollDatabase.AddEmployee(empid, emp);
        }

        #endregion
    }
}
