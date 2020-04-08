using System.Collections.Generic;

namespace BugTracker.Models
{
    public class Manager
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public List<Project> Projects { get; set; }
    }
}