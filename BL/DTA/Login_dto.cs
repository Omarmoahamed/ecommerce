using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ecommerce.BL.DAT
{
    public class Login_dto
    {
        [Required(ErrorMessage ="username is required")]
       
        public string username { get; set; } = string.Empty;

        [Required(ErrorMessage ="password is required")]
        public string password { get; set; } = string.Empty;
    }
}
