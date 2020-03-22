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
        private NorthwindContext context;

        public ProductManagementService(NorthwindContext context)
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
        public int CreateProduct(Product product)
        {
            if (product != null)
            {
                var productTemp = this.context.Products.Find(product.Id);

                if (productTemp != null)
                {
                    product.Id = this.context.Products.Max(q => q.Id) + 1;
                }

                this.context.Products.Add(product);
                this.context.SaveChanges();
                return product.Id;
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
        public bool DestroyPicture(int categoryId)
        {
            var category = this.context.ProductCategories.Find(categoryId);

            if (category == null)
            {
                return false;
            }

            category.Picture = null;
            this.context.Update(category);
            this.context.SaveChanges();
            return true;
        }

        /// <inheritdoc/>
        public bool DestroyProduct(int productId)
        {
            var product = this.context.Products.Find(productId);

            if (product != null)
            {
                this.context.Products.Remove(product);
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
            return this.context.Products.Where(c => c.Id >= offset).Take(limit).ToList();
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
            var category = this.context.ProductCategories.Find(categoryId);

            if (category.Picture == null)
            {
                bytes = null;
                return false;
            }

            bytes = category.Picture;
            return true;
        }

        /// <inheritdoc/>
        public bool TryShowProduct(int productId, out Product product)
        {
            product = this.context.Products.Find(productId);

            if (product != null)
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

        /// <inheritdoc/>
        public bool UpdatePicture(int categoryId, Stream stream)
        {
            var category = this.context.ProductCategories.Find(categoryId);

            if (category == null)
            {
                return false;
            }

            using (MemoryStream memoryStream = new MemoryStream())
            {
                stream.Seek(0, SeekOrigin.Begin);
                stream.CopyTo(memoryStream);
                category.Picture = memoryStream.ToArray();
            }

            this.context.Update(category);
            this.context.SaveChanges();

            return true;
        }

        /// <inheritdoc/>
        public bool UpdateProduct(int productId, Product product)
        {
            if (product != null)
            {
                this.context.Update(product);
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
