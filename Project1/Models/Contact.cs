using System.ComponentModel.DataAnnotations;

namespace Project1.Models
{
    public class Contact
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = " Name is required.")]
        public string Name { get; set; } = String.Empty;

        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Invalid email address.")]
        public string Email { get; set; } = String.Empty;

        [Required(ErrorMessage = "Subject is required.")]
        public string Subject { get; set; } = String.Empty;

        [Required(ErrorMessage = "Message is required.")]
        public string Message { get; set; } = String.Empty;
    }
}
