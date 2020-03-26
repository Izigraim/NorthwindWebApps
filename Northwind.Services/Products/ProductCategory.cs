using Northwind.DataAccess.Products;

namespace Northwind.Services.Products
{
    /// <summary>
    /// Represents a product category.
    /// </summary>
    public class ProductCategory
    {
        /// <summary>
        /// Gets or sets a product category identifier.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets a product category name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets a product category description.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets a product category picture.
        /// </summary>
        public byte[] Picture { get; set; }

        public static explicit operator ProductCategory(ProductCategoryTransferObject transferObject)
        {
            ProductCategory productCategory = new ProductCategory
            {
                Id = transferObject.Id,
                Name = transferObject.Name,
                Description = transferObject.Description,
                Picture = transferObject.Picture,
            };

            return productCategory;
        }

        public static explicit operator ProductCategoryTransferObject(ProductCategory productCategory)
        {
            ProductCategoryTransferObject transferObject = new ProductCategoryTransferObject
            {
                Id = productCategory.Id,
                Name = productCategory.Name,
                Description = productCategory.Description,
                Picture = productCategory.Picture,
            };

            return transferObject;
        }
    }
}
