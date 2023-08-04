using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using WebApplication2.DAL.models;

namespace Ecommerce.DAL.models
{
    public class Order : Baseentity
    {
        [ForeignKey("User")]
        public string Userid { get; set; }
        public decimal Total_cost { get; set; }
        
        public DateTime created_Date { get; set; }
        public IEnumerable<Orderitem>? order_items { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.Always)]
        public User user { get; set; }

        public int quantity { get; set; }
        public Order() 
        {
            this.created_Date= DateTime.Now;
        }


    }
}
