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
            var viewModel = _context.Memberships
                .Where(m => m.MemberId == user_Id)
                .Select(m => m.Organization)
                .ToList();

            return View("MyMemberships",viewModel);
        }

        [Authorize]
        public ActionResult MyOrganizations()
        {
            var user_Id = User.Identity.GetUserId();
            var viewModel = _context.Organizations
                .Where(o => o.OwnerId == user_Id)
                .ToList();

            return View("MyOrganizations", viewModel);
        }

        [Authorize]
        public ActionResult Details(int id)
        {
            var userId = User.Identity.GetUserId();
            var viewModel = new OrganizationDetailsViewModel()
            {
                Organization = _context.Organizations.Include(o => o.Jobs).FirstOrDefault(o => o.Id == id),
                Memberships = _context.Memberships.Where(m => m.OrganizationId == id).Include(m => m.Member).ToList()
            };
            if (viewModel.Organization.OwnerId== userId)
            {
                viewModel.ShowActions = true;
            }

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
            var newMember = _context.Users.Single(u => u.Email == viewModel.Email);
            var organizationId = viewModel.OrganizationId;


            if (!_context.Memberships.Any(m => m.MemberId == newMember.Id && m.OrganizationId == organizationId))
            {
                var membership = new Membership()
                {
                    OrganizationId = organizationId,
                    MemberId = newMember.Id
                };

                if (newMember.DefaultOrganizationId == null)
                {
                    newMember.DefaultOrganizationId = organizationId;
                }
                _context.Memberships.Add(membership);
                _context.SaveChanges();
            }
            
            

            return RedirectToAction("Details", new { id = organizationId});
        }

        [Authorize]
        public ActionResult Default()
        {
            var user = _context.Users.Find(User.Identity.GetUserId());
            if (user.DefaultOrganizationId == null)
            {
                return RedirectToAction("Index");
            }
            return RedirectToAction("Details", new {id = user.DefaultOrganizationId});
        }
    }
}