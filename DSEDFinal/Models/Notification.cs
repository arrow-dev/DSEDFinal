using System;
using System.ComponentModel.DataAnnotations;

namespace DSEDFinal.Models
{
    public class Notification
    {
        public int Id { get; set; }
        public DateTime DateTime { get; private set; }
        
        [Required]
        public Hazard Hazard { get; private set; }

        protected Notification()
        {
            
        }

        public Notification(Hazard hazard)
        {
            if (hazard == null)
            {
                throw new ArgumentNullException("hazard");
            }
            DateTime=DateTime.Now;
            Hazard = hazard;
        }
    }
}