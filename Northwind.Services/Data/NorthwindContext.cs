using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Northwind.Services.Employees;
using Northwind.Services.Products;

namespace Northwind.Services.Data
{
    public class NorthwindContext : DbContext
    {
        public DbSet<ProductCategory> ProductCategories { get; set; }

        public DbSet<Product> Products { get; set; }

        public DbSet<Employee> Employees { get; set; }

        public NorthwindContext(DbContextOptions<NorthwindContext> options)
            : base(options)
        {

        }
    }
}
