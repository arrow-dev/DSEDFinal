using System.ComponentModel.DataAnnotations;

namespace DSEDFinal.ViewModels
{
    public class AddMembershipFormViewModel
    {
        public int OrganizationId { get; set; }

        [Required]
        [EmailAddress]
        [ValidUser(ErrorMessage = "This email does not belong to a registered user.")]
        public string Email { get; set; }
    }
}