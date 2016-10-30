using DSEDFinal.Models;
using System.Collections.Generic;

namespace DSEDFinal.ViewModels
{
    public class OrganizationDetailsViewModel
    {
        public Organization Organization { get; set; }
        public IEnumerable<Membership> Memberships { get; set; }
    }
}