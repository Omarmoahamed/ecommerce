using Ecommerce.BL.Services;
using Ecommerce.DAL.models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.EcommerceAPI.controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DiscountController : ControllerBase
    {
        public Idiscountservice discountservice;

        public Iapplydiscountservice applydiscountservice;

        public DiscountController(Idiscountservice discountservice, Iapplydiscountservice applydiscountservice)
        {
            this.discountservice = discountservice;
            this.applydiscountservice = applydiscountservice;
        }

        [HttpPost]
        [Route("addDiscount")]
        public async Task<ActionResult> addDiscount([FromBody]discount discount) 
        {
           await discountservice.Add_discount(discount);
            return Ok("discount is added");
        }

        [HttpPost]
        [Route("applydiscount/total")]

        public async Task<ActionResult> applydiscount([FromBody]discount discount) 
        {
          await  discountservice.appliedDiscountTotalCost(discount);

            return Ok(" order total discount is applied");

            
        }

        [HttpPost]
        [Route("addDiscount/product")]

        public async Task<ActionResult> addproductdiscount(discountapplied_product discountapplied_Product) 
        {
            await applydiscountservice.Addproductdiscount(discountapplied_Product);
            return Ok("product discount is added");
        }

        [HttpPost]
        [Route("applydiscount/product")]

        public async Task<ActionResult> applyproduct(int id) 
        {
            await applydiscountservice.applyproductdiscount(id);
            return Ok("discount is applied to product");
        }

        [HttpGet]
        [Route("getdiscounts")]

        public async Task<ActionResult<IList<discount>>> getalldiscounts() 
        {
            var discounts = await discountservice.Get_discountasync();
            return Ok(discounts);
        }

        [HttpGet]
        [Route("getdiscounts/product")]
        public async Task<ActionResult<IList<Applieddiscount>>> getallproductdiscounts() 
        {
          var product_discounts =  await applydiscountservice.listproductdiscount();

            return Ok(product_discounts);
        }

        [HttpGet("getdiscount/{id}")]

        public async Task<ActionResult<discount>> getdiscountbyid(int id) 
        {
            var discount = await discountservice.GetDiscountid(id);
            return Ok(discount);
        }

        [HttpPut]
        [Route("updatediscount")]

        public async Task<ActionResult> updatediscount(discount discount) 
        {
            await discountservice.Update_discount(discount);
            return Ok("discount is applied");
        }

        [HttpDelete("deletediscount/{id}")]

        public async Task<ActionResult> deletediscount(int id) 
        {
            await discountservice.Delete_discount(id);
            return Ok("discount is deleted");

        }


        [HttpDelete]
        [Route("delete/productdiscount")]

        public async Task<ActionResult> deleteproductdiscount(discountapplied_product discountapplied_Product) 
        {
            await applydiscountservice.deleteproductdiscount(discountapplied_Product);

            return Ok("discount on product is removed successfully");
        }



    }
}
