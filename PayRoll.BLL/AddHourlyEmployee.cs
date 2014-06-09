using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PayRoll.BLL
{
    public class AddHourlyEmployee : AddEmployeeTransaction
    {
        private readonly double hourlyRate;

        public AddHourlyEmployee(int empid, string name, string address, double hourlyRate, PayrollDatabase database)
            : base(empid, name, address, database)
        {
            this.hourlyRate = hourlyRate;
        }

        protected override PaymentClassification MakeClassification()
        {
            return new HourlyClassification(hourlyRate);
        }

        protected override PaymentSchedule MakeSchedule()
        {
            return new WeeklySchedule();
        }

    
    }
}
