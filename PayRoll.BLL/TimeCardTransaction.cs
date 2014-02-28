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


        public TimeCardTransaction(DateTime date, double hours, int empId)
            : base()
        {
            this.date = date;
            this.hours = hours;
            this.empId = empId;
        }


        public void Execute()
        {
            Employee emp = PayrollDatabase.GetEmployee(empId);

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
