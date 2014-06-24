using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PayRoll.BLL
{
    public class AddSalariedEmployee : AddEmployeeTransaction
    {
        private readonly double salary;


        public AddSalariedEmployee(int empid, string name, string address, double salary, PayrollDatabase database)
            : base(empid, name, address, database)
        {
            this.salary = salary;
        }


        protected override PaymentClassification MakeClassification()
        {
            return new SalariedClassification(salary);
        }

        protected override PaymentSchedule MakeSchedule()
        {
            return new MonthlySchedule();    
        }
    }
}
