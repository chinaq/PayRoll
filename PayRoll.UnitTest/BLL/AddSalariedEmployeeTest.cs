﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using PayRoll.BLL;


namespace PayRoll.UnitTest.BLL
{
    [TestFixture]
    public class AddSalariedEmployeeTest
    {
        [Test]
        public void TestAddSalariedEmployee()
        {
            AddEmployeeTransaction addEmp = new AddSalariedEmployee(1, "Bob", "Street Lasa", 1800);
            addEmp.Excute();
            Employee emp = PayrollDatabase.GetEmployee(1);
            Assert.IsNotNull(emp);

            PaymentClassification classifiction = emp.Classification;
            PaymentMethod method = emp.Method;
            PaymentSchedule schedule = emp.Schedule;

            Assert.AreEqual("Bob", emp.Name);
            Assert.IsTrue(classifiction is SalariedClassifiction);
            Assert.IsTrue(method is HoldMethod);
            Assert.IsTrue(schedule is MonthlySchedule);

            SalariedClassifiction salaryClassifiction = classifiction as SalariedClassifiction;
            Assert.AreEqual(1800, salaryClassifiction.Salary, 0.0001);
        }
    }
}
