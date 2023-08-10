using Ecommerce.DAL.models;
using Ecommerce.DAL.Repository;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.BL.Services
{
    public class Shoppingcartitemservice : Ishoppingcartitemservice
    {
        public IRepository<Shopping_cart_item> Repository;

        public IRepository<Shoppingcart> shoppingcart_repository;

        public IRepository<Product> ProductRepository;

        public Ishoppingcart cart;

        private readonly HttpContext httpContext;

        public Shoppingcartitemservice(IRepository<Shopping_cart_item> repository, Ishoppingcart cart, IHttpContextAccessor httpContextAccessor, IRepository<Product> productRepository, IRepository<Shoppingcart> shoppingcart_repository)
        {

            Repository = repository;
            this.cart = cart;
            this.httpContext = httpContextAccessor.HttpContext!;
            ProductRepository = productRepository;
            this.shoppingcart_repository= shoppingcart_repository;
        }

        public async Task addcartitem(int id) 
        {
       var shoppingcart = await cart.Addshoppingcart();

            var cartgid =  httpContext.Request.Cookies["cartid"];
            var product = await ProductRepository.Getbyidasync(id);
            var cartitem = await Repository.Query().SingleOrDefaultAsync(ci => ci.productid == product.ID && ci.shopcartGid == cartgid);

            if(cartitem == null) 
            {
                cartitem = new Shopping_cart_item()
                {
                    productid = product.ID,
                    quantity = 1,
                    price = product.price,
                    productname = product.name,
                    shopcartGid = cartgid!,
                    Shoppingcartid = shoppingcart.ID,
              };
                await Repository.Add(cartitem);
                shoppingcart.totalquantity = shoppingcart.totalquantity + 1;
                shoppingcart.Totalcost = shoppingcart.Totalcost+ cartitem.price;
                await shoppingcart_repository.Update(shoppingcart);
                
            }
            else 
            {
                cartitem.quantity++;
                shoppingcart.totalquantity= shoppingcart.totalquantity + 1;
                shoppingcart.Totalcost = shoppingcart.Totalcost + cartitem.price;
                await shoppingcart_repository.Update(shoppingcart);
                await Repository.Update(cartitem);
            }



        }

        public async Task removecartitem(int id) 
        {
            var cartgid = httpContext.Request.Cookies["cartid"];

            var cartitem = await Repository.Getbyidasync(c=> c.productid == id && c.shopcartGid == cartgid);
            var shoppingcart = await cart.GetShoppingcart();

            if(cartitem.quantity >1) 
            {
                cartitem.quantity--;
                shoppingcart.totalquantity--;
                shoppingcart.Totalcost = shoppingcart.Totalcost - cartitem.price;
                await Repository.Update(cartitem);
                await shoppingcart_repository.Update(shoppingcart);

            }
            else 
            {
                await Repository.Delete(cartitem);
                shoppingcart.totalquantity--;
                shoppingcart.Totalcost = shoppingcart.Totalcost - cartitem.price;
                await shoppingcart_repository.Update(shoppingcart);

            }
        }

        public  async Task clearall() 
        {
            var cartgid = httpContext.Request.Cookies["cartid"];

            var cartitems = await Repository.Getbyidsasync(c => c.shopcartGid == cartgid);

           await Repository.Delete(cartitems);
        }
    }
}
