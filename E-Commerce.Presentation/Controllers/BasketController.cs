using E_Commerce.Service.Abstraction.Interfaces;
using E_Commerce.Shared.DTOs.BasketDTOs;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Presentation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BasketController : ControllerBase
    {
        private readonly IBasketService _basketService;

        public BasketController(IBasketService basketService)
        {
            _basketService = basketService;
        }

        [HttpGet("{id:guid}")]
        public async Task<ActionResult<BasketDTO>> GetBasketById([FromRoute] Guid id)
        {
            var basket = await _basketService.GetBasketAsync(id);
            if (basket == null)
            {
                return NotFound();
            }
            return Ok(basket);
        }

        [HttpPost]
        public async Task<ActionResult<BasketDTO>> CreateOrUpdateBasket([FromBody] BasketDTO basket)
        {
            if (ModelState.IsValid)
            {
                var updatedBasket = await _basketService.CreateOrUpdateBasketAsync(basket);
                return Ok(updatedBasket);
            }
            else
            {
                return BadRequest(ModelState);
            }
        }
        [HttpDelete("{id:guid}")]
        public async Task<ActionResult> DeleteBasket([FromRoute] Guid id)
        {
            var result = await _basketService.DeleteBasketAsync(id);
            if (!result)
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}
