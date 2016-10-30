using System.ComponentModel.DataAnnotations;

namespace DSEDFinal.ViewModels
{
    public class OrganizationFormViewModel
    {
        [Required]
        [StringLength(255)]
        public string Name { get; set; }
    }
}