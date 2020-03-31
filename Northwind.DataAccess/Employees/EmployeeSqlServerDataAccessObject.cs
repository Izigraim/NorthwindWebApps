// ReSharper disable CheckNamespace

using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;

namespace Northwind.DataAccess.Employees
{
    /// <summary>
    /// Represents a SQL Server-tailored DAO for Northwind products.
    /// </summary>
    public sealed class EmployeeSqlServerDataAccessObject : IEmployeeDataAccessObject
    {
        private readonly SqlConnection connection;

        public EmployeeSqlServerDataAccessObject(SqlConnection connection)
        {
            this.connection = connection ?? throw new ArgumentNullException(nameof(connection));
        }

        public bool DeleteEmployee(int employeeId)
        {
            if (employeeId <= 0)
            {
                throw new ArgumentException("Must be greater than zero.", nameof(employeeId));
            }

            const string commandText =
@"DELETE FROM dbo.Employees WHERE EmployeeID = @employeeId
SELECT @@ROWCOUNT";

            using (var command = new SqlCommand(commandText, this.connection))
            {
                const string emplId = "@employeeId";
                command.Parameters.Add(emplId, SqlDbType.Int);
                command.Parameters[emplId].Value = employeeId;

                var result = command.ExecuteScalar();
                return ((int)result) > 0;
            }
        }

        public EmployeeTransferObject FindEmployee(int employeeId)
        {
            if (employeeId <= 0)
            {
                throw new ArgumentException("Must be greater than zero.", nameof(employeeId));
            }

            const string commandText =
@"SELECT c.EmployeeID, c.LastName, c.FirstName, c.Title, c.TitleOfCourtesy, c.BirthDate, c.HireDate, c.Address, c.City, c.Region, c.PostalCode, c.Country, c.HomePhone, c.Extension, c.Photo, c.Notes, c.ReportsTo, c.PhotoPath FROM dbo.Employees as c
WHERE c.EmployeeID = @employeeId";

            using (var command = new SqlCommand(commandText, this.connection))
            {
                const string emplId = "@employeeId";
                command.Parameters.Add(emplId, SqlDbType.Int);
                command.Parameters[emplId].Value = employeeId;

                using (var reader = command.ExecuteReader())
                {
                    if (!reader.Read())
                    {
                        throw new ArgumentException(nameof(employeeId));
                    }

                    return CreateEmployee(reader);
                }
            }
        }

        public int InsertEmployee(EmployeeTransferObject employeeTransferObject)
        {
            if (employeeTransferObject == null)
            {
                throw new ArgumentNullException(nameof(employeeTransferObject));
            }

            const string commandText =
@"INSERT INTO dbo.Employees (LastName, FirstName, Title, TitleOfCourtesy, BirthDate, HireDate, Address, City, Region, PostalCode, Country, HomePhone, Extension, Photo, Notes, ReportsTo, PhotoPath) OUTPUT Inserted.EmployeeID
VALUES (@lastName, @firstName, @title, @titleOfCourtesy, @birthDate, @hireDate, @address, @city, @region, @postalCode, @country, @homePhone, @extension, @photo, @notes, @reportsTo, @photoPath)";

            using (var command = new SqlCommand(commandText, this.connection))
            {
                AddSqlParameters(employeeTransferObject, command);

                var id = command.ExecuteScalar();
                return (int)id;
            }
        }

        public IList<EmployeeTransferObject> SelectEmployees(int offset, int limit)
        {
            if (offset < 0)
            {
                throw new ArgumentException("Must be greater than zero or equals zero.", nameof(offset));
            }

            if (limit < 1)
            {
                throw new ArgumentException("Must be greater than zero.", nameof(limit));
            }

            const string commandTemplate =
@"SELECT c.EmployeeID, c.LastName, c.FirstName, c.Title, c.TitleOfCourtesy, c.BirthDate, c.HireDate, c.Address, c.City, c.Region, c.PostalCode, c.Country, c.HomePhone, c.Extension, c.Photo, c.Notes, c.ReportsTo, c.PhotoPath FROM dbo.Employees as c
ORDER BY c.EmployeeID
OFFSET {0} ROWS
FETCH FIRST {1} ROWS ONLY";

            string commandText = string.Format(CultureInfo.CurrentCulture, commandTemplate, offset, limit);
            return this.ExecuteReader(commandText);
        }

        public bool UpdateEmployee(EmployeeTransferObject employeeTransferObject)
        {
            if (employeeTransferObject == null)
            {
                throw new ArgumentNullException(nameof(employeeTransferObject));
            }

            const string commandText =
@"UPDATE dbo.Employees SET LastName = @lastName, FirstName = @firstName, Title = @title, TitleOfCourtesy = @titleOfCourtesy, BirthDate = @birthDate, HireDate = @hireDate, Address = @address, City = @city, Region = @region, PostalCode = @postalCode, Country = @country, HomePhone = @homePhone, Extension = @extension, Photo = @photo, Notes = @notes, ReportsTo = @reportsTo, PhotoPath = @photoPath
WHERE EmployeeID = @employeeId
SELECT @@ROWCOUNT";

            using (var command = new SqlCommand(commandText, this.connection))
            {
                AddSqlParameters(employeeTransferObject, command);

                const string employeeId = "@employeeId";
                command.Parameters.Add(employeeId, SqlDbType.Int);
                command.Parameters[employeeId].Value = employeeTransferObject.Id;

                var result = command.ExecuteScalar();
                return ((int)result) > 0;
            }
        }

        private static void AddSqlParameters(EmployeeTransferObject employeeTransferObject, SqlCommand command)
        {
            const string lastNameParameter = "@lastName";
            command.Parameters.Add(lastNameParameter, SqlDbType.NVarChar, 20);
            command.Parameters[lastNameParameter].Value = employeeTransferObject.LastName;

            const string firstNameParameter = "@firstname";
            command.Parameters.Add(firstNameParameter, SqlDbType.NVarChar, 10);
            command.Parameters[firstNameParameter].Value = employeeTransferObject.FirstName;

            const string titleParameter = "@title";
            command.Parameters.Add(titleParameter, SqlDbType.NVarChar, 30);
            command.Parameters[titleParameter].IsNullable = true;

            if (employeeTransferObject.Title != null)
            {
                command.Parameters[titleParameter].Value = employeeTransferObject.Title;
            }
            else
            {
                command.Parameters[titleParameter].Value = DBNull.Value;
            }

            const string titleOfCourtesyParameter = "@titleOfCourtesy";
            command.Parameters.Add(titleOfCourtesyParameter, SqlDbType.NVarChar, 25);
            command.Parameters[titleOfCourtesyParameter].IsNullable = true;

            if (employeeTransferObject.TitleOfCourtesy != null)
            {
                command.Parameters[titleOfCourtesyParameter].Value = employeeTransferObject.TitleOfCourtesy;
            }
            else
            {
                command.Parameters[titleOfCourtesyParameter].Value = DBNull.Value;
            }

            const string birthDateParameter = "@birthDate";
            command.Parameters.Add(birthDateParameter, SqlDbType.DateTime);
            command.Parameters[birthDateParameter].IsNullable = true;

            if (employeeTransferObject.BirthDate != null)
            {
                command.Parameters[birthDateParameter].Value = employeeTransferObject.BirthDate;
            }
            else
            {
                command.Parameters[birthDateParameter].Value = DBNull.Value;
            }

            const string hireDateParameter = "@hireDate";
            command.Parameters.Add(hireDateParameter, SqlDbType.DateTime);
            command.Parameters[hireDateParameter].IsNullable = true;

            if (employeeTransferObject.HireDate != null)
            {
                command.Parameters[hireDateParameter].Value = employeeTransferObject.HireDate;
            }
            else
            {
                command.Parameters[hireDateParameter].Value = DBNull.Value;
            }

            const string addressParameter = "@address";
            command.Parameters.Add(addressParameter, SqlDbType.NVarChar, 60);
            command.Parameters[addressParameter].IsNullable = true;

            if (employeeTransferObject.Address != null)
            {
                command.Parameters[addressParameter].Value = employeeTransferObject.Address;
            }
            else
            {
                command.Parameters[addressParameter].Value = DBNull.Value;
            }

            const string cityParameter = "@city";
            command.Parameters.Add(cityParameter, SqlDbType.NVarChar, 15);
            command.Parameters[cityParameter].IsNullable = true;

            if (employeeTransferObject.City != null)
            {
                command.Parameters[cityParameter].Value = employeeTransferObject.City;
            }
            else
            {
                command.Parameters[cityParameter].Value = DBNull.Value;
            }

            const string regionParameter = "@region";
            command.Parameters.Add(regionParameter, SqlDbType.NVarChar, 15);
            command.Parameters[regionParameter].IsNullable = true;

            if (employeeTransferObject.Region != null)
            {
                command.Parameters[regionParameter].Value = employeeTransferObject.Region;
            }
            else
            {
                command.Parameters[regionParameter].Value = DBNull.Value;
            }

            const string postalCodeParameter = "@postalCode";
            command.Parameters.Add(postalCodeParameter, SqlDbType.NVarChar, 10);
            command.Parameters[postalCodeParameter].IsNullable = true;

            if (employeeTransferObject.PostalCode != null)
            {
                command.Parameters[postalCodeParameter].Value = employeeTransferObject.PostalCode;
            }
            else
            {
                command.Parameters[postalCodeParameter].Value = DBNull.Value;
            }

            const string countryParameter = "@country";
            command.Parameters.Add(countryParameter, SqlDbType.NVarChar, 15);
            command.Parameters[countryParameter].IsNullable = true;

            if (employeeTransferObject.Country != null)
            {
                command.Parameters[countryParameter].Value = employeeTransferObject.Country;
            }
            else
            {
                command.Parameters[countryParameter].Value = DBNull.Value;
            }

            const string homePhoneParameter = "@homePhone";
            command.Parameters.Add(homePhoneParameter, SqlDbType.NVarChar, 24);
            command.Parameters[homePhoneParameter].IsNullable = true;

            if (employeeTransferObject.HomePhone != null)
            {
                command.Parameters[homePhoneParameter].Value = employeeTransferObject.HomePhone;
            }
            else
            {
                command.Parameters[homePhoneParameter].Value = DBNull.Value;
            }

            const string extensionParameter = "@extension";
            command.Parameters.Add(extensionParameter, SqlDbType.NVarChar, 4);
            command.Parameters[extensionParameter].IsNullable = true;

            if (employeeTransferObject.Extension != null)
            {
                command.Parameters[extensionParameter].Value = employeeTransferObject.Extension;
            }
            else
            {
                command.Parameters[extensionParameter].Value = DBNull.Value;
            }

            const string photoParameter = "@photo";
            command.Parameters.Add(photoParameter, SqlDbType.Image);
            command.Parameters[photoParameter].IsNullable = true;

            if (employeeTransferObject.Photo != null)
            {
                command.Parameters[photoParameter].Value = employeeTransferObject.Photo;
            }
            else
            {
                command.Parameters[photoParameter].Value = DBNull.Value;
            }

            const string notesParameter = "@notes";
            command.Parameters.Add(notesParameter, SqlDbType.NText);
            command.Parameters[notesParameter].IsNullable = true;

            if (employeeTransferObject.Notes != null)
            {
                command.Parameters[notesParameter].Value = employeeTransferObject.Notes;
            }
            else
            {
                command.Parameters[notesParameter].Value = DBNull.Value;
            }

            const string reportsToParameter = "@reportsTo";
            command.Parameters.Add(reportsToParameter, SqlDbType.Int);
            command.Parameters[reportsToParameter].IsNullable = true;

            if (employeeTransferObject.ReportsTo != null)
            {
                command.Parameters[reportsToParameter].Value = employeeTransferObject.ReportsTo;
            }
            else
            {
                command.Parameters[reportsToParameter].Value = DBNull.Value;
            }

            const string photoPathParameter = "@photoPath";
            command.Parameters.Add(photoPathParameter, SqlDbType.NVarChar, 255);
            command.Parameters[photoPathParameter].IsNullable = true;

            if (employeeTransferObject.ReportsTo != null)
            {
                command.Parameters[photoPathParameter].Value = employeeTransferObject.ReportsTo;
            }
            else
            {
                command.Parameters[photoPathParameter].Value = DBNull.Value;
            }
        }

        private static EmployeeTransferObject CreateEmployee(SqlDataReader reader)
        {
            var id = (int)reader["EmployeeID"];

            const string lastNameColumnName = "LastName";
            string lastName = null;
            if (reader[lastNameColumnName] != DBNull.Value)
            {
                lastName = (string)reader[lastNameColumnName];
            }

            const string firstNameColumnName = "FirstName";
            string firstName = null;
            if (reader[firstNameColumnName] != DBNull.Value)
            {
                firstName = (string)reader[firstNameColumnName];
            }

            const string titleColumnName = "Title";
            string title = null;
            if (reader[titleColumnName] != DBNull.Value)
            {
                title = (string)reader[titleColumnName];
            }

            const string titleOfCourtesyColumnName = "TitleOfCourtesy";
            string titleOfCourtesy = null;
            if (reader[titleOfCourtesyColumnName] != DBNull.Value)
            {
                titleOfCourtesy = (string)reader[titleOfCourtesyColumnName];
            }

            const string birthDateColumnName = "BirthDate";
            DateTime? birthDate = null;
            if (reader[birthDateColumnName] != DBNull.Value)
            {
                birthDate = (DateTime)reader[birthDateColumnName];
            }

            const string hireDateColumnName = "HireDate";
            DateTime? hireDate = null;
            if (reader[hireDateColumnName] != DBNull.Value)
            {
                hireDate = (DateTime)reader[hireDateColumnName];
            }

            const string addressColumnName = "Address";
            string address = null;
            if (reader[addressColumnName] != DBNull.Value)
            {
                address = (string)reader[addressColumnName];
            }

            const string cityColumnName = "City";
            string city = null;
            if (reader[cityColumnName] != DBNull.Value)
            {
                city = (string)reader[cityColumnName];
            }

            const string regionColumnName = "Region";
            string region = null;
            if (reader[regionColumnName] != DBNull.Value)
            {
                region = (string)reader[regionColumnName];
            }

            const string postalCodeColumnName = "PostalCode";
            string postalCode = null;
            if (reader[postalCodeColumnName] != DBNull.Value)
            {
                postalCode = (string)reader[postalCodeColumnName];
            }

            const string countryColumnName = "Country";
            string country = null;
            if (reader[countryColumnName] != DBNull.Value)
            {
                country = (string)reader[countryColumnName];
            }

            const string homePhoneColumnName = "HomePhone";
            string homePhone = null;
            if (reader[homePhoneColumnName] != DBNull.Value)
            {
                homePhone = (string)reader[homePhoneColumnName];
            }

            const string extensionColumnName = "Extension";
            string extension = null;
            if (reader[extensionColumnName] != DBNull.Value)
            {
                extension = (string)reader[extensionColumnName];
            }

            const string notesColumnName = "Notes";
            string notes = null;

            if (reader[notesColumnName] != DBNull.Value)
            {
                notes = (string)reader["Notes"];
            }

            const string photoColumnName = "Photo";
            byte[] photo = null;

            if (reader[photoColumnName] != DBNull.Value)
            {
                photo = (byte[])reader["Photo"];
            }

            const string reportsToColumnName = "ReportsTo";
            int? reportsTo;

            if (reader[reportsToColumnName] != DBNull.Value)
            {
                reportsTo = (int)reader[reportsToColumnName];
            }
            else
            {
                reportsTo = null;
            }

            const string photoPathColumnName = "PhotoPath";
            string photoPath = null;
            if (reader[photoPathColumnName] != DBNull.Value)
            {
                photoPath = (string)reader[photoPathColumnName];
            }

            return new EmployeeTransferObject
            {
                Id = id,
                LastName = lastName,
                FirstName = firstName,
                Title = title,
                TitleOfCourtesy = titleOfCourtesy,
                BirthDate = birthDate,
                HireDate = hireDate,
                Address = address,
                City = city,
                Region = region,
                PostalCode = postalCode,
                Country = country,
                HomePhone = homePhone,
                Extension = extension,
                Photo = photo,
                Notes = notes,
                ReportsTo = reportsTo,
                PhotoPath = photoPath,

            };
        }

        private IList<EmployeeTransferObject> ExecuteReader(string commandText)
        {
            var employee = new List<EmployeeTransferObject>();

#pragma warning disable CA2100 // Review SQL queries for security vulnerabilities
            using (var command = new SqlCommand(commandText, this.connection))
#pragma warning restore CA2100 // Review SQL queries for security vulnerabilities
            using (var reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    employee.Add(CreateEmployee(reader));
                }
            }

            return employee;
        }
    }
}
