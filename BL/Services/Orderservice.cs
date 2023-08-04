using Ecommerce.DAL.models;
using Ecommerce.DAL.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace Ecommerce.BL.Services
{
    public class Orderservice : IOrderservice
    {
        public IRepository<Order> repository;
        public IRepository<Shopping_cart_item> cart_item;
        public IRepository<Orderitem> orderitemrepository;
        public Ishoppingcartitemservice cartitemservice;
        public IRepository<discount> discountRepository;
        public HttpContext httpContext;
        public Orderservice(IRepository<Order> repository, IRepository<discount> discountRepository, IHttpContextAccessor httpContextAccessor, IRepository<Shopping_cart_item> cart_item, Ishoppingcartitemservice cartitemservice, IRepository<Orderitem> orderitem) 
        {
            this.repository = repository;
            this.discountRepository = discountRepository;
            this.httpContext = httpContextAccessor.HttpContext!;
            this.cart_item = cart_item;
            this.cartitemservice = cartitemservice;
            this.orderitemrepository = orderitem;
        }

        public async Task AddOrder(Shoppingcart shoppingcart) 
        {

            var discount = discountRepository.Getbyidasync(d => d.IsApplied_orderdiscount == true).Result;
            var userId = httpContext?.User.FindFirst(ClaimTypes.NameIdentifier)!.Value;
            var order = new Order()
            {
                Userid = userId!,
                Total_cost = shoppingcart.Totalcost,
                quantity = shoppingcart.totalquantity,
            };

            if (discount != null) 
            {
                order.Total_cost = order.Total_cost - (order.Total_cost * discount.discount_percentage);
            }
            await this.repository.Add(order);

            IEnumerable<Shopping_cart_item> cart_Items = shoppingcart.cartItems;

            foreach (var item in cart_Items) 
            {
                var cartitem = new Orderitem()
                {
                    price= item.price,
                    quantity= item.quantity,
                    Orderid = order.ID,
                    productname = item.productname,
                    
                };

              await  orderitemrepository.Add(cartitem);            
            }
            await cartitemservice.clearall();
        }



        public async Task<IList<Order>> Getorders() 
        {
          var userId = httpContext?.User.FindFirst(ClaimTypes.NameIdentifier)!.Value;

          var orders =  await repository.Query().Include(o=> o.order_items).Where(o=>o.Userid==userId).ToListAsync();

          return orders;
        }
    }
}
