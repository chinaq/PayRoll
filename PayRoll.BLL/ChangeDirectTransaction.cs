using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PayRoll.BLL
{
    public class ChangeDirectTransaction:ChangeMethodTranscation
    {
        private readonly string bank;
        private readonly string account;



        public ChangeDirectTransaction(int empId, string bank, string account, PayrollDatabase database)
            : base(empId, database)
        {
            this.bank = bank;
            this.account = account;
        }


        protected override PaymentMethod Method
        {
            get { return new DirectMethod(bank, account); }
        }
    }
}
