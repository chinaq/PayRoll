using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PayRoll.BLL
{
    public class Employee
    {
        private int empId;
        private string name;
        private string address;
        private PaymentClassification classification;
        private PaymentMethod method;
        private PaymentSchedule schedule;
        private Affiliation affiliation;

        

        public int EmpId
        {
            get { return empId; }
        }

        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        public string Address
        {
            get { return address; }
            set { address = value; }
        }

        public PaymentClassification Classification
        {
            get { return classification; }
            set { classification = value; }
        }

        public PaymentMethod Method
        {
            get { return method; }
            set { method = value; }
        }

        public PaymentSchedule Schedule
        {
            get { return schedule; }
            set { schedule = value; }
        }

        public Affiliation Affiliation
        {
            get { return affiliation; }
            set { affiliation = value; }
        }



        public Employee(int empid, string name, string address)
        {
            this.empId = empid;
            this.name = name;
            this.address = address;
        }



        public bool IsPayDate(DateTime payDay)
        {
            return schedule.IsPayDate(payDay);
        }

        public void PayDay(PayCheck payCheck)
        {
            double grossPay  = classification.Calculate(payCheck);
            double deductions = affiliation.CalculateDeduction(payCheck);
            double netPay = grossPay - deductions;
            payCheck.GrossPay = grossPay;
            payCheck.Deductions = deductions;
            payCheck.NetPay = netPay;
            method.Pay(payCheck);
        }

        public DateTime GetStartDay(DateTime payDay)
        {
            return schedule.GetStartDay(payDay);
        }
    }
}
