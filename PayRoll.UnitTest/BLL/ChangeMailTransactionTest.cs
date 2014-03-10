using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PayRoll.BLL;
using NUnit.Framework;

namespace PayRoll.UnitTest.BLL
{
    [TestFixture]
    public class ChangeMailTransactionTest
    {
        [Test]
        public void ExecuteTest()
        {
            int empId = 28;
            string mailAddress = "MisStreet";

            AddEmployeeTransaction addSalEmp = new AddSalariedEmployee(empId, "kara", "samubola", 3000);
            addSalEmp.Execute();

            ChangeMethodTranscation changeMailTrans = new ChangeMailTransaction(empId, mailAddress );
            changeMailTrans.Execute();
            Employee emp = PayrollDatabase.GetEmployee(empId);
            Assert.IsNotNull(emp);
            Assert.IsTrue(emp.Method  is MailMethod);

            MailMethod mailMethod = emp.Method as MailMethod;
            Assert.IsNotNull(mailMethod);
            Assert.AreEqual(mailMethod.Address, mailAddress);
        }
    }
}
