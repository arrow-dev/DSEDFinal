using System.Collections.Generic;
using DSEDFinal.Models;

namespace DSEDFinal.ViewModels
{
    public class HomeViewModel
    {
        public IEnumerable<Organization> MyOrganizations { get; set; }
        public IEnumerable<Organization> MyMemberships { get; set; }
    }
}