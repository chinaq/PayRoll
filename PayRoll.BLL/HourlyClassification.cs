using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PayRoll.BLL
{
    public class HourlyClassification: PaymentClassification
    {
        private double hourlyRate;

        public double HourlyRate
        {
            get { return hourlyRate; }
        }

        public HourlyClassification(double hourlyRate)
        {
            this.hourlyRate = hourlyRate;
        }
    }
}
