using Ecommerce.DAL.models;

namespace Ecommerce.BL.Services
{
    public interface IOrderservice
    {
        public Task AddOrder(Shoppingcart shoppingcart);

        public Task<IList<Order>> Getorders();
    }
}
