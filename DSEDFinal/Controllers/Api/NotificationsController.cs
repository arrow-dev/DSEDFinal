using AutoMapper;
using DSEDFinal.Models;
using Microsoft.AspNet.Identity;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web.Http;

namespace DSEDFinal.Controllers.Api
{
    [Authorize]
    public class NotificationsController : ApiController
    {
        private ApplicationDbContext _context;

        public NotificationsController()
        {
            _context = new ApplicationDbContext();
        }

        public IEnumerable<NotificationDto> GetNewNotifications()
        {
            var userId = User.Identity.GetUserId();
            var notifications = _context.UserNotifications
                .Where(un => un.UserId == userId)
                .Select(un => un.Notification)
                .Include(n => n.Hazard.User)
                .Include(n => n.Hazard.Job)
                .ToList();

            Mapper.Initialize(c =>
            {
                c.CreateMap<ApplicationUser, UserDto>();
                c.CreateMap<Job, JobDto>();
                c.CreateMap<Hazard, HazardDto>();
                c.CreateMap<Notification, NotificationDto>();
            });

            return notifications.Select(Mapper.Map<Notification, NotificationDto>);
        }
    }
}
