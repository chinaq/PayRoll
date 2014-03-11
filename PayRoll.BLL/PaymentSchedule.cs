using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PayRoll.BLL
{
    public interface PaymentSchedule
    {
        bool IsPayDate(DateTime payDay);
    }
}
