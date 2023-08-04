using System.ComponentModel.DataAnnotations.Schema;

namespace Ecommerce.DAL.models
{
    public class discount : Baseentity
    {
        public enum discounttype 
        {
            discount_ordertotal = 1,

            discount_product = 2,

            

        }
        public string name { get; set; }

        public bool IsApplied_orderdiscount { get; set; }
        public decimal discount_percentage { get; set; }

        
        public discounttype discount_type { get; set; } 
        public decimal discount_mintotalprice { get; set; }
        public int discount_minAmount { get; set; }

        public DateTime discount_startdate { get; set; }

        public DateTime discount_enddate { get; set; }

        
    }
}
