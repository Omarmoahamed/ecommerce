using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WebApplication2.DAL.models;

namespace Ecommerce.BL.DTA
{
    public class Registeruser_dto
    {
        [Required(ErrorMessage ="username is required")]
        [MaxLength(20, ErrorMessage ="max length is 20 characters")]
        [MinLength(5, ErrorMessage = "mininmum length is 5 characters")]
        public string username { get; set; } 

        [Required(ErrorMessage = "password is required")]
        [DataType(DataType.Password)]
        public string password { get; set; }

        [EmailAddress]
        [Required(ErrorMessage = "email is required")]

        public string emailAddress { get; set;}

        [RegularExpression(@"^(011|012|010|015)[0-9]{8}", ErrorMessage = "phonenumber is invalid")]
        public string phoneNumber { get; set; }

        public string address { get; set; }

        public User userbinding() 
        {
            return new User() {Email = this.emailAddress, Address = this.address, UserName = this.username, SecurityStamp= Guid.NewGuid().ToString(),Role = "User" }; 
        }
    }
}
