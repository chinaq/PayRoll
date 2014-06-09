using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PayRoll.BLL
{
    public abstract class ChangeEmployeeTransaction: Transaction
    {
        private readonly int empId;



        public ChangeEmployeeTransaction(int empId, PayrollDatabase database):base(database)
        {
            this.empId = empId;
        }




        protected abstract void Change(Employee emp);


        #region Transaction 成员

        public override void Execute()
        {
            Employee emp = database.GetEmployee(empId);
            if (emp != null)
            {
                Change(emp);
            }
            else
            {
                throw new ApplicationException("no such employee");
            }
        }

        #endregion
    }
}
