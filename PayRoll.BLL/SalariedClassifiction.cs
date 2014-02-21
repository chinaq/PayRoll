using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PayRoll.BLL
{
    public class SalariedClassifiction : PaymentClassification
    {
        private readonly double salary;

        public double Salary
        {
            get { return salary; }
        }

        public SalariedClassifiction(double salary)
        {
            this.salary = salary;
        }


    }
}
