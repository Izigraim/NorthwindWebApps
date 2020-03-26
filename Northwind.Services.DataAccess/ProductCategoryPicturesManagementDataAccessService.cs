using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using Northwind.Services.Products;
using Northwind.DataAccess;
using Northwind.DataAccess.Products;
using System.IO;

namespace Northwind.Services.DataAccess
{
    public class ProductCategoryPicturesManagementDataAccessService : IProductCategoryPicturesService
    {
        private SqlServerDataAccessFactory sqlServerDataAccess;

        public ProductCategoryPicturesManagementDataAccessService(SqlConnection sqlConnection)
        {
            this.sqlServerDataAccess = new SqlServerDataAccessFactory(sqlConnection);
        }

        public bool DestroyPicture(int categoryId)
        {
            ProductCategoryTransferObject transferObject;

            try
            {
                transferObject = this.sqlServerDataAccess.GetProductCategoryDataAccessObject().FindProductCategory(categoryId);
            }
            catch (ProductCategoryNotFoundException)
            {
                return false;
            }

            transferObject.Picture = null;
            if (this.sqlServerDataAccess.GetProductCategoryDataAccessObject().UpdateProductCategory(transferObject))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool TryShowPicture(int categoryId, out byte[] bytes)
        {
            ProductCategoryTransferObject transferObject;

            try
            {
                transferObject = this.sqlServerDataAccess.GetProductCategoryDataAccessObject().FindProductCategory(categoryId);
            }
            catch (ProductCategoryNotFoundException)
            {
                bytes = null;
                return false;
            }

            bytes = transferObject.Picture;
            return true;
        }

        public bool UpdatePicture(int categoryId, Stream stream)
        {
            ProductCategoryTransferObject transferObject;

            try
            {
                transferObject = this.sqlServerDataAccess.GetProductCategoryDataAccessObject().FindProductCategory(categoryId);
            }
            catch (ProductCategoryNotFoundException)
            {
                return false;
            }

            using (MemoryStream memoryStream = new MemoryStream())
            {
                stream.Seek(0, SeekOrigin.Begin);
                stream.CopyTo(memoryStream);
                transferObject.Picture = memoryStream.ToArray();
            }

            if (this.sqlServerDataAccess.GetProductCategoryDataAccessObject().UpdateProductCategory(transferObject))
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
