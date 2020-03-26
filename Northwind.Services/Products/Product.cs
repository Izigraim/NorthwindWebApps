using System.Collections.Generic;
using Northwind.DataAccess.Products;

namespace Northwind.Services.Products
{
    /// <summary>
    /// Represents a product.
    /// </summary>
    public class Product
    {
        /// <summary>
        /// Gets or sets a product identifier.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets a product name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets a supplier identifier.
        /// </summary>
        public int? SupplierId { get; set; }

        /// <summary>
        /// Gets or sets a category identifier.
        /// </summary>
        public int? CategoryId { get; set; }

        /// <summary>
        /// Gets or sets a quantity per unit.
        /// </summary>
        public string QuantityPerUnit { get; set; }

        /// <summary>
        /// Gets or sets a unit price.
        /// </summary>
        public decimal? UnitPrice { get; set; }

        /// <summary>
        /// Gets or sets an amount of units in stock.
        /// </summary>
        public short? UnitsInStock { get; set; }

        /// <summary>
        /// Gets or sets an amount of units on order.
        /// </summary>
        public short? UnitsOnOrder { get; set; }

        /// <summary>
        /// Gets or sets a reorder level.
        /// </summary>
        public short? ReorderLevel { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether a product is discontinued.
        /// </summary>
        public bool Discontinued { get; set; }

        public static explicit operator Product(ProductTransferObject transferProduct)
        {
            Product product = new Product()
            {
                Id = transferProduct.Id,
                Name = transferProduct.Name,
                SupplierId = transferProduct.SupplierId,
                CategoryId = transferProduct.CategoryId,
                QuantityPerUnit = transferProduct.QuantityPerUnit,
                UnitPrice = transferProduct.UnitPrice,
                UnitsInStock = transferProduct.UnitsInStock,
                UnitsOnOrder = transferProduct.UnitsOnOrder,
                ReorderLevel = transferProduct.ReorderLevel,
                Discontinued = transferProduct.Discontinued,
            };

            return product;
        }

        public static explicit operator ProductTransferObject(Product product)
        {
            ProductTransferObject transferProduct = new ProductTransferObject()
            {
                Id = product.Id,
                Name = product.Name,
                SupplierId = product.SupplierId,
                CategoryId = product.CategoryId,
                QuantityPerUnit = product.QuantityPerUnit,
                UnitPrice = product.UnitPrice,
                UnitsInStock = product.UnitsInStock,
                UnitsOnOrder = product.UnitsOnOrder,
                ReorderLevel = product.ReorderLevel,
                Discontinued = product.Discontinued,
            };

            return transferProduct;
        }
    }
}
