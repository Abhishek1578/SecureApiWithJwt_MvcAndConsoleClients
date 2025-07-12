using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SBIMFJWT.DATA;
using SBIMFJWT.Models;

namespace SBIMFJWT.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles ="Admin")]
    public class ProductController : ControllerBase
    {

        private readonly ApplicationDbContext db;
        public ProductController(ApplicationDbContext db)
        {
            this.db = db;
        }


        [HttpGet]
        public IActionResult GetAllProduct()
        {
            var products = db.Products.ToList();
            return Ok(products);
        }

        [HttpGet("/{id}")]
        public IActionResult GetProductById(int id)
        {
            var products = db.Products.Find(id);
            if (products == null)
            {
                return NotFound("Product ID is not found");
            }
            return Ok(products);
        }

        [HttpPost]
        public IActionResult AddProduct(Product product)
        {
            db.Products.Add(product);
            db.SaveChanges();
            return Ok("Product Added Sucessfully");
        }

        [HttpPut]
        public IActionResult UpdateProduct(Product product)
        {
            var products = db.Products.Find(product.Id);
            if (products == null)
            {
                return NotFound("Product Id is not found");
            }
            products.Name = product.Name;
            products.Description = product.Description;
            product.Price = product.Price;
            db.SaveChanges();
            return Ok("Product Updated Successfully");
        }

        [HttpDelete("/{id}")]
        public IActionResult DeleteProduct(int id)
        {
            var product = db.Products.Find(id);
            if (product == null)
            {
                return NotFound("Product Id is not found");
            }
            db.Products.Remove(product);
            db.SaveChanges();
            return Ok("Product Deleted Successfully");
        }
    }
}
