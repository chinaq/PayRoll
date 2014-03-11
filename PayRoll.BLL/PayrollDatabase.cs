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
        private static Hashtable unionMembers = new Hashtable();

        public static Employee GetEmployee(int id)
        {
            return employees[id] as Employee;
        }

        public static void AddEmployee(int id, Employee emloyee)
        {
            employees[id] = emloyee;
        }

        public static void DeleteEmployee(int id)
        {
            employees.Remove(id);
        }



        public static void AddUnionMember(int memberId, Employee emp)
        {
            unionMembers[memberId] = emp;
        }


        public static Employee GetUnionMember(int memberId)
        {
            return unionMembers[memberId] as Employee;
        }

        public static void RemoveUnionMember(int memberId)
        {
            unionMembers.Remove(memberId);
        }

        public  static ArrayList GetEmployeeIds()
        {
            return new ArrayList(employees.Keys);
        }
    }
}
