using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Services.Abstractions;

namespace Presentation
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController(IServiceManager serviceManager) : ControllerBase
    {
        // EndPoint to get all products: Public not static method
        [HttpGet] // Get : api/products
        public async Task<IActionResult> GetAllProducts()
        {
            var result = await serviceManager.productService.GetAllProductsAsync();

            // Check if no products are found
            if (result == null || !result.Any())
            {
                return NotFound("No products found"); // 404 Not Found
            }
            else
            {
                return Ok(result); // 200 OK
            }
        }

        // EndPoint to get a product by ID: Public not static method

        [HttpGet("{id}")] // Get : api/products/{id}
        public async Task<IActionResult> GetProductById(int id)
        {
            var result = await serviceManager.productService.GetProductByIdAsync(id);
            // Check if product is found
            if (result == null)
            {
                return NotFound($"Product with ID {id} not found"); // 404 Not Found
            }
            else
            {
                return Ok(result); // 200 OK
            }
        }

        // EndPoint to Get Brands: Public not static method

        [HttpGet("brands")] // Get : api/products/brands
        public async Task<IActionResult> GetBrands()
        {
            var result = await serviceManager.productService.GetAllBrandsAsync();
            // Check if no brands are found
            if (result == null || !result.Any())
            {
                return NotFound("No brands found"); // 404 Not Found
            }
            else
            {
                return Ok(result); // 200 OK
            }
        }


        // EndPoint to Get Types: Public not static method

        [HttpGet("types")] // Get : api/products/types
        public async Task<IActionResult> GetTypes()
        {
            var result = await serviceManager.productService.GetAllTypesAsync();
            // Check if no types are found
            if (result == null || !result.Any())
            {
                return NotFound("No types found"); // 404 Not Found
            }
            else
            {
                return Ok(result); // 200 OK
            }
        }
    }
}
