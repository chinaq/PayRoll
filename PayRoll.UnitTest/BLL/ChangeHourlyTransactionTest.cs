using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PayRoll.BLL;
using NUnit.Framework;

namespace PayRoll.UnitTest.BLL
{
    [TestFixture]
    public class ChangeHourlyTransactionTest
    {
        [Test]
        public void ExecuteTest()
        {
            int empId = 21;
            double hourlyRate = 2.3;

            AddEmployeeTransaction addSalEmp = new AddSalariedEmployee(empId, "kara", "samubola", 3000);
            addSalEmp.Execute();

            ChangeClassificationTranscation changeHourlyTrans = new ChangeHourlyTransaction(empId, hourlyRate);
            changeHourlyTrans.Execute();
            Employee emp = PayrollDatabase.GetEmployee(empId);
            Assert.IsNotNull(emp);
            Assert.IsTrue(emp.Classification is HourlyClassification);

            HourlyClassification hc = emp.Classification as HourlyClassification;
            Assert.IsNotNull(hc);
            Assert.AreEqual(hc.HourlyRate, hourlyRate);
        }
    }
}
