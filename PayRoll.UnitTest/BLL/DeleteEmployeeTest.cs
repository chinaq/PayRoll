using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PayRoll.BLL;
using NUnit.Framework;

namespace PayRoll.UnitTest.BLL
{
    [TestFixture]
    public class DeleteEmployeeTest : SetUpInmemoryDb
    {
        [Test]
        public void ExecuteTest()
        {
            AddCommissionedEmployee t =
                new AddCommissionedEmployee(
                    7, "Bill", "Home", 2500, 3.2, database);
            t.Execute();

            Employee e = database.GetEmployee(7);
            Assert.IsNotNull(e);

            DeleteEmployeeTransaction dt =
                new DeleteEmployeeTransaction(7, database);
            dt.Execute();

            e = database.GetEmployee(7);
            Assert.IsNull(e);

            e = database.GetEmployee(7);
            Assert.IsNull(e);
        }
    }
}
