using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PayRoll.BLL;
using System.Collections;
using System.Data.SqlClient;

namespace PayRoll.DAL
{
    public class SqlPayrollDatabase : PayrollDatabase
    {

        private readonly SqlConnection connection;
        private string methodCode;
        private SqlCommand insertPaymentMethodCommand;



        public SqlPayrollDatabase()
        {
            connection = new SqlConnection(
                @"Initial Catalog = PayRoll; Data Source = (local)\SQLSERVER2008; " +
                "user id = sa; password = sa1203as0321");
            connection.Open();
        }




        public void AddEmployee(Employee employee)
        {
            PrepareToSavePaymentMethod(employee);


            string sql = "insert into Employee values(@EmpId, @Name, @Address, " +
                "@ScheduleType, @PaymentMethodType, @PaymentClassificationType)";
            SqlCommand command = new SqlCommand(sql, connection);

            command.Parameters.AddWithValue("@EmpId", employee.EmpId);
            command.Parameters.AddWithValue("@Name", employee.Name);
            command.Parameters.AddWithValue("@Address", employee.Address);
            command.Parameters.AddWithValue("@ScheduleType", ScheduleCode(employee.Schedule));

            //SavePaymentMethod(employee);
            command.Parameters.AddWithValue("@PaymentMethodType", methodCode);
            command.Parameters.AddWithValue("@PaymentClassificationType", employee.Classification.GetType().ToString());

            command.ExecuteNonQuery();

            if (insertPaymentMethodCommand != null)
                insertPaymentMethodCommand.ExecuteNonQuery();
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



        private String ScheduleCode(PaymentSchedule schedule)
        {
            if (schedule is MonthlySchedule)
                return "monthly";
            else if (schedule is WeeklySchedule)
                return "weekly";
            else if (schedule is BiWeeklySchedule)
                return "biweekly";
            else
                return "unknown";
        }



        private void PrepareToSavePaymentMethod(Employee employee)
        {
            PaymentMethod method = employee.Method;
            if (method is HoldMethod)
                methodCode = "hold";
            else if (method is DirectMethod)
            {
                methodCode = "directdeposit";
                DirectMethod directMethod = method as DirectMethod;
                string sql = "insert into DirectDepositAccount " +
                    "values (@Bank, @Account, @EmpId)";
                insertPaymentMethodCommand = new SqlCommand(sql, connection);
                insertPaymentMethodCommand.Parameters.AddWithValue("@Bank", directMethod.Bank);
                insertPaymentMethodCommand.Parameters.AddWithValue("@Account", directMethod.Account);
                insertPaymentMethodCommand.Parameters.AddWithValue("@EmpId", employee.EmpId);
            }
            else if (method is MailMethod)
            {
                methodCode = "mail";
                MailMethod mailMethod = method as MailMethod;
                string sql = "insert into PayCheckAddress " +
                    "values (@Address, @EmpId)";
                insertPaymentMethodCommand = new SqlCommand(sql, connection);
                insertPaymentMethodCommand.Parameters.AddWithValue("@Address", mailMethod.Address);
                insertPaymentMethodCommand.Parameters.AddWithValue("@EmpId", employee.EmpId);
            }
            else
                methodCode = "unknown";
        }

    }
}
