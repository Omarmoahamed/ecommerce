using System.ComponentModel.DataAnnotations.Schema;

namespace Ecommerce.DAL.models
{
    public class discountapplied_product : Baseentity
    {
        [ForeignKey("discount")]
        public int discount_id { get; set; }

        [ForeignKey("Product")]
        public int product_id { get; set; }
    }
}
