﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PayRoll.BLL
{
    public class HoldMethod : PaymentMethod
    {
        public void Pay(PayCheck payCheck)
        {
            payCheck.SetField("Disposition", "Hold");
        }
    }
}
