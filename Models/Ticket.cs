using System.ComponentModel.DataAnnotations;

namespace BugTracker.Models
{
    public class Ticket
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        [Required]
        public int ProjectId { get; set; }

        public Project Project { get; set; }



    }
}