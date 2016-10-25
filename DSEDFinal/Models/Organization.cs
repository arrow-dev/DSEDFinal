using System.ComponentModel.DataAnnotations;

namespace DSEDFinal.Models
{
    public class Organization
    {
        public int Id { get; set; }

        [Required]
        [StringLength(255)]
        public string Name { get; set; }

        [Required]
        public string OwnerId { get; set; }
        public ApplicationUser Owner { get; set; }
    }
}