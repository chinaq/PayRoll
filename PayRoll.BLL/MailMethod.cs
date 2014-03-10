using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PayRoll.BLL
{
    public class MailMethod:PaymentMethod
    {
        private readonly string address;    

        public string Address
        {
            get { return address; }
        }



        public MailMethod(string address)
        {
            this.address = address;
        }

    }
}
