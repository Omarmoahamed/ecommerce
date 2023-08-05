using Ecommerce.BL.Services;
using Ecommerce.DAL.models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.EcommerceAPI.controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShoppingcartController : ControllerBase
    {
        public Ishoppingcart shoppingcartservice;

        public Ishoppingcartitemservice shoppingcartitemservice;

        public ShoppingcartController(Ishoppingcart shoppingcartservice, Ishoppingcartitemservice shoppingcartitemservice) 
        {
            this.shoppingcartitemservice= shoppingcartitemservice;

            this.shoppingcartservice= shoppingcartservice;

        }

        [HttpGet]
        [Route("get/shoppingcart")]

        public async Task<ActionResult<Shoppingcart>> getshoppingcart() 
        {
            var shoppingcart = await shoppingcartservice.GetShoppingcart();

            return Ok(shoppingcart);
        }
        
        
        [HttpPost("Add/cartitem/{id}")]
        

        public async Task<ActionResult> addshoppingcartitem(int id) 
        {
            await shoppingcartitemservice.addcartitem(id);

            return Ok("cart item is added successfully");
        }

        [HttpDelete("remove/cartitem/{id}")]

        public async Task<ActionResult> removecartitem(int id) 
        {
           await shoppingcartitemservice.removecartitem(id);

            return Ok("shopping cart item is removed successfully");

        }

        [HttpDelete]
        [Route("clearall/shoppingcart")]

        public async Task<ActionResult> clearallitems() 
        {
           await shoppingcartitemservice.clearall();

            return Ok("items is cleared successfully");
        }

      


    }
}
