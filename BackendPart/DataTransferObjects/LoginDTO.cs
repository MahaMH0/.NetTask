using System.ComponentModel.DataAnnotations;

namespace BackendPart.DTO
{
    public class LoginDTO
    {
        [Required(ErrorMessage = "User name is Required")]
        public string UserName { get; set; }
        [Required(ErrorMessage = "Password is Required")]
        public string Password { get; set; }
    }
}
