using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace PayRoll.BLL
{
    public class PayDayTransaction
    {
        private ArrayList empIds;
        private DateTime payDay;
        private Hashtable payChecks = new Hashtable();

        public PayDayTransaction(DateTime payDay)
        {
            this.payDay = payDay;
        }



        private ArrayList GetEmployeeIds()
        {
            return PayrollDatabase.GetEmployeeIds();
        }

        public void Execute()
        {
            empIds = GetEmployeeIds();
            Employee emp;
            PayCheck payCheck;
            foreach(int empId in empIds)
            {
                emp = PayrollDatabase.GetEmployee(empId);
                if (emp.IsPayDate(payDay))
                {
                    payCheck = new PayCheck(payDay);
                    payChecks[empId] = payCheck;
                    emp.PayDay(payCheck);
                }
            }
        }

        public PayCheck GetPayCheck(int empId)
        {
            return payChecks[empId] as PayCheck;
        }
    }
}
