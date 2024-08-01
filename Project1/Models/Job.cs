using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Project1.Models
{
    public class Job
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "The Job Title field is required.")]
        public string JobTitle { get; set; } = String.Empty;

        [Required(ErrorMessage = "The Number of Posts field is required.")]
        public int Noofposts { get; set; }

        [Required(ErrorMessage = "The Job Description field is required.")]
        public string JobDescription { get; set; } = String.Empty;

        [Required(ErrorMessage = "The Experience field is required.")]
        public string Experience { get; set; } = String.Empty;

        [Required(ErrorMessage = "The Specialisation field is required.")]
        public string Specialisation { get; set; } = String.Empty;

        [Required(ErrorMessage = "The Last Date to Apply field is required.")]
        public string LastDateToApply { get; set; } = String.Empty;

        [Required(ErrorMessage = "The Salary field is required.")]
        public double Salary { get; set; }

        [Required(ErrorMessage = "The Job Type field is required.")]
        public string JobType { get; set; } = String.Empty;





        public int CompanyRefId { get; set; }

        [ForeignKey("CompanyRefId"), Required]
        public Company Company { get; set; } = new Company();





        [Required(ErrorMessage = "The Date Created field is required.")]
        public string DateCreated { get; set; } = String.Empty;
    }
}
