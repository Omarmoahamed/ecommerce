using Ecommerce.DAL.models;
using Ecommerce.DAL.Repository;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.BL.Services
{
    public class Categoryservice : Icategoryservice
    {
        public IRepository<Category> Repository;

        public Categoryservice(IRepository<Category> Repository) 
        {
            this.Repository = Repository;
        }

        public async Task addcategory(Category category) 
        {
            await Repository.Add(category);
        }

        public async Task deletecategory(int id) 
        {
            await Repository.Delete(id);
        }

        public async Task updatecategory(Category category) 
        {
            await Repository.Update(category);
        }

        public async Task<IList<Category>> getcategories() 
        {
          var categories =  await Repository.GetAllasync();

            return categories;
        }

        public async Task<Category> getcategory_products(int id) 
        {
            var category = await Repository.Query().Include(c=> c.products).FirstOrDefaultAsync(c=>c.ID==id);
            return category!;
        }
    }
}
