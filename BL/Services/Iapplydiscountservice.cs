using Ecommerce.DAL.models;

namespace Ecommerce.BL.Services
{
    public interface Iapplydiscountservice
    {
        public Task Addproductdiscount(discountapplied_product dsap);

        public Task applyproductdiscount(int id);

        public Task deleteproductdiscount(discountapplied_product discountapplied_Product);
        
        public void updateproductdiscount (discountapplied_product dsap);

        public  Task<IList<discountapplied_product>> listproductdiscount();

        public Task<discountapplied_product> getproductdiscount(int id);


    }
}
