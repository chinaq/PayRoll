using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace PayRoll.BLL
{
    public class PayrollDatabase
    {
        private static Hashtable employees = new Hashtable();

        public static Employee GetEmployee(int id)
        {
            return employees[id] as Employee;
        }

        public static void AddEmployee(int id, Employee emloyee)
        {
            employees[id] = emloyee;
        }
    }
}
