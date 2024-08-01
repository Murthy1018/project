using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Project1.Models
{
    public class AppliedJobs
    {
        [Key]
        public int Id { get; set; }

        public int UserRefId { get; set; }

        public int JobRefId { get; set; }

        //[ForeignKey("UserRefId"), Required]
        //public User User { get; set; } = new User();

        //[ForeignKey("JobRefId"), Required]
        //public Job Job { get; set; } = new Job();
    }
}
