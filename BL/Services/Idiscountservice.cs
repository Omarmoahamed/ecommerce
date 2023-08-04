using Ecommerce.DAL.models;

namespace Ecommerce.BL.Services
{
    public interface Idiscountservice
    {
        public Task Add_discount(discount discount);

        public Task<bool> appliedDiscountTotalCost(discount disc);

        public Task Update_discount(discount discount);

        public IAsyncEnumerable<discountapplied_product> Delete_discount(int discountid);

        public Task delete(int id);
        public Task<IList<discount>> Get_discountasync();

        public Task<discount> GetDiscountid(int discountid);


    }
}
