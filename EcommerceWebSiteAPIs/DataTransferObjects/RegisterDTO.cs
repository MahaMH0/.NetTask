using System.ComponentModel.DataAnnotations;

namespace EcommerceWebSiteAPIs.DTO
{
    public class RegisterDTO
    {
        [EmailAddress]
        [Required(ErrorMessage ="Email Address is Required")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Name Required")]
        [Display(Name = "Full Name")]
        [RegularExpression(@"^[A-Za-z]{3,}(\s[A-Za-z]{3,})+$",
           ErrorMessage = "Name is not valid")]
        public string FullName { get; set; }
        [Required(ErrorMessage = "Phone Required")]
        [Display(Name = "Phone Number")]
        [RegularExpression(@"^(011|012|010|015)[0-9]{8}",
            ErrorMessage = "Phone is not valid")]
        public string Phone { get; set; }
        [Required(ErrorMessage = "Enter Username")]
        [MaxLength(15, ErrorMessage = "maximum Length is 10")]
        [MinLength(5, ErrorMessage = "minimum Length is 5")]
        public string Username { get; set; }
        [Required(ErrorMessage = "Enter Password")]
        [DataType(DataType.Password)]
        public string password { get; set; }
        public string Address { get; set; }
        public string DeliveryOptions { get; set; }
    }
}
