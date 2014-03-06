using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using NUnit.Framework;
using PayRoll.BLL;

namespace PayRoll.UnitTest.BLL
{
    [TestFixture]
    public class AddHourlyEmployeeTest
    {
        [Test]
        public void ExecuteTest()
        {
            AddEmployeeTransaction addEmp = new AddHourlyEmployee(2, "Cala", "Selee Street", 0.8);
            addEmp.Execute();
            Employee emp = PayrollDatabase.GetEmployee(2);
            Assert.IsNotNull(emp);

            PaymentClassification classification = emp.Classification;
            PaymentMethod method = emp.Method;
            PaymentSchedule schedule = emp.Schedule;

            Assert.AreEqual("Cala", emp.Name);
            Assert.IsTrue(classification is HourlyClassification);
            Assert.IsTrue(method is HoldMethod);
            Assert.IsTrue(schedule is WeeklySchedule);

            HourlyClassification hourlyClassifiction = classification as HourlyClassification;
            Assert.AreEqual(0.8, hourlyClassifiction.HourlyRate, 0.0001);
        }
    }
}
