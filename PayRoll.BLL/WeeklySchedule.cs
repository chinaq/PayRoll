using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PayRoll.BLL
{
    public class WeeklySchedule : PaymentSchedule
    {
        private bool IsFriday(DateTime date)
        {
            return date.DayOfWeek == DayOfWeek.Friday;
        }

        public bool IsPayDate(DateTime payDay)
        {
            return IsFriday(payDay);
        }


        public DateTime GetStartDay(DateTime endDay)
        {
            return endDay.AddDays(-6);
        }

    }
}
