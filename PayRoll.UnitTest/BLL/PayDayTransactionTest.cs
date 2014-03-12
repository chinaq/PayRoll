using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using PayRoll.BLL;

namespace PayRoll.UnitTest.BLL
{
    [TestFixture]
    public class PayDayTransactionTest
    {
        [Test]
        public void ExecuteTest_PaySingleSalariedEmployee()
        { 
            int empId =67;
            int memberId = 77;
            DateTime payDay = new DateTime(2014, 12, 31);
            PayrollDatabase.DeleteEmployee(empId);
            PayrollDatabase.RemoveUnionMember(memberId);

            AddEmployeeTransaction addSalEmp = new AddSalariedEmployee(empId, "lasa", "fala Street", 3000);
            addSalEmp.Execute();
            Employee emp = PayrollDatabase.GetEmployee(empId);
            Assert.IsTrue(emp.Method is HoldMethod);

            PayDayTransaction pt = new PayDayTransaction(payDay);
            pt.Execute();
            PayCheck payCheck = pt.GetPayCheck(empId);

            Assert.IsNotNull(payCheck);
            Assert.AreEqual(payCheck.GrossPay, 3000, 0.01);
            Assert.AreEqual(payCheck.Deductions, 0, 0.01);
            Assert.AreEqual(payCheck.NetPay, 3000, 0.01);
            Assert.AreEqual(payCheck.PayDate, payDay);
            Assert.AreEqual(payCheck.GetField("Disposition"), "Hold");
        }



        [Test]
        public void ExecuteTest_PayingHourlyEmployeeNoTimeCard()
        {
            int empId = 89;
            int memberId = 907;
            DateTime payDay = new DateTime(2001, 11, 9);
            PayrollDatabase.DeleteEmployee(empId);
            PayrollDatabase.RemoveUnionMember(memberId);

            AddEmployeeTransaction addHourlyEmp = new AddHourlyEmployee(empId, "lasa", "fala Street", 30);
            addHourlyEmp.Execute();
            Employee emp = PayrollDatabase.GetEmployee(empId);
            Assert.IsTrue(emp.Method is HoldMethod);

            PayDayTransaction pt = new PayDayTransaction(payDay);
            pt.Execute();
            PayCheck payCheck = pt.GetPayCheck(empId);

            ValidateHourlyPaycheck(pt, empId, payDay, 0);
        }


        [Test]
        public void ExecuteTest_PayingHourlyEmployeeOneTimeCard()
        {
            int empId = 2;
            PayrollDatabase.DeleteEmployee(empId);

            AddHourlyEmployee t = new AddHourlyEmployee(empId, "Bill", "Home", 15.25);
            t.Execute();
            DateTime payDate = new DateTime(2001, 11, 9);   //Friday

            TimeCardTransaction tc = new TimeCardTransaction(payDate, 2.0, empId);
            tc.Execute();
            PayDayTransaction pt = new PayDayTransaction(payDate);
            pt.Execute();
            ValidateHourlyPaycheck(pt, empId, payDate, 30.5);
        }



        [Test]
        public void ExecuteTest_PayingHourlyEmployeeOvertimeOneTimeCard()
        {            
            int empId = 2;
            PayrollDatabase.DeleteEmployee(empId);

            AddHourlyEmployee t = new AddHourlyEmployee(empId, "Bill", "Home", 15.25);
            t.Execute();
            DateTime payDate = new DateTime(2001, 11, 9);   //Friday

            TimeCardTransaction tc = new TimeCardTransaction(payDate, 9.0, empId);
            tc.Execute();
            PayDayTransaction pt = new PayDayTransaction(payDate);
            pt.Execute();
            ValidateHourlyPaycheck(pt, empId, payDate, (8 + 1.5) * 15.25);
        }



        [Test]
        public void ExecuteTest_PayingHourlyEmployeeOnWrongDate()
        {
            int empId = 2;
            PayrollDatabase.DeleteEmployee(empId);

            AddHourlyEmployee t = new AddHourlyEmployee(empId, "Bill", "Home", 15.25);
            t.Execute();
            DateTime payDate = new DateTime(2001, 11, 7);   //Wednesday

            TimeCardTransaction tc = new TimeCardTransaction(payDate, 9.0, empId);
            tc.Execute();
            PayDayTransaction pt = new PayDayTransaction(payDate);
            pt.Execute();
            PayCheck pc = pt.GetPayCheck(empId);

            Assert.IsNull(pc);
        }



        [Test]
        public void ExecuteTest_PaySingleHourlyEmployeeTwoTimeCards()
        {
            int empId = 2;
            PayrollDatabase.DeleteEmployee(empId);

            AddHourlyEmployee t = new AddHourlyEmployee(
                empId, "Bill", "Home", 15.25);
            t.Execute();
            DateTime payDate = new DateTime(2001, 11, 9); // Friday

            TimeCardTransaction tc =
                new TimeCardTransaction(payDate, 2.0, empId);
            tc.Execute();
            TimeCardTransaction tc2 =
                new TimeCardTransaction(payDate.AddDays(-1), 5.0, empId);
            tc2.Execute();
            PayDayTransaction pt = new PayDayTransaction(payDate);
            pt.Execute();
            ValidateHourlyPaycheck(pt, empId, payDate, 7 * 15.25);
        }



        [Test]
        public void ExecuteTest_PaySingleHourlyEmployeeWithTimeCardsSpanningTwoPayPeriods()
        {
            int empId = 2;
            PayrollDatabase.DeleteEmployee(empId);

            AddHourlyEmployee t = new AddHourlyEmployee(empId, "Bill", "Home", 15.25);
            t.Execute();
            DateTime payDate = new DateTime(2001, 11, 9); // Friday
            DateTime dateInPreviousPayPeriod = new DateTime(2001, 10, 30);

            TimeCardTransaction tc = new TimeCardTransaction(payDate, 2.0, empId);
            tc.Execute();
            TimeCardTransaction tc2 = new TimeCardTransaction(dateInPreviousPayPeriod, 5.0, empId);
            tc2.Execute();
            PayDayTransaction pt = new PayDayTransaction(payDate);
            pt.Execute();
            ValidateHourlyPaycheck(pt, empId, payDate, 2 * 15.25);
        }



        private void ValidateHourlyPaycheck(PayDayTransaction pt, int empId, DateTime payDate, double pay)
        {
            PayCheck pc = pt.GetPayCheck(empId);
            Assert.IsNotNull(pc);

            Assert.AreEqual(payDate, pc.PayDate);
            Assert.AreEqual(pay, pc.GrossPay, 0.001);
            Assert.AreEqual("Hold", pc.GetField("Disposition"));
            Assert.AreEqual(0, pc.Deductions, 0.001);
            Assert.AreEqual(pay, pc.NetPay, 0.001);
        }
    }
}
