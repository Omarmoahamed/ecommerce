using Ecommerce.DAL.models;

namespace Ecommerce.BL.Services
{
    public interface Icategoryservice
    {
        public Task addcategory(Category category);

        public Task deletecategory(int id);

        public Task updatecategory(Category category);
        
        public Task<IList<Category>> getcategories();

        public Task<Category> getcategory_products(int id);

    }
}
