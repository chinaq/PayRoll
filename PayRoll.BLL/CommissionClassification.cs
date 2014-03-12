using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace PayRoll.BLL
{
    public class CommissionClassification : PaymentClassification
    {
        private double baseRate;
        private double commissionRate;
        private Hashtable salesReceipts = new Hashtable(); 


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

        public void AddSalesReceipt(SalesReceipt salesReceipt)
        {
            salesReceipts[salesReceipt.Date] = salesReceipt;
        }

        public SalesReceipt GetSalesReceipt(DateTime date)
        {
            return salesReceipts[date] as SalesReceipt;
        }


        public double Calculate(PayCheck payCheck)
        {
            double salesTotal = 0;
            foreach (SalesReceipt salesReceipt in salesReceipts.Values)
            {
                if (IsInPayPeriod(salesReceipt, payCheck.PayDate))
                {
                    salesTotal += salesReceipt.Amount;
                }
            }
            return baseRate + (salesTotal * commissionRate * 0.01);
        }


        private bool IsInPayPeriod(SalesReceipt salesReceipt, DateTime payPeriod)
        {
            DateTime payPeriodEndDate = payPeriod;
            DateTime payPeriodStartDate = payPeriod.AddDays(-13);
            return salesReceipt.Date <= payPeriodEndDate && salesReceipt.Date >= payPeriodStartDate;
        }
    }
}
