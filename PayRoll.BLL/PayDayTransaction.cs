using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace PayRoll.BLL
{
    public class PayDayTransaction:Transaction
    {
        private ArrayList empIds;
        private DateTime payDay;
        private Hashtable payChecks = new Hashtable();

        public PayDayTransaction(DateTime payDay, PayrollDatabase database):base(database)
        {
            this.payDay = payDay;
        }



        private ArrayList GetEmployeeIds()
        {
            return database.GetAllEmployeeIds();
        }

        public override void Execute()
        {
            empIds = GetEmployeeIds();
            Employee emp;
            PayCheck payCheck;
            foreach(int empId in empIds)
            {
                emp = database.GetEmployee(empId);
                if (emp.IsPayDate(payDay))
                {
                    DateTime startDay = emp.GetStartDay(payDay);
                    payCheck = new PayCheck(startDay, payDay);
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
