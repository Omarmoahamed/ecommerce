using Ecommerce.DAL.models;

namespace Ecommerce.BL.Services
{
    public interface Ishoppingcart
    {
        public  Task<Shoppingcart> Addshoppingcart();

        public Task<Shoppingcart> GetShoppingcart();
    }
}
