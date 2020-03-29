using System;
using System.Collections.Generic;
using System.Text;

namespace Northwind.Services.Employees
{
    public interface IEmployeeManagementService
    {
        IList<Employee> ShowEmployees(int offset, int limit);

        bool TryShowEmployee(int employeeId, out Employee employee);

        int CreateEmployee(Employee employee);

        bool DestroyEmployee(int employeeId);

        bool UpdateEmployee(int employeeId, Employee employee);
    }
}
