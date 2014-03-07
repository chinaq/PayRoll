using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PayRoll.BLL
{
    public class ServiceCharge
    {
        private readonly double amount;
        private readonly DateTime date;



        public double Amount
        {
            get { return amount; }
        }

        public DateTime Date
        {
            get { return date; }
        }



        public ServiceCharge(double amount, DateTime date)
        {
            this.amount = amount;
            this.date = date;
        }
    }
}
