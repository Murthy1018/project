using System.ComponentModel.DataAnnotations;

namespace Project1.Models
{
    public class Company
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "The Company Name field is required.")]
        public string Name { get; set; } = String.Empty;

        public string Logo { get; set; } = String.Empty;

        [Required(ErrorMessage = "The Company Website field is required.")]
        public string Website { get; set; } = String.Empty;

        [Required(ErrorMessage = "The Contact Email field is required.")]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string Email { get; set; } = String.Empty;

        [Required(ErrorMessage = "The Company City field is required.")]
        public string City { get; set; } = String.Empty;

        [Required(ErrorMessage = "The Company State field is required.")]
        public string State { get; set; } = String.Empty;

        [Required(ErrorMessage = "The Company Country field is required.")]
        public string Country { get; set; } = String.Empty;
    }
}
