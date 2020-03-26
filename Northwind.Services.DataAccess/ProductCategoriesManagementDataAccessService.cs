using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using Northwind.Services.Products;
using Northwind.DataAccess;
using Northwind.DataAccess.Products;

namespace Northwind.Services.DataAccess
{
    public class ProductCategoriesManagementDataAccessService : IProductCategoryManagementService
    {
        private SqlServerDataAccessFactory sqlServerDataAccess;

        public ProductCategoriesManagementDataAccessService(SqlConnection sqlConnection)
        {
            this.sqlServerDataAccess = new SqlServerDataAccessFactory(sqlConnection);
        }

        public int CreateCategory(ProductCategory productCategory)
        {
            return this.sqlServerDataAccess.GetProductCategoryDataAccessObject().InsertProductCategory((ProductCategoryTransferObject)productCategory);
        }

        public bool DestroyCategory(int categoryId)
        {
            if (this.sqlServerDataAccess.GetProductCategoryDataAccessObject().DeleteProductCategory(categoryId))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public IList<ProductCategory> LookupCategoriesByName(IList<string> names)
        {
            throw new NotImplementedException();
        }

        public IList<ProductCategory> ShowCategories(int offset, int limit)
        {
            var categoriesTransfer = this.sqlServerDataAccess.GetProductCategoryDataAccessObject().SelectProductCategories(offset, limit);

            List<ProductCategory> productCategories = new List<ProductCategory>();

            foreach(var categorieTransfer in categoriesTransfer)
            {
                productCategories.Add((ProductCategory)categorieTransfer);
            }

            return productCategories;
        }

        public bool TryShowCategory(int categoryId, out ProductCategory productCategory)
        {
            try
            {
                productCategory = (ProductCategory)this.sqlServerDataAccess.GetProductCategoryDataAccessObject().FindProductCategory(categoryId);
            }
            catch(ProductCategoryNotFoundException)
            {
                productCategory = null;
                return false;
            }

            if (productCategory != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool UpdateCategories(int categoryId, ProductCategory productCategory)
        {
            if (this.sqlServerDataAccess.GetProductCategoryDataAccessObject().UpdateProductCategory((ProductCategoryTransferObject)productCategory))
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
