using Ecommerce.DAL.models;
using Ecommerce.DAL.Repository;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.BL.Services
{
    public class Shoppingcartservice : Ishoppingcart
    {
        private readonly HttpContext? httpContext;

        public IRepository<Shoppingcart> repository;
        public IRepository<Shopping_cart_item> itemRepository;

        public Shoppingcartservice(IHttpContextAccessor httpContextAccessor, IRepository<Shoppingcart> repository, IRepository<Shopping_cart_item> itemrepository) 
        {
            this.httpContext = httpContextAccessor.HttpContext;
            this.repository = repository;
            this.itemRepository = itemrepository;
        }

        public async Task<Shoppingcart> Addshoppingcart() 
        {
            Shoppingcart shoppingcart = new Shoppingcart();
            
                var cartguid = shoppingcart.cartguid;
                httpContext!.Response.Cookies.Append("cartid", cartguid.ToString(), new CookieOptions() { MaxAge = TimeSpan.FromDays(399)} );
                await  repository.Add(shoppingcart);
                return shoppingcart;
               
            
            
            
        }

        public  async Task<Shoppingcart> GetShoppingcart() 
        {
            
            if(httpContext!.Request.Cookies["cartid"] == null) 
            {
              await  Addshoppingcart();
            }
            string? guid = httpContext.Request.Cookies["cartid"];
            var cartguid = new Guid(guid!);

          var cart =   await repository.Query().Include(s=> s.cartItems).Where(s=> s.cartguid == cartguid).FirstAsync();

            return cart;

            


        }
    }
}
