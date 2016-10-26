using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DSEDFinal.Models
{
    public class Membership
    {

        [Key]
        [Column(Order = 1)]
        public string MemberId { get; set; }

        [Key]
        [Column(Order = 2)]
        public int OrganizationId { get; set; }

        public ApplicationUser Member { get; set; }
        public Organization Organization { get; set; }    
    }
}