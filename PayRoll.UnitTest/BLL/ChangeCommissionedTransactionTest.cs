using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PayRoll.BLL;
using NUnit.Framework;

namespace PayRoll.UnitTest.BLL
{
    [TestFixture]
    public class ChangeCommissionedTransactionTest
    {
        [Test]
        public void ExecuteTest()
        {
            int empId = 28;
            double baseRate = 2.3;
            double commissionRate = 5.6;

            AddEmployeeTransaction addSalEmp = new AddSalariedEmployee(empId, "kara", "samubola", 3000);
            addSalEmp.Execute();

            ChangeClassificationTranscation changeCommissionedTrans = new ChangeCommissionedTransaction(empId, baseRate, commissionRate);
            changeCommissionedTrans.Execute();
            Employee emp = PayrollDatabase.GetEmployee(empId);
            Assert.IsNotNull(emp);
            Assert.IsTrue(emp.Classification is CommissionClassification);

            CommissionClassification hc = emp.Classification as CommissionClassification;
            Assert.IsNotNull(hc);
            Assert.AreEqual(hc.BaseRate, baseRate);
            Assert.AreEqual(hc.CommissionRate, commissionRate);
        
        }
    }
}
