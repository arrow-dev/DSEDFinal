using DSEDFinal.Models;
using DSEDFinal.ViewModels;
using Microsoft.AspNet.Identity;
using System.Data.Entity;
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
        public ActionResult Details(int id)
        {
            var viewModel = new OrganizationDetailsViewModel()
            {
                Organization = _context.Organizations.Find(id),
                Memberships = _context.Memberships.Where(m => m.OrganizationId == id).Include(m => m.Member).ToList()
            };

            return View(viewModel);
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

        [Authorize]
        public ActionResult AddMember(int id)
        {
            var viewModel = new AddMembershipFormViewModel()
            {
                OrganizationId = id
            };

            return View(viewModel);
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddMember(AddMembershipFormViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return View("AddMember", viewModel);
            }

            var userId = User.Identity.GetUserId();
            var newMemberId = _context.Users.Single(u => u.Email == viewModel.Email).Id;
            var organizationId = viewModel.OrganizationId;


            if (!_context.Memberships.Any(m => m.MemberId == newMemberId && m.OrganizationId == organizationId))
            {
                var membership = new Membership()
                {
                    OrganizationId = organizationId,
                    MemberId = newMemberId
                };

                _context.Memberships.Add(membership);
                _context.SaveChanges();
            }
            
            

            return RedirectToAction("Index");
        }
    }
}