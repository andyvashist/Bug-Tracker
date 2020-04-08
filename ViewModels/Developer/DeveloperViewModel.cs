using System.Collections.Generic;

namespace BugTracker.ViewModels.Developer
{
    public class DeveloperViewModel
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public virtual ICollection<Models.Project> Projects { get; set; }
        public bool IsSelected { get; set; }
    }
}