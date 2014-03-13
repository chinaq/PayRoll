using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PayRoll.BLL
{
    public class ChangeMemberTransaction : ChangeAffiliationTransaction
    {
        private readonly double dues;
        private readonly int memberId;



        public ChangeMemberTransaction(int empId, int memberId, double dues)
            : base(empId)
        {
            this.dues = dues;
            this.memberId = memberId;
        }



        protected override Affiliation Affiliation
        {
            get { return new UnionAffiliation(memberId, dues); }
        }


        protected override void RecordMembership(Employee emp)
        {
            PayrollDatabase.AddUnionMember(memberId, emp);
        }
    }
}
