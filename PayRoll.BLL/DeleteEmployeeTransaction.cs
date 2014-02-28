using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PayRoll.BLL
{
    public class DeleteEmployeeTransaction:Transaction 
    {
        private readonly int id;

        public DeleteEmployeeTransaction(int id)
        {
            this.id = id;
        }

        public void Execute()
        {
            PayrollDatabase.DeleteEmployee(id);
        }
    }
}
