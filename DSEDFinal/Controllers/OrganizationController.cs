using DSEDFinal.Models;
using DSEDFinal.ViewModels;
using Microsoft.AspNet.Identity;
using System.Linq;
using System.Web.Mvc;

namespace DSEDFinal.Controllers
{
    public class OrganizationController : Controller
    {
        private ApplicationDbContext _context;

        public OrganizationController()
        {
            _context = new ApplicationDbContext();
        }

        [Authorize]
        public ActionResult Index()
        {
            var user_Id = User.Identity.GetUserId();
            var organizations = _context.Organizations
                .Where(o => o.OwnerId == user_Id)
                .ToList();

            return View(organizations);
        }

        [Authorize]
        public ActionResult Create()
        {
            var viewModel = new OrganizationFormViewModel();

            return View(viewModel);
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(OrganizationFormViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return View("Create", viewModel);
            }
            var organization = new Organization()
            {
                Name = viewModel.Name,
                OwnerId = User.Identity.GetUserId()
            };

            _context.Organizations.Add(organization);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}