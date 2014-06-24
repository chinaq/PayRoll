using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PayRoll.BLL
{
    public class ChangeSalariedTransaction:ChangeClassificationTranscation
    {
        
        private readonly double salary;

        public ChangeSalariedTransaction(int empId, double salary, PayrollDatabase database)
            : base(empId, database)
        {
            this.salary = salary;
        }



        protected override PaymentClassification GetClassification()
        {
            return new SalariedClassification(salary);
        }

        protected override PaymentSchedule GetSchedule()
        {
            return new MonthlySchedule();
        }
    }
}
