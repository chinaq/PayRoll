using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PayRoll.BLL;
using NUnit.Framework;

namespace PayRoll.UnitTest.BLL
{
    [TestFixture]
    public class DeleteEmployeeTest
    {
        [Test]
        public void TestDeleteEmplyee()
        {
            AddCommissionedEmployee t =
                new AddCommissionedEmployee(
                    7, "Bill", "Home", 2500, 3.2);
            t.Execute();

            Employee e = PayrollDatabase.GetEmployee(7);
            Assert.IsNotNull(e);

            DeleteEmployeeTransaction dt =
                new DeleteEmployeeTransaction(7);
            dt.Execute();

            e = PayrollDatabase.GetEmployee(7);
            Assert.IsNull(e);

            e = PayrollDatabase.GetEmployee(7);
            Assert.IsNull(e);
        }
    }
}
