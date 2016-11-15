using DSEDFinal.Models;
using DSEDFinal.ViewModels;
using Microsoft.AspNet.Identity;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;

namespace DSEDFinal.Controllers
{
    public class JobController : Controller
    {
        private ApplicationDbContext _context;

        public JobController()
        {
            _context=new ApplicationDbContext();
        }
        [Authorize]
        public ActionResult Create(int id)
        {
            var userId = User.Identity.GetUserId();

            if (_context.Memberships.FirstOrDefault(m=>m.MemberId == userId && m.OrganizationId == id) == null)
            return new HttpUnauthorizedResult();

            var viewModel = new JobFormViewModel()
            {
                OrganizationId= id
            };
            return View(viewModel);
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(JobFormViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return View("Create", viewModel);
            }
            var job = new Job()
            {
                Reference = viewModel.Reference,
                OrganizationId = viewModel.OrganizationId
            };

            _context.Jobs.Add(job);
            _context.SaveChanges();

            return RedirectToAction("Details", "Organization", new { id = viewModel.OrganizationId });
        }

        public ActionResult Details(int id)
        {
            var userId = User.Identity.GetUserId();

            var job = _context.Jobs
                .Include(j => j.Hazards.Select(h => h.User))
                .FirstOrDefault(j => j.Id == id);

            if (_context.Memberships.FirstOrDefault(m => m.MemberId == userId && m.OrganizationId == job.OrganizationId)==null)
            return new HttpUnauthorizedResult();

            return View(job);
        }
    }
}