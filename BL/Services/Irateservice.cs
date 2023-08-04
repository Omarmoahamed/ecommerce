using Ecommerce.DAL.models;

namespace Ecommerce.BL.Services
{
    public interface Irateservice
    {
        public Task AddRate(int rating, int productid);
        public Task UpdateRate(int rateid,int rating);
        public Task DeleteRate(int rate);

        public Task<Rate> GetUserRate(int productid);

    }
}
