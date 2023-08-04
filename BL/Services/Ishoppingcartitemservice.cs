using Ecommerce.DAL.models;

namespace Ecommerce.BL.Services
{
    public interface Ishoppingcartitemservice
    {
        public Task addcartitem(int id);
        public Task removecartitem(int id);
        public Task clearall();
    }
}
