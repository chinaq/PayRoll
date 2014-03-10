using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using PayRoll.BLL;

namespace PayRoll.UnitTest.BLL
{
    [TestFixture]
    public class ChangeUnaffiliationTransactionTest
    {
        [Test]
        public void ExecuteTest()
        {
            int empId = 33;
            int memberId = 98;
            DateTime dues = new DateTime(2014, 5, 6);

            PayrollDatabase.DeleteEmployee(empId);
            PayrollDatabase.RemoveUnionMember(memberId);

            AddSalariedEmployee addSalEmp = new AddSalariedEmployee(empId, "Masa", "Faav Street", 5000);
            addSalEmp.Execute();
            ChangeAffiliationTransaction changeMemberTrans = new ChangeMemberTransaction(empId, memberId, dues);
            changeMemberTrans.Execute();
            Employee empAff = PayrollDatabase.GetEmployee(empId);
            Assert.IsNotNull(empAff);
            Assert.IsTrue(empAff.Affiliation is UnionAffiliation);


            ChangeAffiliationTransaction changeAffTrans = new ChangeUnaffiliatedTransaction(empId);
            changeAffTrans.Execute();
            Employee empNoAff = PayrollDatabase.GetEmployee(empId);
            Assert.IsNotNull(empNoAff);
            Assert.IsTrue(empNoAff.Affiliation is NoAffiliation);
            NoAffiliation ua = empNoAff.Affiliation as NoAffiliation;
            Assert.IsNotNull(ua);

            Employee member = PayrollDatabase.GetUnionMember(memberId);
            Assert.IsNull(member);

        }

    }
}
