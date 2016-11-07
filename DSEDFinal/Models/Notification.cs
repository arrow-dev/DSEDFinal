using System;
using System.ComponentModel.DataAnnotations;

namespace DSEDFinal.Models
{
    public class Notification
    {
        public int Id { get; set; }
        public DateTime DateTime { get; private set; }
        
        [Required]
        public Job Job { get; private set; }

        protected Notification()
        {
            
        }

        public Notification(Job job)
        {
            if (job == null)
            {
                throw new ArgumentNullException("job");
            }
            DateTime=DateTime.Now;
            Job = job;
        }
    }
}