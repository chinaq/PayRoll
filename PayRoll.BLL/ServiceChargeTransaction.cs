using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PayRoll.BLL
{
    public class ServiceChargeTransaction:Transaction
    {
        private readonly int memberId;
        private readonly double amount;
        private readonly DateTime date;

        public ServiceChargeTransaction(int memberId, double amount, DateTime date, PayrollDatabase database): base(database)
        {
            this.memberId = memberId;
            this.amount = amount;
            this.date = date;
        }




        #region Transaction 成员

        public override void Execute()
        {
            Employee emp = database.GetUnionMember(memberId);
            if (emp != null)
            {
                UnionAffiliation unionAffiliation = emp.Affiliation as UnionAffiliation;
                if (unionAffiliation != null)
                {
                    ServiceCharge sc = new ServiceCharge(amount, date);
                    unionAffiliation.AddServiceCharge(sc);
                }
                else
                {
                    throw new ApplicationException("not join Union Affiliaction.");
                }
            }
            else
            {
                throw new ApplicationException ("no such employee");
            }
        }


        #endregion
    }
}
