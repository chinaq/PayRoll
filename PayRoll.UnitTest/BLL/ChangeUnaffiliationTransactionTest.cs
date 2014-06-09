using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using PayRoll.BLL;

namespace PayRoll.UnitTest.BLL
{
    [TestFixture]
    public class ChangeUnaffiliationTransactionTest: SetUpInmemoryDb
    {
        [Test]
        public void ExecuteTest()
        {
            int empId = 33;
            int memberId = 98;
            double dues = 98.9;

            database.DeleteEmployee(empId);
            database.RemoveUnionMember(memberId);

            AddSalariedEmployee addSalEmp = new AddSalariedEmployee(empId, "Masa", "Faav Street", 5000, database);
            addSalEmp.Execute();
            ChangeAffiliationTransaction changeMemberTrans = new ChangeMemberTransaction(empId, memberId, dues, database);
            changeMemberTrans.Execute();
            Employee empAff = database.GetEmployee(empId);
            Assert.IsNotNull(empAff);
            Assert.IsTrue(empAff.Affiliation is UnionAffiliation);


            ChangeAffiliationTransaction changeAffTrans = new ChangeUnaffiliatedTransaction(empId, database);
            changeAffTrans.Execute();
            Employee empNoAff = database.GetEmployee(empId);
            Assert.IsNotNull(empNoAff);
            Assert.IsTrue(empNoAff.Affiliation is NoAffiliation);
            NoAffiliation ua = empNoAff.Affiliation as NoAffiliation;
            Assert.IsNotNull(ua);

            Employee member = database.GetUnionMember(memberId);
            Assert.IsNull(member);

        }

    }
}
