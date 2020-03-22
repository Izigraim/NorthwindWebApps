using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Northwind.Services.Data;
using Northwind.Services.Products;

namespace NorthwindWebApps.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductCategoriesController : ControllerBase
    {
        private readonly IProductManagementService productManagementService;

        public ProductCategoriesController(IProductManagementService productManagementService)
        {
            this.productManagementService = productManagementService;
        }

        [HttpGet]
        public ActionResult<IEnumerable<ProductCategory>> GetCategories(int offset = 0, int limit = 10)
        {
            return this.Ok(this.productManagementService.ShowCategories(offset, limit));
        }

        [HttpGet("{categoryId}")]
        public ActionResult<ProductCategory> GetCategory(int categoryId)
        {
            if (this.productManagementService.TryShowCategory(categoryId, out ProductCategory productCategory))
            {
                return this.Ok(productCategory);
            }
            else
            {
                return this.NotFound();
            }
        }

        [HttpPost]
        public ActionResult<ProductCategory> CreateCategory(ProductCategory productCategory)
        {
            if (productCategory == null)
            {
                return this.BadRequest();
            }
            else
            {
                this.productManagementService.CreateCategory(productCategory);
                return this.Ok(productCategory);
            }
        }

        [HttpPut("{id}")]
        public ActionResult UpdateCategory(int id, ProductCategory productCategory)
        {
            if (id != productCategory.Id)
            {
                return this.BadRequest();
            }

            this.productManagementService.UpdateCategories(id, productCategory);

            return this.NoContent();
        }

        [HttpDelete("{id}")]
        public ActionResult DeleteCategory(int id)
        {
            if (this.productManagementService.DestroyCategory(id))
            {
                return this.NoContent();
            }
            else
            {
                return this.NotFound();
            }
        }

        [HttpPut("{id}/picture")]
        public ActionResult PutPicture(int id)
        {
            var files = Request.Form.Files;

            foreach (var file in files)
            {
                if (file != null && file.Length > 0)
                {
                    using var stream = new MemoryStream();
                    file.CopyTo(stream);
                    if (!this.productManagementService.UpdatePicture(id, stream))
                    {
                        return this.NotFound();
                    }
                }
                else
                {
                    return this.BadRequest();
                }
            }

            return this.NoContent();
        }

        [HttpGet("{id}/picture")]
        public ActionResult<byte[]> GetPicture(int id)
        {
            if (this.productManagementService.TryShowPicture(id, out byte[] bytes))
            {
                return this.Ok(bytes);
            }

            return this.NotFound();
        }

        [HttpDelete("{id}/picture")]
        public ActionResult<byte[]> DeletePicture(int id)
        {
            if (this.productManagementService.DestroyPicture(id))
            {
                return this.NoContent();
            }

            return this.NotFound();
        }
    }
}
