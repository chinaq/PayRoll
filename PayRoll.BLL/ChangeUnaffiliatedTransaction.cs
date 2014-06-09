using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PayRoll.BLL
{
    public class ChangeUnaffiliatedTransaction:ChangeAffiliationTransaction
    {
        public ChangeUnaffiliatedTransaction(int empId, PayrollDatabase database)
            : base(empId, database)
        { }



        protected override Affiliation Affiliation
        {
            get { return new NoAffiliation(); }
        }

        protected override void RecordMembership(Employee emp)
        {
            UnionAffiliation ua = emp.Affiliation as UnionAffiliation;
            if (ua != null)
            {
                int memberId = ua.MemberId;
                database.RemoveUnionMember(memberId);
            }
        }
    }
}
