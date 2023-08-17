using Ecommerce.DAL.models;

namespace Ecommerce.BL.Services
{
    public interface Idiscountservice
    {
        public Task Add_discount(discount discount);

        public Task Update_discount(discount discount);

        public Task Delete_discount(int discountid);

         Task<bool> appliedDiscountTotalCost(discount disc);
        public Task<IList<discount>> Get_discountasync();

        public Task<discount> GetDiscountid(int discountid);


    }
}
