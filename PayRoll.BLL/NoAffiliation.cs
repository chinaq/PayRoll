using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PayRoll.BLL
{
    public class NoAffiliation:Affiliation
    {
        public double CalculateDeduction(PayCheck payCheck)
        {
            return 0;
        }
    }
}
