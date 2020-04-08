using System.Collections.Generic;

namespace BugTracker.Models
{
    public class Project
    {
        public Project()
        {
            Developers = new List<Developer>();
        }
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public virtual ICollection<Developer> Developers { get; set; }
        public ICollection<Ticket> Tickets { get; set; }
        public int ManagerId { get; set; }
        public Manager Manager { get; set; }
    }
}