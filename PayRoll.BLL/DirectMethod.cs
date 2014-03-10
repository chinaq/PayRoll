using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PayRoll.BLL
{
    public class DirectMethod:PaymentMethod
    {
        private readonly string bank;
        private readonly string account;



        public string Bank
        {
            get { return bank; }
        }

        public string Account
        {
            get { return account; }
        }



        public DirectMethod(string bank, string account)
        {
            this.bank = bank;
            this.account = account;
        }

    }
}
