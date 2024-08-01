using System.ComponentModel.DataAnnotations;

namespace Project1.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }

        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        [Required(ErrorMessage = "The Email field is required.")]
        public string Email { get; set; } = String.Empty;

        [Required(ErrorMessage = "The Password field is required.")]
        public string Password { get; set; } = String.Empty;

        [Required(ErrorMessage = "Enter firstname")]
        public string FirstName { get; set; } = String.Empty;

        [Required(ErrorMessage = "Enter lastname")]
        public string LastName { get; set; } = String.Empty;

        [Required(ErrorMessage = "Enter phone number")]
        [Phone(ErrorMessage = "Invalid phone number")]
        public string Phone { get; set; } = String.Empty;

        [Required(ErrorMessage = "The Address field is required.")]
        public string Address { get; set; } = String.Empty;

        [Required(ErrorMessage = "The Country field is required.")]
        public string Country { get; set; } = String.Empty;

        //[Required]
        public string Resume { get; set; } = String.Empty;

    }
}
