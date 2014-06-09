using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PayRoll.BLL
{
    public class ChangeCommissionedTransaction: ChangeClassificationTranscation
    {
        private double baseRate;
        private double commissionRate;

        public ChangeCommissionedTransaction(int empId, double baseRate, double commissionRate, PayrollDatabase database)
            : base(empId, database)
        {
            this.baseRate = baseRate;
            this.commissionRate = commissionRate;
        }



        protected override PaymentClassification GetClassification()
        {
            return new CommissionClassification(baseRate, commissionRate);
        }

        protected override PaymentSchedule GetSchedule()
        {
            return new BiWeeklySchedule();
        }
    }
}
