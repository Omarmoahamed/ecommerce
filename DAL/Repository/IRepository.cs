using Ecommerce.DAL.models;
using System.Linq.Expressions;

namespace Ecommerce.DAL.Repository
{
    public interface IRepository<Tentity> where Tentity : Baseentity
    {


        public IQueryable<Tentity> Query();
        Task<Tentity> Getbyidasync(int id);
        Task<IList<Tentity>> GetAllasync();

        Task<IList<Tentity>> Getbyidsasync(params Expression<Func<Tentity, bool>>[] expression);

        Task<Tentity> Getbyidasync(params Expression<Func<Tentity, bool>>[] expression);
        Task Add(Tentity entity);

        Task Update(Tentity entity);
        Task Delete(int id);
        Task Delete(Tentity entity);

        Task Delete(IList <Tentity> entity);

    }
}
