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
    }
}
