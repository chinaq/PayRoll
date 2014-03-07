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

        public ChangeCommissionedTransaction(int empId, double baseRate, double commissionRate)
            : base(empId)
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
