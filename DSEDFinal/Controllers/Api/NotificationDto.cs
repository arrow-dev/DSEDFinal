using System;

namespace DSEDFinal.Controllers.Api
{
    public class NotificationDto
    {
        public DateTime DateTime { get;  set; }
        public HazardDto Hazard { get;  set; }
    }
}