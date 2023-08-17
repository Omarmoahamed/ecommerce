using Ecommerce.DAL.models;
using System.Linq.Expressions;

namespace Ecommerce.DAL.Repository
{
    public interface IrepositoryNoTracking<Tentity> : IRepository<Tentity> where Tentity : Baseentity
    {
        Task<Tentity> GetbyidNoTracking(int id);

        Task<IEnumerable<Tentity>> GetAllNoTracking();

        Task<Tentity> GetbyidNoTracking(params Expression<Func<Tentity, bool>>[] exp);

        Task<IEnumerable< Tentity>> GetlistbyidNoTracking(params Expression<Func<Tentity, bool>>[] exp);
    }
}
