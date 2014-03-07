using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PayRoll.BLL
{
    public class SalesReceiptTransaction : Transaction
    {
        private readonly DateTime date;
        private readonly double amount;
        private readonly int empId;


        public SalesReceiptTransaction(DateTime date, double amount, int empId)
        {
            this.date = date;
            this.amount = amount;
            this.empId = empId;
        }



        #region Transaction 成员

        public void Execute()
        {
            Employee emp = PayrollDatabase.GetEmployee(empId);
            if (emp != null)
            {
                CommissionClassification cc = emp.Classification as CommissionClassification;
                if (cc != null)
                {
                    SalesReceipt receipt = new SalesReceipt(date, amount);
                    cc.AddSalesReceipt(receipt);
                }
                else
                {
                    throw new ApplicationException("Tried to add sales receipt to" +
                            "non-commissioned employee");
                }
            }
            else
            {
                throw new ApplicationException("No such employee.");
            }
        }

        #endregion
    }
}
