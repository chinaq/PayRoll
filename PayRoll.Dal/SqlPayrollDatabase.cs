using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PayRoll.BLL;
using System.Collections;
using System.Data.SqlClient;

namespace PayRoll.DAL
{
    public class SqlPayrollDatabase: PayrollDatabase
    {

        private readonly SqlConnection connection;

        public SqlPayrollDatabase()
        {
            connection = new SqlConnection(
                @"Initial Catalog = PayRoll; Data Source = (local)\SQLSERVER2008; " +
                "user id = sa; password = sa1203as0321");
            connection.Open();
        }




        public void AddEmployee(Employee employee)
        {
            string sql = "insert into Employee values(@EmpId, @Name, @Address, " + 
                "@ScheduleType, @PaymentMethodType, @PaymentClassificationType)";
            SqlCommand command = new SqlCommand(sql, connection);

            command.Parameters.AddWithValue("@EmpId", employee.EmpId);
            command.Parameters.AddWithValue("@Name", employee.Name);
            command.Parameters.AddWithValue("@Address", employee.Address);
            command.Parameters.AddWithValue("@ScheduleType", employee.Schedule.GetType().ToString());
            command.Parameters.AddWithValue("@PaymentMethodType", employee.Method.GetType().ToString());
            command.Parameters.AddWithValue("@PaymentClassificationType", employee.Classification.GetType().ToString());

            command.ExecuteNonQuery();
        }



        public Employee GetEmployee(int id)
        {
            throw new NotImplementedException();
        }

        public void DeleteEmployee(int id)
        {
            throw new NotImplementedException();
        }

        public void AddUnionMember(int unionMemberId, Employee employee)
        {
            throw new NotImplementedException();
        }

        public Employee GetUnionMember(int id)
        {
            throw new NotImplementedException();
        }

        public void RemoveUnionMember(int id)
        {
            throw new NotImplementedException();
        }

        public ArrayList GetAllEmployeeIds()
        {
            throw new NotImplementedException();
        }

    }
}
