using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace PayRoll.BLL
{
    public class HourlyClassification: PaymentClassification
    {
        private double hourlyRate;
        private Hashtable timeCards = new Hashtable();


        public double HourlyRate
        {
            get { return hourlyRate; }
        }

        public HourlyClassification(double hourlyRate)
        {
            this.hourlyRate = hourlyRate;
        }


        public void AddTimeCard(TimeCard tc)
        {
            timeCards[tc.Date] = tc;
        }

        public TimeCard GetTimeCard(DateTime date)
        {
            return timeCards[date] as TimeCard;
        }
    }
}
