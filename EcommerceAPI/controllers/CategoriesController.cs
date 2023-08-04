using Ecommerce.BL.Services;
using Ecommerce.DAL.models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.EcommerceAPI.controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        public Icategoryservice categoryservice;

        public CategoriesController(Icategoryservice categoryservice) 
        {
            this.categoryservice = categoryservice;
        }


        [HttpPost]
        [Route("addcategories")]

        public async Task<ActionResult> add(Category category) 
        {
            await categoryservice.addcategory(category);
            return Ok("added successfully");
        }

        [HttpGet]
        [Route("Getcategories")]
        public async Task<ActionResult<IList<Category>>> getcategories() 
        {
           var category = await categoryservice.getcategories();

            return Ok(category);
        }

        [HttpGet("Getcategories/products/{id}")]

        public async Task<ActionResult<Category>> getcategories_products(int id) 
        {
            var categories = await categoryservice.getcategory_products(id);

            return Ok(categories);
        }


        [HttpPut]
        [Route("updatecatgory")]

        public async Task<ActionResult> updatecategory(Category category) 
        {
            await categoryservice.updatecategory(category);
            return Ok("category is updated successfully");
        }

        [HttpDelete("delete/{id}")]

        public async Task<ActionResult> deletecategory(int id) 
        {
            await categoryservice.deletecategory(id);

            return Ok("category deleted successfully");
        }


    }
}
