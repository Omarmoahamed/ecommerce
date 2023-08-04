using Ecommerce.BL.Services;
using Ecommerce.DAL.models;
using Ecommerce.DAL.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.EcommerceAPI.controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        public Iproductservice productservice;
        public IRepository<Product> repository;

        public ProductController(Iproductservice productservice, IRepository<Product> repository)
        {
            this.productservice = productservice;
            this.repository = repository;
        }


        [HttpGet("getproduct/{id}")]
        public async Task<ActionResult<Product>> Getproductbyid(int id)
        {
            var product = await productservice.GetProductaByIdAsync(id);
            if (product == null)
            {
                return BadRequest("product isn't found");
            }

            return Ok(product);
        }

        [HttpGet("getproduct/category/{id}")]
        public async Task<ActionResult<IList<Product>>> getproductbycategory(int id) 
        {
            var products = await repository.Getbyidsasync(c => c.categoryid == id);

            return Ok(products);
        }

        [HttpGet]

        public async Task<ActionResult<IList<Product>>> getall()
        {
            var products = await productservice.GetProductsAsync();

            return Ok(products);
        }

        [HttpPost]
        [Route("Addproduct")]
        public async Task<ActionResult> addproduct([FromBody] Product pr)
        {
            await productservice.Addproduct(pr);

            return Ok("product is added successfully");

        }


        [HttpDelete("delete/{id}")]
        
        public async Task<ActionResult> deleteproduct(int id) 
        {
           await productservice.Deleteproduct(id);
            return Ok("product deleted successfully");
        }

        [HttpPut]
        [Route("updateproduct")]

        public async Task<ActionResult> updateproduct(Product pr) 
        {
            await productservice.Updateproduct(pr);

            return Ok("product is successfully updated");
        }
        

    }
}
