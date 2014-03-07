﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PayRoll.BLL
{
    public class Employee
    {
        private int empid;
        private string name;
        private string address;
        private PaymentClassification classification;
        private PaymentMethod method;
        private PaymentSchedule schedule;
        private Affiliation affiliation;

        

        public int Empid
        {
            get { return empid; }
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
            this.empid = empid;
            this.name = name;
            this.address = address;
        }
    }
}
