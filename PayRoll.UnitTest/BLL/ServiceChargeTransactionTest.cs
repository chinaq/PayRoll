using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PayRoll.BLL;
using NUnit.Framework;

namespace PayRoll.UnitTest.BLL
{
    [TestFixture]
    public class ServiceChargeTransactionTest:SetUpInmemoryDb 
    {
        [Test]
        public void ExecuteTest()
        {
            int empId = 15;
            int memberId = 86;
            double unionDues = 90.8;

            double chargeAmount = 12;
            DateTime chargeDate = new DateTime(2014,6,7);

            //增加并获取雇员
            AddCommissionedEmployee addComEmp = new AddCommissionedEmployee(empId, "Muma", "shanghai", 12.3, 1.56, database);
            addComEmp.Execute();
            Employee emp = database.GetEmployee(empId);
            Assert.IsNotNull(emp);
            
            //设置并保存公会会员
            Affiliation unionAffiliation = new UnionAffiliation(memberId, unionDues);
            emp.Affiliation = unionAffiliation;
            database.AddUnionMember(memberId, emp);

            //将会费信息添加到会员上
            ServiceChargeTransaction sct = new ServiceChargeTransaction(memberId, chargeAmount, chargeDate, database);
            sct.Execute();

            //检查会费
            UnionAffiliation ua = emp.Affiliation as UnionAffiliation;
            Assert.IsNotNull(ua);
            ServiceCharge sc = ua.GetServiceCharge(chargeDate);
            Assert.IsNotNull(sc);
            Assert.AreEqual(sc.Amount, chargeAmount, 0.001);
        }
    }
}
