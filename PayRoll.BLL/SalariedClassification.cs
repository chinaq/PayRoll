using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PayRoll.BLL
{
    public class SalariedClassification : PaymentClassification
    {
        private readonly double salary;

        public double Salary
        {
            get { return salary; }
        }

        public SalariedClassification(double salary)
        {
            this.salary = salary;
        }

        public double Calculate(PayCheck payCheck)
        {
            return salary;
        }

    }
}
