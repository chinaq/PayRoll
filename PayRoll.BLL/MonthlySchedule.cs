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

    }
}
