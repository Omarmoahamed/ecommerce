using System.Text.Json.Serialization;

namespace Ecommerce.DAL.models
{
    public class Product : Baseentity
    {
        public string name { get; set; }

        public decimal price { get; set; }

        public decimal old_price { get; set; }

        public int quantity { get; set; }

        public string description { get; set; }

        public string image_url { get; set; }

        public bool applied_discount { get; set; }

       public int rate { get; set; }
        public IEnumerable<Rate> rates { get; set; }

        public int categoryid { get; set; }

        [JsonIgnore(Condition =JsonIgnoreCondition.Always)]
        public Category category { get; set; }

        [JsonIgnore(Condition =JsonIgnoreCondition.Always)]
        public Shopping_cart_item shopping_Cart_Items { get; set; }

    }
}
