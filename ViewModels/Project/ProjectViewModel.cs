using BugTracker.ViewModels.Developer;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BugTracker.ViewModels.Project
{
    public class ProjectViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        [Display(Name = "Assign Developers")]
        public List<SelectedDevelopers> SelectedDevelopers { get; set; }
        [Display(Name = "Edit Developers")]
        public List<DeveloperViewModel> Developers { get; set; }
        public List<ViewModels.ManagerViewModel> Managers { get; set; }
        public int ManagerId { get; set; }
        public ViewModels.ManagerViewModel Manager { get; set; }
    }
}