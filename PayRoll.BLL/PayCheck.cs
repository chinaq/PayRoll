using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace PayRoll.BLL
{
    public class PayCheck
    {
        private readonly DateTime payDate;
        private double grossPay;
        private double deductions;
        private double netPay;
        private Hashtable fields = new Hashtable();



        public DateTime PayDate
        {
            get { return payDate; }
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



        public PayCheck(DateTime payDate)
        {
            this.payDate = payDate;
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
