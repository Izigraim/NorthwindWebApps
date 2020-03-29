using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Northwind.Services.Employees;

namespace NorthwindWebApps.Controllers
{
    [ApiController]
    [ApiConventionType(typeof(DefaultApiConventions))]
    [Route("api/[controller]")]
    public class EmployeesController : ControllerBase
    {
        private readonly IEmployeeManagementService employeeManagementService;

        public EmployeesController(IEmployeeManagementService employeeManagementService)
        {
            this.employeeManagementService = employeeManagementService;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Employee>> GetEmployees(int offset = 0, int limit = 10)
        {
            return this.Ok(this.employeeManagementService.ShowEmployees(offset, limit));
        }

        [HttpGet("{id}")]
        public ActionResult<Employee> GetEmployee(int id)
        {
            if (this.employeeManagementService.TryShowEmployee(id, out Employee employee))
            {
                return this.Ok(employee);
            }
            else
            {
                return this.NotFound();
            }
        }

        [HttpPost]
        public ActionResult<Employee> CreateEmployee(Employee employee)
        {
            if (employee == null)
            {
                return this.BadRequest();
            }
            else
            {
                this.employeeManagementService.CreateEmployee(employee);
                return this.Ok(employee);
            }
        }

        [HttpDelete("{id}")]
        public ActionResult DeleteEmployee(int id)
        {
            if (this.employeeManagementService.DestroyEmployee(id))
            {
                return this.NoContent();
            }
            else
            {
                return this.NotFound();
            }
        }

        [HttpPut("{id}")]
        public ActionResult UpdateEmployee(int id, Employee employee)
        {
            if (id != employee.Id)
            {
                return this.BadRequest();
            }
            else
            {
                this.employeeManagementService.UpdateEmployee(id, employee);
                return this.NoContent();
            }
        }
    }
}
