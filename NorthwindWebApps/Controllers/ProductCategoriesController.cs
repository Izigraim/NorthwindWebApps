using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
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
        public async Task<ActionResult<IEnumerable<ProductCategory>>> GetCategories(int offset = 0, int limit = 10)
        {
            return this.Ok(this.productManagementService.ShowCategories(offset, limit));
        }

        [HttpGet("{categoryId}")]
        public async Task<ActionResult<ProductCategory>> GetCategory(int categoryId)
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
        public async Task<ActionResult<ProductCategory>> CreateCategory(ProductCategory productCategory)
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
        public async Task<ActionResult> UpdateCategory(int id, ProductCategory productCategory)
        {
            if (id != productCategory.Id)
            {
                return this.BadRequest();
            }

            this.productManagementService.UpdateCategories(id, productCategory);

            return this.NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<ProductCategory>> DeleteCategory(int id)
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
    }
}
