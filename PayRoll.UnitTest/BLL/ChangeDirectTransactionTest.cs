﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PayRoll.BLL;
using NUnit.Framework;

namespace PayRoll.UnitTest.BLL
{
    [TestFixture]
    public class ChangeDirectTransactionTest : SetUpInmemoryDb
    {
        [Test]
        public void ExecuteTest()
        {
            int empId = 29;
            string bank = "MisBank";
            string account = "3329";

            AddEmployeeTransaction addSalEmp = new AddSalariedEmployee(empId, "kara", "samubola", 3000, database);
            addSalEmp.Execute();

            ChangeMethodTranscation changeDirectTrans = new ChangeDirectTransaction(empId, bank, account, database);
            changeDirectTrans.Execute();
            Employee emp = database.GetEmployee(empId);
            Assert.IsNotNull(emp);
            Assert.IsTrue(emp.Method is DirectMethod);

            DirectMethod directMethod = emp.Method as DirectMethod;
            Assert.IsNotNull(directMethod);
            Assert.AreEqual(directMethod.Bank, bank);
            Assert.AreEqual(directMethod.Account, account);
        
        }
    }
}
