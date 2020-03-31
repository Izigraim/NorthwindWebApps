using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using Northwind.DataAccess;
using Northwind.Services.Employees;
using Northwind.DataAccess.Employees;

namespace Northwind.Services.DataAccess
{
    public class EmployeeManagementDataAccessService : IEmployeeManagementService
    {
        private SqlServerDataAccessFactory sqlServerDataAccess;

        public EmployeeManagementDataAccessService(SqlConnection sqlConnection)
        {
            this.sqlServerDataAccess = new SqlServerDataAccessFactory(sqlConnection);
        }

        public int CreateEmployee(Employee employee)
        {
            return this.sqlServerDataAccess.GetEmployeeDataAccessObject().InsertEmployee((EmployeeTransferObject)employee);
        }

        public bool DestroyEmployee(int employeeId)
        {
            if (this.sqlServerDataAccess.GetEmployeeDataAccessObject().DeleteEmployee(employeeId))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public IList<Employee> ShowEmployees(int offset, int limit)
        {
            var employeesTransfer = this.sqlServerDataAccess.GetEmployeeDataAccessObject().SelectEmployees(offset, limit);

            List<Employee> employees = new List<Employee>();

            foreach (var employeeTransfer in employeesTransfer)
            {
                employees.Add((Employee)employeeTransfer);
            }

            return employees;
        }

        public bool TryShowEmployee(int employeeId, out Employee employee)
        {
            try
            {
                employee = (Employee)this.sqlServerDataAccess.GetEmployeeDataAccessObject().FindEmployee(employeeId);
            }
            catch (ArgumentException)
            {
                employee = null;
                return false;
            }

            if (employee != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool UpdateEmployee(int employeeId, Employee employee)
        {
            if (this.sqlServerDataAccess.GetEmployeeDataAccessObject().UpdateEmployee((EmployeeTransferObject)employee))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
