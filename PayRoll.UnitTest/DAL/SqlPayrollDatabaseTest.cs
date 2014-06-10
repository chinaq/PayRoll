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


        [SetUp]
        public void SetUp()
        {
            database = new SqlPayrollDatabase();

            connection = new SqlConnection(
                @"Initial Catalog = PayRoll; Data Source = (local)\SQLSERVER2008; " +
                "user id = sa; password = sa1203as0321");
            connection.Open();
            SqlCommand command = new SqlCommand("delete from Employee", connection);
            command.ExecuteNonQuery();
        }


        [TearDown]
        public void TearDown()
        {
            connection.Close();
        }



        [Test]
        public void AddEmployee()
        {
            Employee employee = new Employee(123, "George", "123 Baker St.");
            employee.Schedule = new MonthlySchedule();
            employee.Method = new DirectMethod("Bank 1", "12389");
            employee.Classification = new SalariedClassifiction(1000.00);
            database.AddEmployee(employee);
                        
            SqlCommand command = new SqlCommand("select * from Employee", connection);
            SqlDataAdapter adapter = new SqlDataAdapter(command);
            DataSet dataset = new DataSet();
            adapter.Fill(dataset);
            DataTable table = dataset.Tables["table"];

            Assert.AreEqual(1, table.Rows.Count);
            DataRow row = table.Rows[0];
            Assert.AreEqual(123, row["EmpId"]);
            Assert.AreEqual("George", row["Name"]);
            Assert.AreEqual("123 Baker St.", row["Address"]);

        }


        [Test]
        public void ScheduleCodeTest()
        {
            Employee employee = new Employee(123, "George", "123 Baker St.");
            employee.Schedule = new MonthlySchedule();
            employee.Method = new DirectMethod("Bank 1", "12389");
            employee.Classification = new SalariedClassifiction(1000.00);
            database.AddEmployee(employee);

            SqlCommand command = new SqlCommand(
                "select * from Employee", connection);
            SqlDataAdapter adapter = new SqlDataAdapter(command);
            DataSet dataset = new DataSet();
            adapter.Fill(dataset);
            DataTable table = dataset.Tables["table"];

            Assert.AreEqual(1, table.Rows.Count);
            DataRow row = table.Rows[0];
            Assert.AreEqual("monthly", row["ScheduleType"]);
        }

    }
}
