using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Ecommerce.DAL.models
{
    public class Shopping_cart_item : Baseentity
    {

        public string shopcartGid { get; set; }
        [ForeignKey("Products")]
        public int productid { get; set; }

        public int quantity { get; set; }

        public decimal price { get; set; }

        public string productname { get; set; }

        public int Shoppingcartid { get; set; }

       


        [JsonIgnore(Condition = JsonIgnoreCondition.Always)]
        public Shoppingcart shopcart { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.Always)]
        public Product products { get; set; }


    }
}
