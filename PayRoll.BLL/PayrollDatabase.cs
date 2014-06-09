using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace PayRoll.BLL
{
    public interface PayrollDatabase
    {
        void AddEmployee(Employee employee);
        Employee GetEmployee(int id);
        void DeleteEmployee(int id);
        void AddUnionMember(int unionMemberId, Employee employee);
        Employee GetUnionMember(int id);
        void RemoveUnionMember(int id);
        ArrayList GetAllEmployeeIds();
    }
}
