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
        public void ExecuteTest_PayHourlyEmployeeNoTimeCard()
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

            ValidatePaycheck(pt, empId, payDay, 0);
        }


        [Test]
        public void ExecuteTest_PayHourlyEmployeeOneTimeCard()
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
            ValidatePaycheck(pt, empId, payDate, 30.5);
        }



        [Test]
        public void ExecuteTest_PayHourlyEmployeeOvertimeOneTimeCard()
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
            ValidatePaycheck(pt, empId, payDate, (8 + 1.5) * 15.25);
        }



        [Test]
        public void ExecuteTest_PayHourlyEmployeeOnWrongDate()
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
            ValidatePaycheck(pt, empId, payDate, 7 * 15.25);
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
            ValidatePaycheck(pt, empId, payDate, 2 * 15.25);
        }



        [Test]
        public void ExecuteTest_PaySingleCommissionedEmployeeNoReceipts()
        {
            int empId = 2;
            PayrollDatabase.DeleteEmployee(empId);
            AddCommissionedEmployee t = new AddCommissionedEmployee(empId, "Bill", "Home", 1500, 10);
            t.Execute();
            DateTime payDate = new DateTime(2018, 1, 5); // Payday
            PayDayTransaction pt = new PayDayTransaction(payDate);
            pt.Execute();
            ValidatePaycheck(pt, empId, payDate, 1500.0);
        }



        [Test]
        public void ExecuteTest_PaySingleCommissionedEmployeeOneReceipt()
        {
            int empId = 2;
            PayrollDatabase.DeleteEmployee(empId);
            AddCommissionedEmployee t = new AddCommissionedEmployee(empId, "Bill", "Home", 1500, 10);
            t.Execute();
            DateTime payDate = new DateTime(2018, 1, 5); // Payday

            SalesReceiptTransaction sr = new SalesReceiptTransaction(payDate, 5000.00, empId);
            sr.Execute();
            PayDayTransaction pt = new PayDayTransaction(payDate);
            pt.Execute();
            ValidatePaycheck(pt, empId, payDate, 2000.00);
        }



        [Test]
        public void ExecuteTest_PaySingleCommissionedEmployeeOnWrongDate()
        {
            int empId = 2;
            AddCommissionedEmployee t = new AddCommissionedEmployee(
                empId, "Bill", "Home", 1500, 10);
            t.Execute();
            DateTime payDate = new DateTime(2018, 1, 12); // wrong friday

            SalesReceiptTransaction sr =
                new SalesReceiptTransaction(payDate, 5000.00, empId);
            sr.Execute();
            PayDayTransaction pt = new PayDayTransaction(payDate);
            pt.Execute();

            PayCheck pc = pt.GetPayCheck(empId);
            Assert.IsNull(pc);
        }




        [Test]
        public void ExecuteTest_PaySingleCommissionedEmployeeTwoReceipts()
        {
            int empId = 2;
            AddCommissionedEmployee t = new AddCommissionedEmployee(
                empId, "Bill", "Home", 1500, 10);
            t.Execute();
            DateTime payDate = new DateTime(2018, 1, 5); // Payday

            SalesReceiptTransaction sr =
                new SalesReceiptTransaction(payDate, 5000.00, empId);
            sr.Execute();
            SalesReceiptTransaction sr2 = new SalesReceiptTransaction(
                payDate.AddDays(-1), 3500.00, empId);
            sr2.Execute();
            PayDayTransaction pt = new PayDayTransaction(payDate);
            pt.Execute();
            ValidatePaycheck(pt, empId, payDate, 2350.00);
        }



        [Test]
        public void ExecuteTest_PaySingleCommissionedEmployeeWithReceiptsSpanningTwoPayPeriods()
        {
            int empId = 2;
            AddCommissionedEmployee t = new AddCommissionedEmployee(
                empId, "Bill", "Home", 1500, 10);
            t.Execute();
            DateTime payDate = new DateTime(2018, 1, 5); // Payday

            SalesReceiptTransaction sr =
                new SalesReceiptTransaction(payDate, 5000.00, empId);
            sr.Execute();
            SalesReceiptTransaction sr2 = new SalesReceiptTransaction(
                payDate.AddDays(-15), 3500.00, empId);
            sr2.Execute();
            PayDayTransaction pt = new PayDayTransaction(payDate);
            pt.Execute();
            ValidatePaycheck(pt, empId, payDate, 2000.00);
        }



        private void ValidatePaycheck(PayDayTransaction pt, int empId, DateTime payDate, double pay)
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
