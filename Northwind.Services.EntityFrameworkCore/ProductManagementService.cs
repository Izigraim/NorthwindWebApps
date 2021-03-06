﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Northwind.Services.Data;
using Northwind.Services.Products;

namespace Northwind.Services.EntityFrameworkCore
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
        public IList<Product> LookupProductsByName(IList<string> names)
        {
            throw new NotImplementedException();
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
