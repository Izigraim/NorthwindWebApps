using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.IO;
using System.Linq;
using Northwind.Services.Data;

namespace Northwind.Services.Products
{
    /// <summary>
    /// Represents a stub for a product management service.
    /// </summary>
    public sealed class ProductManagementService : IProductManagementService
    {
        private NorthwindContext context = new NorthwindContext();

        /// <inheritdoc/>
        public int CreateCategory(ProductCategory productCategory)
        {
            if (productCategory != null)
            {
                this.context.ProductCategories.Add(productCategory);
                this.context.SaveChanges();
                return productCategory.Id;
            }
            else
            {
                return 0;
            }
        }

        /// <inheritdoc/>
        public int CreateProduct(Product product)
        {
            throw new NotImplementedException();
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
        public bool DestroyPicture(int categoryId)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc/>
        public bool DestroyProduct(int productId)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc/>
        public IList<ProductCategory> LookupCategoriesByName(IList<string> names)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc/>
        public IList<Product> LookupProductsByName(IList<string> names)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc/>
        public IList<ProductCategory> ShowCategories(int offset, int limit)
        {
            return this.context.ProductCategories.Where(c => c.Id >= offset).Take(limit).ToList();
        }

        /// <inheritdoc/>
        public IList<Product> ShowProducts(int offset, int limit)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc/>
        public IList<Product> ShowProductsForCategory(int categoryId)
        {
            throw new NotImplementedException();
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
        public bool TryShowPicture(int categoryId, out byte[] bytes)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc/>
        public bool TryShowProduct(int productId, out Product product)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc/>
        public bool UpdateCategories(int categoryId, ProductCategory productCategory)
        {
            var category = this.context.ProductCategories.Single(c => c.Id == categoryId);

            if (category != null)
            {
                category.Name = productCategory.Name;
                category.Description = productCategory.Description;
                this.context.SaveChanges();
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <inheritdoc/>
        public bool UpdatePicture(int categoryId, Stream stream)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc/>
        public bool UpdateProduct(int productId, Product product)
        {
            throw new NotImplementedException();
        }
    }
}
