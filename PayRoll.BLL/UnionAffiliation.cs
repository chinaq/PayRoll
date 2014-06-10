using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace PayRoll.BLL
{
    public class UnionAffiliation:Affiliation
    {
        private readonly double dues;
        private Hashtable charges = new Hashtable();
        private readonly int memberId;



        public double Dues
        {
            get { return dues; }
        } 
        
        public int MemberId
        {
            get { return memberId; }
        } 



        public UnionAffiliation(int memberId, double dues)
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


        public double CalculateDeduction(PayCheck payCheck)
        {
            int fridays = NumberOfFridays(payCheck.StartDay, payCheck.PayDay);
            double allDues = CalculateAllDues(fridays);
            double allServiceCharges = CalculateAllServiceCharges(payCheck.StartDay, payCheck.PayDay);

            return allDues + allServiceCharges;
        }

        private double CalculateAllServiceCharges(DateTime startDay, DateTime endDay)
        {
            double allServiceCharges = 0;
            foreach (ServiceCharge sc in charges.Values)
            { 
                if(DateUtil.IsInPayPeriod(startDay, endDay, sc.Date))
                {
                    allServiceCharges += sc.Amount;
                }
            }
            return allServiceCharges;
        }


        private double CalculateAllDues(int payTimes)
        {
            return dues * payTimes;
        }

        private int NumberOfFridays(DateTime startDay, DateTime endDay)
        {
            int fridays = 0;
            DateTime currentDay = startDay;
            while (currentDay <= endDay)
            {
                if (currentDay.DayOfWeek == DayOfWeek.Friday)
                    fridays++;
                currentDay = currentDay.AddDays(1);
            }            

            return fridays;
        }
    }
}
