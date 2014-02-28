using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PayRoll.BLL
{
    public class TimeCard
    {
        private readonly DateTime date;
        private readonly double hours;

        public DateTime Date
        {
            get { return date; }
        }

        public double Hours
        {
            get { return hours; }
        }


        public TimeCard(DateTime date, double hours)
        {
            this.date = date;
            this.hours = hours;
        }


    }
}
