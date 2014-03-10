using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PayRoll.BLL
{
    public class ChangeMailTransaction:ChangeMethodTranscation
    {
        private string address;



        public ChangeMailTransaction(int empId, string address):base(empId)
        {
            this.address = address;
        }

        

        protected override PaymentMethod Method
        {
            get {return new MailMethod(address);}
        }
    }
}
