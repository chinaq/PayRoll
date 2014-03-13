using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace PayRoll.BLL
{
    public class PayCheck
    {
        private readonly DateTime startDay;
        private DateTime payDay;
        private double grossPay;
        private double deductions;
        private double netPay;
        private Hashtable fields = new Hashtable();



        public DateTime StartDay
        {
            get { return startDay; }
        }

        public DateTime PayDay
        {
            get { return payDay; }
        }

        public double GrossPay
        {
            get { return grossPay; }
            set { grossPay = value; }
        }

        public double Deductions
        {
            get { return deductions; }
            set { deductions = value; }
        }

        public double NetPay
        {
            get { return netPay; }
            set { netPay = value; }
        }



        public PayCheck(DateTime startDay, DateTime payDay)
        {
            this.startDay = startDay;
            this.payDay = payDay;
        }



        public string GetField(string fieldName)
        {
            return fields[fieldName] as string;
        }

        public void SetField(string fieldName, string fieldValue)
        {
            fields[fieldName] = fieldValue;
        }

    }
}
