using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PayRoll.BLL
{
    public class ChangeSalariedTransaction:ChangeClassificationTranscation
    {
        
        private readonly double salary;

        public ChangeSalariedTransaction(int empId, double salary)
            : base(empId)
        {
            this.salary = salary;
        }



        protected override PaymentClassification GetClassification()
        {
            return new SalariedClassifiction(salary);
        }

        protected override PaymentSchedule GetSchedule()
        {
            return new MonthlySchedule();
        }
    }
}
