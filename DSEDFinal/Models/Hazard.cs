using System.ComponentModel.DataAnnotations;

namespace DSEDFinal.Models
{
    public class Hazard
    {
        public int Id { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public string UserId { get; set; }
        public ApplicationUser User { get; set; }

        [Required]
        public int JobId { get; set; }
        public Job Job { get; set; }
    }
}