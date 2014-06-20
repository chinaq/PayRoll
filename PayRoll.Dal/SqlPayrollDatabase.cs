﻿using System;
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
        private SqlCommand insertEmployeeCommand;


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
            PrepareToSaveEmployee(employee);

            SqlTransaction transaction = connection.BeginTransaction("save Emlpoyee");
            try
            {
                ExcuteCommand(insertEmployeeCommand, transaction);
                ExcuteCommand(insertPaymentMethodCommand, transaction);
                transaction.Commit();
            }
            catch (Exception e)
            {
                transaction.Rollback();
                throw e;
            }
        }

        private void ExcuteCommand(SqlCommand command, SqlTransaction transaction)
        {
            if (command != null)
            {
                command.Connection = connection;
                command.Transaction = transaction;
                command.ExecuteNonQuery();
            }
        }

        private void PrepareToSaveEmployee(Employee employee)
        {

            string sql = "insert into Employee values(@EmpId, @Name, @Address, " +
                "@ScheduleType, @PaymentMethodType, @PaymentClassificationType)";
            insertEmployeeCommand = new SqlCommand(sql, connection);

            insertEmployeeCommand.Parameters.AddWithValue("@EmpId", employee.EmpId);
            insertEmployeeCommand.Parameters.AddWithValue("@Name", employee.Name);
            insertEmployeeCommand.Parameters.AddWithValue("@Address", employee.Address);
            insertEmployeeCommand.Parameters.AddWithValue("@ScheduleType", ScheduleCode(employee.Schedule));

            //SavePaymentMethod(employee);
            insertEmployeeCommand.Parameters.AddWithValue("@PaymentMethodType", methodCode);
            insertEmployeeCommand.Parameters.AddWithValue("@PaymentClassificationType", employee.Classification.GetType().ToString());
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
                CraateInsertDirectDespositCommand(employee, directMethod);
            }
            else if (method is MailMethod)
            {
                methodCode = "mail";
                MailMethod mailMethod = method as MailMethod;
                CreateInsertMailMethodCommand(employee, mailMethod);
            }
            else
                methodCode = "unknown";
        }

        private void CreateInsertMailMethodCommand(Employee employee, MailMethod mailMethod)
        {
            string sql = "insert into PayCheckAddress " +
                "values (@Address, @EmpId)";
            insertPaymentMethodCommand = new SqlCommand(sql);
            insertPaymentMethodCommand.Parameters.AddWithValue("@Address", mailMethod.Address);
            insertPaymentMethodCommand.Parameters.AddWithValue("@EmpId", employee.EmpId);

        }

        private void CraateInsertDirectDespositCommand(Employee employee, DirectMethod directMethod)
        {
            string sql = "insert into DirectDepositAccount " +
                "values (@Bank, @Account, @EmpId)";
            insertPaymentMethodCommand = new SqlCommand(sql);
            insertPaymentMethodCommand.Parameters.AddWithValue("@Bank", directMethod.Bank);
            insertPaymentMethodCommand.Parameters.AddWithValue("@Account", directMethod.Account);
            insertPaymentMethodCommand.Parameters.AddWithValue("@EmpId", employee.EmpId);

        }

    }
}
