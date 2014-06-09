using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PayRoll.BLL
{
    public class TimeCardTransaction : Transaction
    {
        private readonly int empId;
        private readonly DateTime date;
        private readonly double hours;


        public TimeCardTransaction(DateTime date, double hours, int empId, PayrollDatabase database)
            : base(database)
        {
            this.date = date;
            this.hours = hours;
            this.empId = empId;
        }


        public override void Execute()
        {
            Employee emp = database.GetEmployee(empId);

            if (emp != null)
            {
                HourlyClassification hc = emp.Classification as HourlyClassification;
                if (hc != null)
                {
                    TimeCard tc = new TimeCard(date, hours);
                    hc.AddTimeCard(tc);
                }
                else
                {
                    throw new ApplicationException("Tried to add timecard to non-hourly employee");
                }
            }
            else
            {
                throw new ApplicationException("No such employee.");
            }
            
        }

    }
}
