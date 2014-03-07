using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PayRoll.BLL;
using NUnit.Framework;

namespace PayRoll.UnitTest.BLL
{
    [TestFixture]
    public class ChangeSalariedTransactionTest
    {
        [Test]
        public void ExecuteTest()
        {
            int empId = 23;
            double salary = 1600;

            AddEmployeeTransaction addHourlyEmp = new AddHourlyEmployee(empId, "kara", "samubola", 3000);
            addHourlyEmp.Execute();

            ChangeClassificationTranscation changeHourlyTrans = new ChangeSalariedTransaction(empId, salary);
            changeHourlyTrans.Execute();
            Employee emp = PayrollDatabase.GetEmployee(empId);
            Assert.IsNotNull(emp);
            Assert.IsTrue(emp.Classification is SalariedClassifiction);

            SalariedClassifiction hc = emp.Classification as SalariedClassifiction;
            Assert.IsNotNull(hc);
            Assert.AreEqual(hc.Salary, salary);
        }
    }
}
