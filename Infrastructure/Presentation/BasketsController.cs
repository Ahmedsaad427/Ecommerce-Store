using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Services.Abstractions;
using Shared;

namespace Presentation
{
    [ApiController]
    [Route("api/[controller]")]
    public class BasketsController : ControllerBase
    {
        private readonly IServiceManager _serviceManager;

        public BasketsController(IServiceManager serviceManager)
        {
            _serviceManager = serviceManager;
        }

        [HttpGet] // GET: api/baskets?id={id}
        public async Task<IActionResult> GetBasketById([FromQuery] string id)
        {
            var result = await _serviceManager.BasketService.GetBasketAsync(id);
            if (result == null)
            {
                return NotFound("No baskets found");
            }

            return Ok(result);
        }

        [HttpPost] // POST: api/baskets
        public async Task<IActionResult> UpdateBasket([FromBody] BasketDto basketDto)
        {
            var result = await _serviceManager.BasketService.UpdateBasketAsync(basketDto);
            if (result == null)
            {
                return NotFound("Failed to update basket");
            }

            return Ok(result);
        }

        [HttpDelete("{id}")] // DELETE: api/baskets/{id}
        public async Task<IActionResult> DeleteBasket(string id)
        {
            var result = await _serviceManager.BasketService.DeleteBasketAsync(id);
            if (!result)
            {
                return NotFound("Basket not found");
            }

            return Ok(result);
        }
    }
}
