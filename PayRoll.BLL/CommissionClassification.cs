using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PayRoll.BLL
{
    public class CommissionClassification : PaymentClassification
    {
        private double baseRate;
        private double commissionRate;

        public double BaseRate
        {
            get { return baseRate; }
        }

        public double CommissionRate
        {
            get { return commissionRate; }
        }

        public CommissionClassification(double baseRate, double commissionRate)
        {
            this.baseRate = baseRate;
            this.commissionRate = commissionRate;
        }
    }
}
