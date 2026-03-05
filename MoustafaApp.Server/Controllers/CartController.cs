using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MoustafaApp.Server.Dtos.CartDtos;
using MoustafaApp.Server.Service.CartService.CartService;
using System.Security.Claims;

namespace MoustafaApp.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class CartController : ControllerBase
    {
        private readonly ICartService _cartService;

        public CartController(ICartService cartService)
        {
            _cartService = cartService;
        }


        [HttpGet]
        public async Task<IActionResult> GetCart()
        {
            
            var cart = await _cartService.GetCart();

            return Ok(cart);
        }



        [HttpPost]
        public async Task<IActionResult> CreateCart()
        {
            var cart = await _cartService.CreateCart();

            return Ok(cart);
        }



        [HttpPost("items")]
        public async Task<IActionResult> AddItem([FromBody] AddItemDto request)
        {

            var cart = await _cartService.AddItemToCart(request.ProductId, request.Quantity);

            if (cart == null)
                return NotFound();

            return Ok(cart);
        }




        [HttpPut("items")]
        public async Task<IActionResult> UpdateQuantity([FromBody] UpdateQuantityItemDto dto)
        {

            var cart = await _cartService.UpdateQuantity(dto.ProductId, dto.Quantity);

            if (cart == null)
                return NotFound();

            return Ok(cart);
        }




        [HttpDelete("items/{productId}")]
        public async Task<IActionResult> RemoveItem(int productId)
        {

            var cart = await _cartService.RemoveItem(productId);

            if (cart == null)
                return NotFound();

            return Ok(cart);
        }




        [HttpDelete]
        public async Task<IActionResult> DeleteCart()
        {

            var result = await _cartService.DeleteCart();

            if (!result)
                return NotFound();

            return NoContent();
        }




        [HttpPost("coupons/{couponId}")]
        public async Task<IActionResult> ApplyCoupon(int couponId)
        {

            var cart = await _cartService.ApplyCoupon( couponId);

            if (cart == null)
                return NotFound();

            return Ok(cart);
        }





        [HttpDelete("coupons")]
        public async Task<IActionResult> RemoveCoupon()
        {

            var cart = await _cartService.RemoveCoupon();

            if (cart == null)
                return NotFound();

            return Ok(cart);
        }
    }
}