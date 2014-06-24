using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PayRoll.BLL;
using System.Collections;
using System.Data.SqlClient;
using PayRoll.Dal;

namespace PayRoll.DAL
{
    public class SqlPayrollDatabase : PayrollDatabase
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

            SaveEmployeeOperation saveEmployeeOperation = new SaveEmployeeOperation(connection, employee);
            saveEmployeeOperation.Excute();
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
