using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PayRoll.BLL
{
    public class DateUtil
    {
        public static bool IsInPayPeriod(DateTime startDay, DateTime endDay, DateTime payDay)
        {
            return (payDay >= startDay && payDay <= endDay);
            
        }
    }
}
