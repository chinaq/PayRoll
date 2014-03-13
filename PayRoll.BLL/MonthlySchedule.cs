using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PayRoll.BLL
{
    public class MonthlySchedule : PaymentSchedule
    {

        public bool IsPayDate(DateTime payDay)
        {
            return IsLastDayOfMonth(payDay);
        }

        private bool IsLastDayOfMonth(DateTime date)
        {
            int mon1 = date.Month;
            int mon2 = date.AddDays(1).Month;
            return (mon1 != mon2);
        }

        public DateTime GetStartDay(DateTime endDay)
        {
            int days = 0;
            while (endDay.AddDays(days - 1).Month == endDay.Month)
                days--;

            return endDay.AddDays(days);
        }

    }
}
