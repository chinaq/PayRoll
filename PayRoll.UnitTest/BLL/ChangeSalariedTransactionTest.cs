using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PayRoll.BLL;
using NUnit.Framework;

namespace PayRoll.UnitTest.BLL
{
    [TestFixture]
    public class ChangeSalariedTransactionTest : SetUpInmemoryDb
    {
        [Test]
        public void ExecuteTest()
        {
            int empId = 23;
            double salary = 1600;

            AddEmployeeTransaction addHourlyEmp = new AddHourlyEmployee(empId, "kara", "samubola", 3000, database);
            addHourlyEmp.Execute();

            ChangeClassificationTranscation changeHourlyTrans = new ChangeSalariedTransaction(empId, salary, database);
            changeHourlyTrans.Execute();
            Employee emp = database.GetEmployee(empId);
            Assert.IsNotNull(emp);
            Assert.IsTrue(emp.Classification is SalariedClassification);

            SalariedClassification hc = emp.Classification as SalariedClassification;
            Assert.IsNotNull(hc);
            Assert.AreEqual(hc.Salary, salary);
        }
    }
}
