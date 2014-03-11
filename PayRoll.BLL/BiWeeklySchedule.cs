using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PayRoll.BLL
{
    public class BiWeeklySchedule : PaymentSchedule
    {

        public bool IsPayDate(DateTime payDay)
        {
            return payDay.DayOfWeek == DayOfWeek.Friday && 
                payDay.Day % 2 == 0;
        }

    }
}
