using Ecommerce.DAL.models;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication2.DAL.models
{
    public class User : IdentityUser
    {
        public enum quality 
        {
            High = 1,
            Medium = 2,
            Low = 3,

        }
        public string Role { get; set; } = string.Empty;
       
        public quality? specs { get; set; }

        public string Address { get; set; } = string.Empty;

        public IEnumerable<Rate> rates { get; set; }

        public IEnumerable<Order> orders { get; set; }
    }
}
