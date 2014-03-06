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
    
    }
}
