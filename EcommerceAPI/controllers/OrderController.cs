using Ecommerce.BL.Services;
using Ecommerce.DAL.models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.EcommerceAPI.controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        public IOrderservice orderservice;

        public OrderController(Orderservice orderservice) 
        {
            this.orderservice = orderservice;
        }

        [HttpPost]
        [Route("add")]

        public async Task<ActionResult> addorder([FromBody]Shoppingcart shoppingcart) 
        {
            await orderservice.AddOrder(shoppingcart);
            return Ok("order added successfully");
        }

        [HttpGet]
        [Route("get")]

        public async Task<ActionResult<IList<Order>>> getorders() 
        {
            var orders = await orderservice.Getorders();

            return Ok(orders);
        }
    }
}
