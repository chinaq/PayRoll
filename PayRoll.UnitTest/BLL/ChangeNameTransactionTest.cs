using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PayRoll.BLL;
using NUnit.Framework;

namespace PayRoll.UnitTest.BLL
{
    [TestFixture]
    public class ChangeNameTransactionTest
    {
        [Test]
        public void ExecuteTest()
        { 
            //目的：检查改名字
            int empId = 18;
            string newName = "Busu";

            //添加雇员
            AddHourlyEmployee addHourlyEmp = new AddHourlyEmployee(empId, "Mana", "duva", 12.3);
            addHourlyEmp.Execute();

            //改名字
            ChangeEmployeeTransaction changeNameTrans = new ChangeNameTransaction(empId, newName);
            changeNameTrans.Execute();

            //获取雇员
            Employee emp = PayrollDatabase.GetEmployee(empId);
            Assert.IsNotNull(emp);

            //检查名字
            Assert.AreEqual(emp.Name, newName);
        }
    }
}
