﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PayRoll.BLL
{
    public interface PaymentClassification
    {
        double Calculate(PayCheck payCheck);
    }
}
