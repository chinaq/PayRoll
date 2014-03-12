using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace PayRoll.BLL
{
    public class UnionAffiliation:Affiliation
    {
        private readonly DateTime dues;
        private Hashtable charges = new Hashtable();
        private readonly int memberId;



        public DateTime Dues
        {
            get { return dues; }
        } 
        
        public int MemberId
        {
            get { return memberId; }
        } 



        public UnionAffiliation(int memberId, DateTime dues)
        {
            this.memberId = memberId;
            this.dues = dues;
        }
                


        public void AddServiceCharge(ServiceCharge sc)
        {
            charges[sc.Date] = sc;
        }

        public ServiceCharge GetServiceCharge(DateTime date)
        {
            return charges[date] as ServiceCharge;
        }


        #region Affiliation 成员

        public double CalulateDeduction(PayCheck payCheck)
        {
            return 0;
        }

        #endregion
    }
}
