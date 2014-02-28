using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PayRoll.BLL
{
    public class AddCommissionedEmployee: AddEmployeeTransaction
    {
        private readonly double commissionRate;
        private readonly double baseRate;

        public AddCommissionedEmployee(int id, string name, string address, double baseRate, double commissionRate)
            : base(id, name, address)
        {
            this.baseRate = baseRate;
            this.commissionRate = commissionRate;
        }

        protected override PaymentClassification MakeClassification()
        {
            return new CommissionClassification(baseRate, commissionRate);
        }

        protected override PaymentSchedule MakeSchedule()
        {
            return new BiWeeklySchedule();
        }
    }
}
