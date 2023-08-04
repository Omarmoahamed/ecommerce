using System.Text.Json.Serialization;

namespace Ecommerce.DAL.models
{
    public class Orderitem : Baseentity
    {
        public string? productname { get; set; }

        public decimal price { get; set; }

        public int quantity { get; set; }

      

        public int Orderid { get; set; }

        [JsonIgnore(Condition =JsonIgnoreCondition.Always)]
        public Order? order { get; set; }
    }
}
