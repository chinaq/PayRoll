using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PayRoll.BLL
{
    public class ChangeHourlyTransaction: ChangeClassificationTranscation
    {
        private readonly double hourlyRate;

        public ChangeHourlyTransaction(int empId, double hourlyRate, PayrollDatabase database)
            : base(empId, database)
        {
            this.hourlyRate = hourlyRate;
        }



        protected override PaymentClassification GetClassification()
        {
            return new HourlyClassification(hourlyRate);
        }

        protected override PaymentSchedule GetSchedule()
        {
            return new WeeklySchedule();
        }
    }
}
