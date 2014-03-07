using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace PayRoll.BLL
{
    public class UnionAffiliation:Affiliation
    {
        //private readonly int memberId;
        //private readonly DateTime dues;
        private Hashtable charges = new Hashtable();



        //public int MemberId
        //{
        //    get { return memberId; }
        //}

        //public DateTime Dues
        //{
        //    get { return dues; }
        //} 


        public void AddServiceCharge(ServiceCharge sc)
        {
            charges[sc.Date] = sc;
        }

        public ServiceCharge GetServiceCharge(DateTime date)
        {
            return charges[date] as ServiceCharge;
        }

    }
}
