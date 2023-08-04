using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using WebApplication2.DAL.models;

namespace Ecommerce.DAL.models
{
    public class Rate:Baseentity
    {
        public enum rating 
        {
           
            bad = 1,
            fine = 2,
            good = 3,
            v_good =4,
            excellent =5

        }
        
        public string Userid { get; set; }   
        public int productid { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.Always)]
         public Product product { get; set; }
        public rating product_rating { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.Always)]
        public User user { get; set; }
        
    }
}
