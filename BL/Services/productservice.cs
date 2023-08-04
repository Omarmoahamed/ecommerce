using Ecommerce.DAL.models;
using Ecommerce.DAL.Repository;

namespace Ecommerce.BL.Services
{
    public class productservice : Iproductservice
    {
        protected IRepository<Product> product_repository;

        public productservice(IRepository<Product> product_repository)
        {
            this.product_repository = product_repository;
        }


        public async Task<IList<Product>> GetProductsAsync() 
        {
           var products = await product_repository.GetAllasync();
            return products;
        }

        public async Task<Product> GetProductaByIdAsync(int id) 
        {
            var product = await product_repository.Getbyidasync(id);
            return product;
        }

        public async Task Addproduct(Product product) 
        {
          await  product_repository.Add(product);
        }

        public async Task Deleteproduct(int id) 
        {
           await product_repository.Delete(id);
        }

        public async Task Updateproduct(Product product) 
        {
           await product_repository.Update(product);
        }


    }
}
