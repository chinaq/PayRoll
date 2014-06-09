using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using PayRoll.BLL;

namespace PayRoll.UnitTest.BLL
{
    [TestFixture]
    public class PayDayTransactionTest:SetUpInmemoryDb
    {
        [Test]
        public void ExecuteTest_PaySingleSalariedEmployee()
        { 
            int empId =67;
            int memberId = 77;
            DateTime payDay = new DateTime(2014, 12, 31);
            database.DeleteEmployee(empId);
            database.RemoveUnionMember(memberId);

            AddEmployeeTransaction addSalEmp = new AddSalariedEmployee(empId, "lasa", "fala Street", 3000, database);
            addSalEmp.Execute();
            Employee emp = database.GetEmployee(empId);
            Assert.IsTrue(emp.Method is HoldMethod);

            PayDayTransaction pt = new PayDayTransaction(payDay, database);
            pt.Execute();
            PayCheck payCheck = pt.GetPayCheck(empId);

            Assert.IsNotNull(payCheck);
            Assert.AreEqual(payCheck.GrossPay, 3000, 0.01);
            Assert.AreEqual(payCheck.Deductions, 0, 0.01);
            Assert.AreEqual(payCheck.NetPay, 3000, 0.01);
            Assert.AreEqual(payCheck.PayDay, payDay);
            Assert.AreEqual(payCheck.GetField("Disposition"), "Hold");
        }



        [Test]
        public void ExecuteTest_PayHourlyEmployeeNoTimeCard()
        {
            int empId = 89;
            int memberId = 907;
            DateTime payDay = new DateTime(2001, 11, 9);
            database.DeleteEmployee(empId);
            database.RemoveUnionMember(memberId);

            AddEmployeeTransaction addHourlyEmp = new AddHourlyEmployee(empId, "lasa", "fala Street", 30, database);
            addHourlyEmp.Execute();
            Employee emp = database.GetEmployee(empId);
            Assert.IsTrue(emp.Method is HoldMethod);

            PayDayTransaction pt = new PayDayTransaction(payDay, database);
            pt.Execute();
            PayCheck payCheck = pt.GetPayCheck(empId);

            ValidatePaycheck(pt, empId, payDay, 0, 0);
        }


        [Test]
        public void ExecuteTest_PayHourlyEmployeeOneTimeCard()
        {
            int empId = 2;
            database.DeleteEmployee(empId);

            AddHourlyEmployee t = new AddHourlyEmployee(empId, "Bill", "Home", 15.25, database);
            t.Execute();
            DateTime payDate = new DateTime(2001, 11, 9);   //Friday

            TimeCardTransaction tc = new TimeCardTransaction(payDate, 2.0, empId, database);
            tc.Execute();
            PayDayTransaction pt = new PayDayTransaction(payDate, database);
            pt.Execute();
            ValidatePaycheck(pt, empId, payDate, 30.5, 0);
        }



        [Test]
        public void ExecuteTest_PayHourlyEmployeeOvertimeOneTimeCard()
        {            
            int empId = 2;
            database.DeleteEmployee(empId);

            AddHourlyEmployee t = new AddHourlyEmployee(empId, "Bill", "Home", 15.25, database);
            t.Execute();
            DateTime payDate = new DateTime(2001, 11, 9);   //Friday

            TimeCardTransaction tc = new TimeCardTransaction(payDate, 9.0, empId, database);
            tc.Execute();
            PayDayTransaction pt = new PayDayTransaction(payDate, database);
            pt.Execute();
            ValidatePaycheck(pt, empId, payDate, (8 + 1.5) * 15.25, 0);
        }



        [Test]
        public void ExecuteTest_PayHourlyEmployeeOnWrongDate()
        {
            int empId = 2;
            database.DeleteEmployee(empId);

            AddHourlyEmployee t = new AddHourlyEmployee(empId, "Bill", "Home", 15.25, database);
            t.Execute();
            DateTime payDate = new DateTime(2001, 11, 7);   //Wednesday

            TimeCardTransaction tc = new TimeCardTransaction(payDate, 9.0, empId, database);
            tc.Execute();
            PayDayTransaction pt = new PayDayTransaction(payDate, database);
            pt.Execute();
            PayCheck pc = pt.GetPayCheck(empId);

            Assert.IsNull(pc);
        }



        [Test]
        public void ExecuteTest_PaySingleHourlyEmployeeTwoTimeCards()
        {
            int empId = 2;
            database.DeleteEmployee(empId);

            AddHourlyEmployee t = new AddHourlyEmployee(
                empId, "Bill", "Home", 15.25, database);
            t.Execute();
            DateTime payDate = new DateTime(2001, 11, 9); // Friday

            TimeCardTransaction tc =
                new TimeCardTransaction(payDate, 2.0, empId, database);
            tc.Execute();
            TimeCardTransaction tc2 =
                new TimeCardTransaction(payDate.AddDays(-1), 5.0, empId, database);
            tc2.Execute();
            PayDayTransaction pt = new PayDayTransaction(payDate, database);
            pt.Execute();
            ValidatePaycheck(pt, empId, payDate, 7 * 15.25, 0);
        }



        [Test]
        public void ExecuteTest_PaySingleHourlyEmployeeWithTimeCardsSpanningTwoPayPeriods()
        {
            int empId = 2;
            database.DeleteEmployee(empId);

            AddHourlyEmployee t = new AddHourlyEmployee(empId, "Bill", "Home", 15.25, database);
            t.Execute();
            DateTime payDate = new DateTime(2001, 11, 9); // Friday
            DateTime dateInPreviousPayPeriod = new DateTime(2001, 10, 30);

            TimeCardTransaction tc = new TimeCardTransaction(payDate, 2.0, empId, database);
            tc.Execute();
            TimeCardTransaction tc2 = new TimeCardTransaction(dateInPreviousPayPeriod, 5.0, empId, database);
            tc2.Execute();
            PayDayTransaction pt = new PayDayTransaction(payDate, database);
            pt.Execute();
            ValidatePaycheck(pt, empId, payDate, 2 * 15.25, 0);
        }



        [Test]
        public void ExecuteTest_PaySingleCommissionedEmployeeNoReceipts()
        {
            int empId = 2;
            database.DeleteEmployee(empId);
            AddCommissionedEmployee t = new AddCommissionedEmployee(empId, "Bill", "Home", 1500, 10, database);
            t.Execute();
            DateTime payDate = new DateTime(2018, 1, 5); // Payday
            PayDayTransaction pt = new PayDayTransaction(payDate, database);
            pt.Execute();
            ValidatePaycheck(pt, empId, payDate, 1500.0, 0);
        }



        [Test]
        public void ExecuteTest_PaySingleCommissionedEmployeeOneReceipt()
        {
            int empId = 2;
            database.DeleteEmployee(empId);
            AddCommissionedEmployee t = new AddCommissionedEmployee(empId, "Bill", "Home", 1500, 10, database);
            t.Execute();
            DateTime payDate = new DateTime(2018, 1, 5); // Payday

            SalesReceiptTransaction sr = new SalesReceiptTransaction(payDate, 5000.00, empId, database);
            sr.Execute();
            PayDayTransaction pt = new PayDayTransaction(payDate, database);
            pt.Execute();
            ValidatePaycheck(pt, empId, payDate, 2000.00, 0);
        }



        [Test]
        public void ExecuteTest_PaySingleCommissionedEmployeeOnWrongDate()
        {
            int empId = 2;
            AddCommissionedEmployee t = new AddCommissionedEmployee(
                empId, "Bill", "Home", 1500, 10, database);
            t.Execute();
            DateTime payDate = new DateTime(2018, 1, 12); // wrong friday

            SalesReceiptTransaction sr =
                new SalesReceiptTransaction(payDate, 5000.00, empId, database);
            sr.Execute();
            PayDayTransaction pt = new PayDayTransaction(payDate, database);
            pt.Execute();

            PayCheck pc = pt.GetPayCheck(empId);
            Assert.IsNull(pc);
        }




        [Test]
        public void ExecuteTest_PaySingleCommissionedEmployeeTwoReceipts()
        {
            int empId = 2;
            AddCommissionedEmployee t = new AddCommissionedEmployee(
                empId, "Bill", "Home", 1500, 10, database);
            t.Execute();
            DateTime payDate = new DateTime(2018, 1, 5); // Payday

            SalesReceiptTransaction sr =
                new SalesReceiptTransaction(payDate, 5000.00, empId, database);
            sr.Execute();
            SalesReceiptTransaction sr2 = new SalesReceiptTransaction(
                payDate.AddDays(-1), 3500.00, empId, database);
            sr2.Execute();
            PayDayTransaction pt = new PayDayTransaction(payDate, database);
            pt.Execute();
            ValidatePaycheck(pt, empId, payDate, 2350.00, 0);
        }



        [Test]
        public void ExecuteTest_PaySingleCommissionedEmployeeWithReceiptsSpanningTwoPayPeriods()
        {
            int empId = 2;
            AddCommissionedEmployee t = new AddCommissionedEmployee(
                empId, "Bill", "Home", 1500, 10, database);
            t.Execute();
            DateTime payDate = new DateTime(2018, 1, 5); // Payday

            SalesReceiptTransaction sr =
                new SalesReceiptTransaction(payDate, 5000.00, empId, database);
            sr.Execute();
            SalesReceiptTransaction sr2 = new SalesReceiptTransaction(
                payDate.AddDays(-15), 3500.00, empId, database);
            sr2.Execute();
            PayDayTransaction pt = new PayDayTransaction(payDate, database);
            pt.Execute();
            ValidatePaycheck(pt, empId, payDate, 2000.00, 0);
        }



        [Test]
        public void ExecuteTest_SalariedUnionMemberDues()
        {
            int empId = 1;
            AddSalariedEmployee t = new AddSalariedEmployee(
                empId, "Bob", "Home", 1000.00, database);
            t.Execute();
            int memberId = 7734;
            ChangeMemberTransaction cmt =
                new ChangeMemberTransaction(empId, memberId, 9.42, database);
            cmt.Execute();
            DateTime payDate = new DateTime(2014, 3, 31);   //有4个周五
            PayDayTransaction pt = new PayDayTransaction(payDate, database);
            pt.Execute();

            ValidatePaycheck(pt, empId, payDate, 1000.0, 9.42 * 4);
        }



        [Test]
        public void ExecuteTest_HourlyUnionMemberServiceCharge()
        {
            int empId = 1;
            AddHourlyEmployee t = new AddHourlyEmployee(
                empId, "Bill", "Home", 15.24, database);
            t.Execute();
            int memberId = 7734;
            ChangeMemberTransaction cmt =
                new ChangeMemberTransaction(empId, memberId, 9.42, database);
            cmt.Execute();
            DateTime payDate = new DateTime(2001, 11, 9);
            ServiceChargeTransaction sct =
                new ServiceChargeTransaction(memberId, 19.42, payDate, database);
            sct.Execute();
            TimeCardTransaction tct =
                new TimeCardTransaction(payDate, 8.0, empId, database);
            tct.Execute();
            PayDayTransaction pt = new PayDayTransaction(payDate, database);
            pt.Execute();

            ValidatePaycheck(pt, empId, payDate, 8 * 15.24, 9.42 + 19.42);
        }




        [Test]
        public void ExecuteTest_ServiceChargesSpanningMultiplePayPeriods()
        {
            int empId = 1;
            AddHourlyEmployee t = new AddHourlyEmployee(
                empId, "Bill", "Home", 15.24, database);
            t.Execute();
            int memberId = 7734;
            ChangeMemberTransaction cmt =
                new ChangeMemberTransaction(empId, memberId, 9.42, database);
            cmt.Execute();
            DateTime payDate = new DateTime(2001, 11, 9);
            DateTime earlyDate =
                new DateTime(2001, 11, 2); // previous Friday
            DateTime lateDate =
                new DateTime(2001, 11, 16); // next Friday
            ServiceChargeTransaction sct =
                new ServiceChargeTransaction(memberId, 19.42, payDate, database);
            sct.Execute();
            ServiceChargeTransaction sctEarly =
                new ServiceChargeTransaction(memberId, 100.00, earlyDate, database);
            sctEarly.Execute();
            ServiceChargeTransaction sctLate =
                new ServiceChargeTransaction(memberId, 200.00, lateDate, database);
            sctLate.Execute();
            TimeCardTransaction tct =
                new TimeCardTransaction(payDate, 8.0, empId, database);
            tct.Execute();
            PayDayTransaction pt = new PayDayTransaction(payDate, database);
            pt.Execute();

            ValidatePaycheck(pt, empId, payDate, 8 * 15.24, 9.42 + 19.42);
        }



        private void ValidatePaycheck(PayDayTransaction pt, int empId, DateTime payDate_Expe, double grossPay_Expe, double deductions_Expe)
        {
            PayCheck pc = pt.GetPayCheck(empId);
            Assert.IsNotNull(pc);

            Assert.AreEqual(payDate_Expe, pc.PayDay);
            Assert.AreEqual(grossPay_Expe, pc.GrossPay, 0.001);
            Assert.AreEqual("Hold", pc.GetField("Disposition"));
            Assert.AreEqual(deductions_Expe, pc.Deductions, 0.001);
            Assert.AreEqual(grossPay_Expe - deductions_Expe, pc.NetPay, 0.001);
        }
    }
}
