using System.ComponentModel.DataAnnotations;

namespace DSEDFinal.ViewModels
{
    public class JobFormViewModel
    {
        [Required]
        public string Reference { get; set; }

        public int OrganizationId { get; set; }
    }
}