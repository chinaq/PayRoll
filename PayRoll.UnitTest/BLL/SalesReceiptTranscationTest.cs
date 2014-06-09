using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PayRoll.BLL;
using NUnit.Framework;

namespace PayRoll.UnitTest.BLL
{
    [TestFixture]
    public class SalesReceiptTranscationTest : SetUpInmemoryDb
    {
        [Test]
        public void ExecuteTest()
        {
            int empId = 12;
            DateTime dateTime = new DateTime(2013, 2, 5);
            double amount = 2300;

            AddEmployeeTransaction addEmpTrans = new AddCommissionedEmployee(empId, "Fasa", "quanwan", 2000, 3.12, database);
            addEmpTrans.Execute();
            SalesReceiptTransaction salesRecTrans = new SalesReceiptTransaction(dateTime, amount, empId, database);
            salesRecTrans.Execute();
            Employee emp = database.GetEmployee(empId);

            Assert.IsNotNull(emp);
            Assert.IsTrue(emp.Classification is CommissionClassification);

            CommissionClassification cc = emp.Classification as CommissionClassification;
            SalesReceipt sr = cc.GetSalesReceipt(dateTime);
            Assert.AreEqual(sr.Date, dateTime);
            Assert.AreEqual(sr.Amount, amount);
        }
    }
}
