using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using PayRoll.BLL;

namespace PayRoll.UnitTest.BLL
{
    [TestFixture]
    public class AddCommissionedEmployeeTest:SetUpInmemoryDb
    {

        [Test]
        public void ExecuteTest()
        {
            AddEmployeeTransaction addEmp = new AddCommissionedEmployee(3, "Dalai", "Bree Street", 0.6, 0.7, database);
            addEmp.Execute();
            Employee emp = database.GetEmployee(3);
            Assert.IsNotNull(emp);

            PaymentClassification classification = emp.Classification;
            PaymentMethod method = emp.Method;
            PaymentSchedule schedule = emp.Schedule;

            Assert.AreEqual("Dalai", emp.Name);
            Assert.IsTrue(classification is CommissionClassification);
            Assert.IsTrue(method is HoldMethod);
            Assert.IsTrue(schedule is BiWeeklySchedule);

            CommissionClassification hourlyClassifiction = classification as CommissionClassification;
            Assert.AreEqual(0.6, hourlyClassifiction.BaseRate, 0.0001);
            Assert.AreEqual(0.7, hourlyClassifiction.CommissionRate, 0.0001);
        }
    }
}
