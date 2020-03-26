using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using Northwind.Services.Products;
using Northwind.DataAccess;
using Northwind.DataAccess.Products;

namespace Northwind.Services.DataAccess
{
    public class ProductManagementDataAccessService : IProductManagementService
    {
        private SqlServerDataAccessFactory sqlServerDataAccess;

        public ProductManagementDataAccessService(SqlConnection sqlConnection)
        {
            this.sqlServerDataAccess = new SqlServerDataAccessFactory(sqlConnection);
        }

        public int CreateProduct(Product product)
        {
            return this.sqlServerDataAccess.GetProductDataAccessObject().InsertProduct((ProductTransferObject)product);
        }

        public bool DestroyProduct(int productId)
        {
            if (this.sqlServerDataAccess.GetProductDataAccessObject().DeleteProduct(productId))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public IList<Product> LookupProductsByName(IList<string> names)
        {
            throw new NotImplementedException();
        }

        public IList<Product> ShowProducts(int offset, int limit)
        {
            var transferProducts = this.sqlServerDataAccess.GetProductDataAccessObject().SelectProducts(offset, limit);

            List<Product> products = new List<Product>();

            foreach (var transferProduct in transferProducts)
            {
                products.Add((Product)transferProduct);
            }

            return products;
        }

        public IList<Product> ShowProductsForCategory(int categoryId)
        {
            throw new NotImplementedException();
        }

        public bool TryShowProduct(int productId, out Product product)
        {
            try
            {
                product = (Product)this.sqlServerDataAccess.GetProductDataAccessObject().FindProduct(productId);
            }
            catch (ProductNotFoundException)
            {
                product = null;
                return false;
            }

            if (product != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool UpdateProduct(int productId, Product product)
        {
            if (this.sqlServerDataAccess.GetProductDataAccessObject().UpdateProduct((ProductTransferObject)product))
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
