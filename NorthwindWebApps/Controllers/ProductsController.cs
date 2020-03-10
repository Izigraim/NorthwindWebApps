using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Northwind.Services.Products;

namespace NorthwindWebApps.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private IProductManagementService productManagementService;

        public ProductsController(IProductManagementService productManagementService)
        {
            this.productManagementService = productManagementService;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Product>> GetProducts(int offset = 0, int limit = 10)
        {
            return this.Ok(this.productManagementService.ShowProducts(offset, limit));
        }

        [HttpGet("{id}")]
        public ActionResult<Product> GetProduct(int id)
        {
            if (this.productManagementService.TryShowProduct(id, out Product product))
            {
                return this.Ok(product);
            }
            else
            {
                return this.NotFound();
            }
        }

        [HttpPost]
        public ActionResult<Product> CreateProduct(Product product)
        {
            if (product == null)
            {
                return this.BadRequest();
            }
            else
            {
                this.productManagementService.CreateProduct(product);
                return this.Ok(product);
            }
        }

        [HttpPut("{id}")]
        public ActionResult UpdateProduct(int id, Product product)
        {
            if (id != product.Id)
            {
                return this.BadRequest();
            }
            else
            {
                this.productManagementService.UpdateProduct(id, product);
                return this.NoContent();
            }
        }

        [HttpDelete("{id}")]
        public ActionResult DeleteProduct(int id)
        {
            if (this.productManagementService.DestroyProduct(id))
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
