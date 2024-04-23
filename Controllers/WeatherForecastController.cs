
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace ProductManagementApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private static readonly List<Product> _productList = new List<Product>();

        // GET: api/Products
        [HttpGet]
        public IActionResult GetProducts()
        {
            return Ok(_productList);
        }

        [HttpPost]
        public IActionResult AddProduct([FromBody] Product product)
        {
            if (product == null)
            {
                return BadRequest("Invalid product data");
            }

            if (String.IsNullOrEmpty(product.Name))
            {
                return BadRequest("Product name is required");
            }

            product.Id = Guid.NewGuid();
            _productList.Add(product);

            return Ok(product); 
        }


        // PUT: api/Products/5
        [HttpPut("{id}")]
        public IActionResult UpdateProduct(Guid id, [FromBody] Product product)
        {
            if (product == null)
            {
                return BadRequest("Invalid product data");
            }

            if (String.IsNullOrEmpty(product.Name))
            {
                return BadRequest("Product name is required");
            }

            var existingProduct = _productList.FirstOrDefault(p => p.Id == id);

            if (existingProduct == null)
            {
                return NotFound();
            }

            existingProduct.Name = product.Name;
            existingProduct.Description = product.Description;
            existingProduct.Price = product.Price;

            return Ok();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteProduct(Guid id)
        {
            var product = _productList.FirstOrDefault(p => p.Id == id);

            if (product == null)
            {
                return NotFound(); 
            }

            _productList.Remove(product);
            return Ok(); 
        }
    }
}
