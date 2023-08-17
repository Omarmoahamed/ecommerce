using Ecommerce.DAL.Data;
using Ecommerce.DAL.models;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Ecommerce.DAL.Repository
{
    public class RepositoryNoTracking<Tentity> : GeneralRepository<Tentity>, IrepositoryNoTracking<Tentity> where Tentity : Baseentity
    {
      
        // This extended class from generalrepository is for better preformance using AsNoTracking to just only read data.
        
        public RepositoryNoTracking(DataContext dataContext) : base(dataContext) 
        {


        }

        public async Task< Tentity> GetbyidNoTracking(int id) 
        {
           var item = await dbset.Where(x => x.ID == id).AsNoTracking().FirstOrDefaultAsync();

            return item!;

        }

        public async Task<IEnumerable<Tentity>> GetAllNoTracking() 
        {

            var items =  await dbset.AsNoTracking().ToListAsync();
            return items;

        }
        public async Task<Tentity> GetbyidNoTracking(params Expression<Func<Tentity,bool>>[] exp)
        {
            var expression = exp[0];
            var item = await dbset.Where(expression).AsNoTracking().FirstOrDefaultAsync();

            return item!;

        }

        public async Task<IEnumerable<Tentity>> GetlistbyidNoTracking(params Expression<Func<Tentity, bool>>[] exp) 
        {
            var expression = exp[0];
            var items = await dbset.AsNoTracking().Where(expression).ToListAsync();

            return items;
        }





    }
}
