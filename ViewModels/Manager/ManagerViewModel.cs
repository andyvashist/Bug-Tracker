using System.Collections.Generic;

namespace BugTracker.ViewModels
{
    public class ManagerViewModel
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public bool IsSelected { get; set; }
        public List<Project.ProjectViewModel> Projects { get; set; }
    }
}