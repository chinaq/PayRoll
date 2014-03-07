﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PayRoll.BLL;
using NUnit.Framework;

namespace PayRoll.UnitTest.BLL
{
    [TestFixture]
    public class ServiceChargeTransactionTest
    {
        [Test]
        public void ExecuteTest()
        {
            int empId = 15;
            int memberId = 86;
            double chargeAmount = 12;
            DateTime chargeDate = new DateTime(2014,6,7);

            //增加并获取雇员
            AddCommissionedEmployee addComEmp = new AddCommissionedEmployee(empId, "Muma", "shanghai", 12.3, 1.56);
            addComEmp.Execute();
            Employee emp = PayrollDatabase.GetEmployee(empId);
            Assert.IsNotNull(emp);
            
            //设置并保存公会会员
            Affiliation unionAffiliation = new UnionAffiliation();
            emp.Affiliation = unionAffiliation;
            PayrollDatabase.AddUnionMember(memberId, emp);

            //将会费信息添加到会员上
            ServiceChargeTransaction sct = new ServiceChargeTransaction(memberId, chargeAmount, chargeDate);
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
