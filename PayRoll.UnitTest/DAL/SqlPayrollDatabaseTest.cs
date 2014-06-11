using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using PayRoll.DAL;
using PayRoll.BLL;
using System.Data.SqlClient;
using System.Data;

namespace PayRoll.UnitTest.DAL
{
    [TestFixture]
    [Category("need_sqlserver")]
    public class SqlPayrollDatabaseTest
    {
        private SqlPayrollDatabase database;
        private SqlConnection connection;
        private Employee employee;


        [SetUp]
        public void SetUp()
        {
            database = new SqlPayrollDatabase();

            connection = new SqlConnection(
                @"Initial Catalog = PayRoll; Data Source = (local)\SQLSERVER2008; " +
                "user id = sa; password = sa1203as0321");
            connection.Open();


            ClearDbTables();
            employee = new Employee(123, "George", "123 Baker St.");
            employee.Schedule = new MonthlySchedule();
            employee.Method = new DirectMethod("Bank 1", "12389");
            employee.Classification = new SalariedClassifiction(1000.00);

        }

        private void ClearDbTables()
        {
            SqlCommand command = new SqlCommand("delete from DirectDepositAccount", connection);
            command.ExecuteNonQuery();

            command = new SqlCommand("delete from PayCheckAddress", connection);
            command.ExecuteNonQuery();

            command = new SqlCommand("delete from Employee", connection);
            command.ExecuteNonQuery();
        }


        [TearDown]
        public void TearDown()
        {
            connection.Close();
        }



        [Test]
        public void AddEmployeeTest_CheckBasicInfo_InEmployeeTable()
        {

            database.AddEmployee(employee);
            DataTable table = LoadTable("Employee");

            Assert.AreEqual(1, table.Rows.Count);
            DataRow row = table.Rows[0];
            Assert.AreEqual(123, row["EmpId"]);
            Assert.AreEqual("George", row["Name"]);
            Assert.AreEqual("123 Baker St.", row["Address"]);

        }



        [Test]
        public void AddEmployeeTest_CheckPaymentSchedule_InEmployeeTable()
        {
            CheckSavedScheduleCode(new MonthlySchedule(), "monthly");
            ClearDbTables();
            CheckSavedScheduleCode(new WeeklySchedule(), "weekly");
            ClearDbTables();
            CheckSavedScheduleCode(new BiWeeklySchedule(), "biweekly");
            ClearDbTables();
        }




        [Test]
        public void AddEmployeeTest_CheckPaymentMethod_InEmployeeTable()
        {
            CheckSavedPaymentMethodCode(new HoldMethod(), "hold");
            ClearDbTables();
            CheckSavedPaymentMethodCode(new DirectMethod("Bank -1", "0987654321"), "directdeposit");
            ClearDbTables();
            CheckSavedPaymentMethodCode(new MailMethod("111 Maple Ct."), "mail");

        }



        [Test]
        public void AddEmployeeTest_CheckDirectMethod_InDirectDepositAccountTable()
        {
            CheckSavedPaymentMethodCode(new DirectMethod("Bank -1", "0987654321"), "directdeposit");
                        
            DataTable table = LoadTable("DirectDepositAccount");

            Assert.AreEqual(1, table.Rows.Count);
            DataRow row = table.Rows[0];
            Assert.AreEqual("Bank -1", row["Bank"]);
            Assert.AreEqual("0987654321", row["Account"]);
            Assert.AreEqual(123, row["EmpId"]);
        }




        [Test]
        public void AddEmployeeTest_CheckMailMethod_InPayCheckAddressTable()
        {
            CheckSavedPaymentMethodCode(new MailMethod("111 Maple Ct."), "mail");

            DataTable table = LoadTable("PaycheckAddress");

            Assert.AreEqual(1, table.Rows.Count);
            DataRow row = table.Rows[0];
            Assert.AreEqual("111 Maple Ct.", row["Address"]);
            Assert.AreEqual(123, row["EmpId"]);
        }








        private void CheckSavedPaymentMethodCode(PaymentMethod method, string expected)
        {
            employee.Method = method;
            database.AddEmployee(employee);

            DataTable table = LoadTable("Employee");
            Assert.AreEqual(1, table.Rows.Count);

            DataRow row = table.Rows[0];
            Assert.AreEqual(expected, row["PaymentMethiodType"]);
        }








        private void CheckSavedScheduleCode(PaymentSchedule schedule, string expected)
        {
            employee.Schedule = schedule;
            database.AddEmployee(employee);

            DataTable table = LoadTable("Employee");
            Assert.AreEqual(1, table.Rows.Count);

            DataRow row = table.Rows[0];
            Assert.AreEqual(expected, row["PaymentScheduleType"]);
        }



        private DataTable LoadTable(String tableName)
        {
            SqlCommand command = new SqlCommand(
                "select * from "+tableName, connection);
            SqlDataAdapter adapter = new SqlDataAdapter(command);
            DataSet dataset = new DataSet();
            adapter.Fill(dataset);
            DataTable table = dataset.Tables["table"];
            return table;
        }

    }
}
