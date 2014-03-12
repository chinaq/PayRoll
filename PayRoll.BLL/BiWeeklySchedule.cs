using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PayRoll.BLL
{
    public class BiWeeklySchedule : PaymentSchedule
    {
        private readonly DateTime startDay = new DateTime(1987, 12, 7);     //每双周进行结算，1987年12月7日，作为第一个周的周一

        public bool IsPayDate(DateTime payDay)
        {
            return payDay.DayOfWeek == DayOfWeek.Friday &&
                IsEvenWeek(payDay);
        }

        private bool IsEvenWeek(DateTime endDay)
        {
            TimeSpan timeDiff = endDay.Date - startDay.Date;
            int dateDiff = timeDiff.Days;
            return (dateDiff / 7) % 2 != 0;
        }

    }
}
