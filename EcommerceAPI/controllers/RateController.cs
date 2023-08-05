using Ecommerce.BL.Services;
using Ecommerce.DAL.models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.EcommerceAPI.controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RateController : ControllerBase
    {
        public Irateservice RateService;

        public RateController(Irateservice rateService) 
        {
            this.RateService = rateService;
        }


        [HttpPost("add/rate/{rating}/{productid}")]
        

        public async Task<ActionResult> addrate(int rating,int productid) 
        {
            await RateService.AddRate(rating, productid);
            return Ok("rating is added successfully");

        }

        [HttpPut("update/rate/{rating}/{productid}")]
        public async Task<ActionResult> updateresult(int rating,int productid) 
        {
            await RateService.UpdateRate(rating, productid);
            return Ok("rate is updated successfully");
        }

        [HttpDelete("delete/rate/{id}")]

        public async Task<ActionResult> deleterate(int id) 
        {
            await RateService.DeleteRate(id);

            return Ok("rate deleted successfully");
        }


        [HttpGet("get/userRate/{productid}")]
        public async Task<ActionResult<Rate>> getuserRate(int productid) 
        {
            var rate = await RateService.GetUserRate(productid);

            return Ok(rate);
        }
    }
}
