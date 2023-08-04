using Ecommerce.DAL.Data;
using Ecommerce.DAL.models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Scaffolding.Metadata;
using System.Linq.Expressions;

namespace Ecommerce.DAL.Repository
{
    public class GeneralRepository<Tentity> : IRepository<Tentity> where Tentity : Baseentity
    {
        public GeneralRepository(DataContext dataContext) 
        {
            this.dataContext = dataContext;

            dbset = dataContext.Set<Tentity>();
        }

        protected DbSet<Tentity> dbset;

        protected DataContext dataContext;

        public IQueryable<Tentity> Query() 
        {
            return dbset;
        }



        public async Task<IList<Tentity>> GetAllasync()
        {
            var items =  await Query().ToListAsync();

            return items;

        }

        public async Task<Tentity> Getbyidasync(int id) 
        {
         Tentity? entity = await dbset.FindAsync(id);
            

            return entity;
        }

        public async Task<IList<Tentity>> Getbyidsasync(int id) 
        {
            var items =  await Query().Where(obj => obj.ID== id).ToListAsync();
            return items;
        }



        public async Task<IList<Tentity>> Getbyidsasync(params Expression<Func<Tentity, bool>>[] expression) 
        {
            Expression<Func<Tentity, bool>> exp = expression[0];
            IList<Tentity> items =  await Query().Where(exp).ToListAsync();
            return items;
            

            
        }

        public async Task<Tentity> Getbyidasync(params Expression<Func<Tentity, bool>>[] expression)
        {
            Expression<Func<Tentity, bool>> exp = expression[0];
            Tentity item = await Query().Where(exp).FirstAsync();
            return item;



        }
        public async Task Add(Tentity entity) 
        {
            await dbset.AddAsync(entity);

            await dataContext.SaveChangesAsync();
        }
         
        public async Task Delete(Tentity entity) 
        {
             dbset.Remove(entity);
            await dataContext.SaveChangesAsync();
        }

        public async Task Delete(IList< Tentity> entity)
        {
            dbset.RemoveRange(entity);
            await dataContext.SaveChangesAsync();
        }
        public async Task Delete(int id) 
        {
            var item =await dbset.FindAsync(id);
            if (item != null) 
            {
                dbset.Remove(item);

                await dataContext.SaveChangesAsync();
            }

            
        }

        public async Task Update(Tentity tentity) 
        {
            dbset.Update(tentity);
            await dataContext.SaveChangesAsync();
        }


    }
}
