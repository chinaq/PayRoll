﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PayRoll.BLL
{
    public interface Affiliation
    {
        double CalculateDeduction(PayCheck payCheck);
    }
}
