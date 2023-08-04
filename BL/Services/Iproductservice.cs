using Ecommerce.DAL.models;

namespace Ecommerce.BL.Services
{
    public interface Iproductservice
    {
        public Task<IList<Product>> GetProductsAsync();

        public Task<Product> GetProductaByIdAsync(int id);

        public Task Addproduct(Product product);

        public Task Deleteproduct(int id);

        public Task Updateproduct(Product product);


    }
}
