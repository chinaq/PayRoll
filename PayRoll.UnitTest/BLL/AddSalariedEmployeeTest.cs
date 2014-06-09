using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using PayRoll.BLL;


namespace PayRoll.UnitTest.BLL
{
    [TestFixture]
    public class AddSalariedEmployeeTest:SetUpInmemoryDb
    {
        [Test]
        public void ExecuteTest()
        {
            AddEmployeeTransaction addEmp = new AddSalariedEmployee(1, "Bob", "Street Lasa", 1800, database);
            addEmp.Execute();
            Employee emp = database.GetEmployee(1);
            Assert.IsNotNull(emp);

            PaymentClassification classifiction = emp.Classification;
            PaymentMethod method = emp.Method;
            PaymentSchedule schedule = emp.Schedule;

            Assert.AreEqual("Bob", emp.Name);
            Assert.IsTrue(classifiction is SalariedClassifiction);
            Assert.IsTrue(method is HoldMethod);
            Assert.IsTrue(schedule is MonthlySchedule);

            SalariedClassifiction salaryClassifiction = classifiction as SalariedClassifiction;     //as 不会报错，只会返回空
            Assert.AreEqual(1800, salaryClassifiction.Salary, 0.0001);
        }
    }
}
