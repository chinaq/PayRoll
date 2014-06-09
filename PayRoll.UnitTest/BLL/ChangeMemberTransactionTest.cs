using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using PayRoll.BLL;

namespace PayRoll.UnitTest.BLL
{
    [TestFixture]
    public class ChangeMemberTransactionTest:SetUpInmemoryDb
    {
        [Test]
        public void ExecuteTest()
        {
            int empId = 33;
            int memberId = 987;
            double dues = 98.6;

            AddSalariedEmployee addSalEmp = new AddSalariedEmployee(empId, "Masa", "Faav Street", 5000, database);
            addSalEmp.Execute();
            ChangeAffiliationTransaction changeAffTrans = new ChangeMemberTransaction(empId, memberId, dues, database);
            changeAffTrans.Execute();

            Employee emp = database.GetEmployee(empId);
            Assert.IsNotNull(emp);
            Assert.IsTrue(emp.Affiliation is UnionAffiliation);

            UnionAffiliation ua = emp.Affiliation as UnionAffiliation;
            Assert.IsNotNull(ua);
            Assert.AreEqual(ua.Dues, dues);

            Employee member = database.GetUnionMember(memberId);
            Assert.IsNotNull(member);
            Assert.AreEqual(emp, member);
        }
    }
}
