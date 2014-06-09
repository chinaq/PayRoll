using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace PayRoll.BLL
{
    public class InMemoryPayrollDatabase: PayrollDatabase
    {

        private static Hashtable employees = new Hashtable();
        private static Hashtable unionMembers = new Hashtable();



        
        public Employee GetEmployee(int id)
        {
            return employees[id] as Employee;
        }




        public void DeleteEmployee(int id)
        {
            employees.Remove(id);
        }




        public void AddUnionMember(int memberId, Employee emp)
        {
            unionMembers[memberId] = emp;
        }





        public Employee GetUnionMember(int memberId)
        {
            return unionMembers[memberId] as Employee;
        }





        public void RemoveUnionMember(int memberId)
        {
            unionMembers.Remove(memberId);
        }
        


        public void AddEmployee(Employee employee)
        {
            employees[employee.Empid] = employee;
        }


        public ArrayList GetAllEmployeeIds()
        {
            return new ArrayList(employees.Keys);
        }
    }
}
