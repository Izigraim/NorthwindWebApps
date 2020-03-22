using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Northwind.Services.Data;

namespace Northwind.Services.Products
{
    public class ProductCategoryPicturesService : IProductCategoryPicturesService
    {
        private NorthwindContext context;

        public ProductCategoryPicturesService(NorthwindContext context)
        {
            this.context = context;
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
    }
}
