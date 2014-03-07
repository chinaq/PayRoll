using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PayRoll.BLL
{
    public abstract class ChangeClassificationTranscation:ChangeEmployeeTransaction
    {
        public ChangeClassificationTranscation(int empId):base(empId)
        {            
        }

        protected abstract PaymentClassification GetClassification();
        protected abstract PaymentSchedule GetSchedule();

        protected override void Change(Employee emp)
        {
            PaymentClassification classification = GetClassification();
            PaymentSchedule schedule = GetSchedule();
            emp.Classification = classification;
            emp.Schedule = schedule;
        }

    }
}
