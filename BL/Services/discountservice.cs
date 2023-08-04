using Ecommerce.DAL.models;
using Ecommerce.DAL.Repository;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.BL.Services
{
    public class discountservice : Idiscountservice
    {
        protected IRepository<discount> discount_repository;
        public IRepository<discountapplied_product> discountapplied_product_repository;

        public Iapplydiscountservice productdiscountservice;

        public discountservice(IRepository<discount> discount_repository, Iapplydiscountservice productdiscount, IRepository<discountapplied_product> discountapplied_product_repository) 
        {
            this.discount_repository = discount_repository;
            this.productdiscountservice = productdiscount;
            this.discountapplied_product_repository = discountapplied_product_repository;
        }

        public async Task Add_discount(discount discount)
        {
           await discount_repository.Add(discount);
        }

        public async Task<bool> appliedDiscountTotalCost(discount disc) 
        {
            var applied =  await discount_repository.Query().Where(d => d.IsApplied_orderdiscount == true).FirstAsync();

            if(applied == null && disc.discount_type == discount.discounttype.discount_ordertotal) 
            {
                if (disc.discount_startdate <= DateTime.Now && disc.discount_enddate > DateTime.Now)
                {
                    disc.IsApplied_orderdiscount = true;
                    return true;
                }
               
                
            }
            return false;
        }

        public async Task Update_discount(discount discount) 
        {
           await discount_repository.Update(discount);
        }

        public  async IAsyncEnumerable<discountapplied_product> Delete_discount(int discountid) 
        {
            var discount = await discount_repository.Getbyidasync(discountid);
            
            if (discount.discount_type == discount.discounttype.discount_product) 
            {
               var discountproduct =  await discountapplied_product_repository.Getbyidsasync(d => d.discount_id == discount.ID);
                await discount_repository.Delete(discountid);
                foreach (var productdiscount in discountproduct) 
                {
                    yield return productdiscount;
                }
            }
            await discount_repository.Delete(discountid);
        }
        

        public async Task delete(int id) 
        {
            await foreach(var discountproduct in this.Delete_discount(id)) 
            {
              await   productdiscountservice.deleteproductdiscount(discountproduct);
            }
        }

        public async Task<IList<discount>> Get_discountasync() 
        {
            var discounts = await discount_repository.GetAllasync();
            return discounts;
        }

        public async Task<discount> GetDiscountid(int discountid) 
        {
            var discount = await discount_repository.Getbyidasync(discountid);
            return discount;
        }
    }
}
