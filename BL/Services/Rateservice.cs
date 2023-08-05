using Ecommerce.DAL.models;
using Ecommerce.DAL.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.VisualBasic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using WebApplication2.DAL.models;
using static Ecommerce.DAL.models.Rate;

namespace Ecommerce.BL.Services
{
    public class Rateservice : Irateservice
    {
        public IRepository<Rate> repository;

        public Iproductservice productservice;

        public UserManager<User> userManager;
        public HttpContext? httpContext;
        public Rateservice(IRepository<Rate> repository, UserManager<User> userManager, IHttpContextAccessor httpContextAccessor, Iproductservice productservice)
        {
            this.repository = repository;
            this.userManager = userManager;
            this.httpContext = httpContextAccessor.HttpContext;
            this.productservice = productservice;
        }

        public async Task AddRate(int rating, int productid) 
        {
            var userId = httpContext?.User.FindFirst(ClaimTypes.NameIdentifier)!.Value;
            var user =  await userManager.FindByIdAsync(userId);
            var product = await productservice.GetProductaByIdAsync(productid);

            Rate rate = new Rate()
            {
                Userid = userId!,
                productid = productid,
                product_rating = (Rate.rating) rating
            };
            await repository.Add(rate);
            var ratecount = repository.GetAllasync().Result.Count();
            product.rate = (product.rate+rating)/ ratecount;
          await  productservice.Updateproduct(product);
            
        }

        public async Task UpdateRate(int rateid ,int rating) 
        {
            var rate = await repository.Getbyidasync(rateid);
            var product = await productservice.GetProductaByIdAsync(rate.productid);

            rate .product_rating = (Rate.rating) rating;

            await repository.Update(rate);

            var ratecount = repository.GetAllasync().Result.Count();
            product.rate = (product.rate + rating) / ratecount;
          await  productservice.Updateproduct(product);

        }

        public async Task DeleteRate(int rateid) 
        {
            var rate = await repository.Getbyidasync(rateid);
            var product = await productservice.GetProductaByIdAsync(rate.productid);

            var ratecount = repository.GetAllasync().Result.Count();
            product.rate = (product.rate - Convert.ToInt32(rate.product_rating)) / ratecount;
          await  productservice.Updateproduct(product);
            await repository.Delete(rate);
        }

        public async Task<Rate> GetUserRate(int productid) 
        {
            var userId = httpContext?.User.FindFirst(ClaimTypes.NameIdentifier)!.Value;
            
            var rate = await repository.Getbyidasync(r => r.productid== productid && r.Userid == userId);

            return rate;


            



        }
    }
}
