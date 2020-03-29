using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Northwind.Services.Data;
using Northwind.Services.Employees;

namespace Northwind.Services.EntityFrameworkCore
{
    public class EmployeeManagementService : IEmployeeManagementService
    {
        private NorthwindContext context;

        public EmployeeManagementService(NorthwindContext context)
        {
            this.context = context;
        }

        public int CreateEmployee(Employee employee)
        {
            if (employee != null)
            {
                if (this.context.Employees.Find(employee.Id) != null)
                {
                    employee.Id = this.context.Employees.Max(c => c.Id) + 1;
                }

                this.context.Employees.Add(employee);
                this.context.SaveChanges();
                return employee.Id;
            }
            else
            {
                return -1;
            }
        }

        public bool DestroyEmployee(int employeeId)
        {
            var employee = this.context.Employees.Find(employeeId);

            if (employee != null)
            {
                this.context.Employees.Remove(employee);
                this.context.SaveChanges();
                return true;
            }
            else
            {
                return false;
            }
        }

        public IList<Employee> ShowEmployees(int offset, int limit)
        {
            return this.context.Employees.Where(c => c.Id >= offset).Take(limit).ToList();
        }

        public bool TryShowEmployee(int employeeId, out Employee employee)
        {
            employee = this.context.Employees.Find(employeeId);
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
            if (employee != null)
            {
                this.context.Employees.Update(employee);
                this.context.SaveChanges();
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
