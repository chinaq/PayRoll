using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PayRoll.BLL;
using NUnit.Framework;

namespace PayRoll.UnitTest.BLL
{   
    [TestFixture]
    public class ChangeAddressTransactionTest:SetUpInmemoryDb
    {
        [Test]
        public void Execute()
        {
            //目的：检查改地址
            int empId = 19;
            string newAddress = "Beijing";

            //添加雇员
            AddHourlyEmployee addHourlyEmp = new AddHourlyEmployee(empId, "Mana", "duva", 12.3, database);
            addHourlyEmp.Execute();

            //改名字
            ChangeEmployeeTransaction changeAddressTrans = new ChangeAddressTransaction(empId, newAddress, database);
            changeAddressTrans.Execute();

            //获取雇员
            Employee emp = database.GetEmployee(empId);
            Assert.IsNotNull(emp);

            //检查名字
            Assert.AreEqual(emp.Address, newAddress);
        
        }
    }
}
