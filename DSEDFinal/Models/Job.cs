using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;

namespace DSEDFinal.Models
{
    public class Job
    {
        public int Id { get; set; }

        public bool IsComplete { get; private set; }

        [Required]
        public string Reference { get; set; }

        [Required]
        public int OrganizationId { get; set; }
        public Organization Organization { get; set; }

        public ICollection<Hazard> Hazards { get; set; }

        public Job()
        {
            Hazards = new Collection<Hazard>();
            IsComplete = false;
        }

        public void Complete()
        {
            IsComplete = true;
        }

    }
}