using System;
using System.Collections.Generic;
using System.Text;
using Northwind.DataAccess.Employees;

namespace Northwind.Services.Employees
{
    public class Employee
    {
        public int Id { get; set; }

        public string LastName { get; set; }

        public string FirstName { get; set; }

        public string Title { get; set; }

        public string TitleOfCourtesy { get; set; }

        public DateTime? BirthDate { get; set; }

        public DateTime? HireDate { get; set; }

        public string Address { get; set; }

        public string City { get; set; }

        public string Region { get; set; }

        public string PostalCode { get; set; }

        public string Country { get; set; }

        public string HomePhone { get; set; }

        public string Extension { get; set; }

        public byte[] Photo { get; set; }

        public string Notes { get; set; }

        public int? ReportsTo { get; set; }

        public string PhotoPath { get; set; }

        public static explicit operator Employee(EmployeeTransferObject transferObject)
        {
            Employee employee = new Employee()
            {
                Id = transferObject.Id,
                LastName = transferObject.LastName,
                FirstName = transferObject.FirstName,
                Title = transferObject.Title,
                TitleOfCourtesy = transferObject.TitleOfCourtesy,
                BirthDate = transferObject.BirthDate,
                HireDate = transferObject.HireDate,
                Address = transferObject.Address,
                City = transferObject.City,
                Region = transferObject.Region,
                PostalCode = transferObject.PostalCode,
                Country = transferObject.Country,
                HomePhone = transferObject.HomePhone,
                Extension = transferObject.Extension,
                Photo = transferObject.Photo,
                Notes = transferObject.Notes,
                ReportsTo = transferObject.ReportsTo,
                PhotoPath = transferObject.PhotoPath,
            };

            return employee;
        }

        public static explicit operator EmployeeTransferObject(Employee employee)
        {
            EmployeeTransferObject employeeTransfer = new EmployeeTransferObject()
            {
                Id = employee.Id,
                LastName = employee.LastName,
                FirstName = employee.FirstName,
                Title = employee.Title,
                TitleOfCourtesy = employee.TitleOfCourtesy,
                BirthDate = employee.BirthDate,
                HireDate = employee.HireDate,
                Address = employee.Address,
                City = employee.City,
                Region = employee.Region,
                PostalCode = employee.PostalCode,
                Country = employee.Country,
                HomePhone = employee.HomePhone,
                Extension = employee.Extension,
                Photo = employee.Photo,
                Notes = employee.Notes,
                ReportsTo = employee.ReportsTo,
                PhotoPath = employee.PhotoPath,
            };

            return employeeTransfer;
        }
    }
}
