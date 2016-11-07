using DSEDFinal.Models;
using DSEDFinal.ViewModels;
using Microsoft.AspNet.Identity;
using System.Linq;
using System.Web.Mvc;

namespace DSEDFinal.Controllers
{
    public class HazardController : Controller
    {
        private ApplicationDbContext _context;

        public HazardController()
        {
            _context = new ApplicationDbContext();
        }
        [Authorize]
        public ActionResult Create(int id)
        {
            var userId = User.Identity.GetUserId();
            var viewModel = new HazardFormViewModel()
            {
                JobId = id
            };
            return View(viewModel);
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(HazardFormViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return View("Create", viewModel);
            }
            var hazard = new Hazard()
            {
                Description=viewModel.Description,
                JobId = viewModel.JobId,
                UserId = User.Identity.GetUserId()
            };

            var job = _context.Jobs.Find(viewModel.JobId);

            var notification= new Notification(job);
        
            var organizatonId = job.OrganizationId;

            var recipents = _context.Memberships
                .Where(m => m.OrganizationId == organizatonId)
                .Select(m => m.Member)
                .ToList();

            foreach (var user in recipents)
            {
                user.Notify(notification);
            }

            _context.Hazards.Add(hazard);
            _context.SaveChanges();

            return RedirectToAction("Details", "Job", new { id = viewModel.JobId });
        }
    }
}