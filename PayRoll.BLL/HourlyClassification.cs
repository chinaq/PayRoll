﻿using System;
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

        public double Calculate(PayCheck payCheck)
        {
            double totalPay = 0;
            foreach (TimeCard timeCard in timeCards.Values)
            {
                if (DateUtil.IsInPayPeriod(payCheck.StartDay, payCheck.PayDay, timeCard.Date))
                {
                    totalPay += CalulatePayForTimeCard(timeCard);
                }
            }
            return totalPay;
        }


                
        private double CalulatePayForTimeCard(TimeCard timeCard)
        {
            double overTimeHours = Math.Max(0.0, timeCard.Hours - 8);
            double normalTimeHours = timeCard.Hours - overTimeHours;
            return (overTimeHours * 1.5 + normalTimeHours) * hourlyRate;
        }

    }
}
