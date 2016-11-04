using System.ComponentModel.DataAnnotations;

namespace DSEDFinal.ViewModels
{
    public class HazardFormViewModel
    {
        [Required]
        public string Description { get; set; }

        public int JobId { get; set; }
    }
}