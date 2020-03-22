using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Northwind.Services.Data;
using Northwind.Services.Products;

namespace Northwind.Services.EntityFrameworkCore
{
    public class ProductCategoryManagementService : IProductCategoryManagementService
    {
        private NorthwindContext context;

        public ProductCategoryManagementService(NorthwindContext context)
        {
            this.context = context;
        }

        /// <inheritdoc/>
        public int CreateCategory(ProductCategory productCategory)
        {
            if (productCategory != null)
            {
                var product = this.context.ProductCategories.Find(productCategory.Id);

                if (product != null)
                {
                    productCategory.Id = this.context.ProductCategories.Max(q => q.Id) + 1;
                }

                this.context.ProductCategories.Add(productCategory);
                this.context.SaveChanges();
                return productCategory.Id;
            }
            else
            {
                return -1;
            }
        }

        /// <inheritdoc/>
        public bool DestroyCategory(int categoryId)
        {
            var category = this.context.ProductCategories.Find(categoryId);

            if (category != null)
            {
                this.context.ProductCategories.Remove(category);
                this.context.SaveChanges();
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <inheritdoc/>
        public IList<ProductCategory> LookupCategoriesByName(IList<string> names)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc/>
        public IList<ProductCategory> ShowCategories(int offset, int limit)
        {
            return this.context.ProductCategories.Where(c => c.Id >= offset).Take(limit).ToList();
        }

        /// <inheritdoc/>
        public bool TryShowCategory(int categoryId, out ProductCategory productCategory)
        {
            productCategory = this.context.ProductCategories.Find(categoryId);
            if (productCategory != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <inheritdoc/>
        public bool UpdateCategories(int categoryId, ProductCategory productCategory)
        {
            if (productCategory != null)
            {
                this.context.Update(productCategory);
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
