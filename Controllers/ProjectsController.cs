using BugTracker.Models;
using BugTracker.ViewModels;
using BugTracker.ViewModels.Developer;
using BugTracker.ViewModels.Project;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace BugTracker.Controllers
{
    public class ProjectsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        public ProjectsController()
        {
            db = new ApplicationDbContext();
        }

        public ActionResult Index()
        {
            return View(db.Projects.ToList());
        }

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var project = db.Projects.Find(id);
            project.Tickets = db.Tickets.Where(t => t.ProjectId == id).ToList();
            if (project == null)
            {
                return HttpNotFound();
            }
            return View(project);
        }

        public ActionResult Create()
        {
            var developersInDb = db.Developers.ToList();
            var managersInDb = db.Managers.ToList();
            var selectedDevelopers = new List<SelectedDevelopers>();
            var managers = new List<ManagerViewModel>();

            foreach (var developer in developersInDb)
            {
                selectedDevelopers.Add(new SelectedDevelopers
                {
                    Id = developer.Id,
                    FirstName = developer.FirstName,
                    IsSelected = false
                });
            }

            foreach (var manager in managersInDb)
            {
                managers.Add(new ManagerViewModel
                {
                    Id = manager.Id,
                    FirstName = manager.FirstName,
                    IsSelected = false
                });
            }


            var project = new ProjectViewModel { SelectedDevelopers = selectedDevelopers, Managers = managers };

            return View(project);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ProjectViewModel project)
        {
            if (ModelState.IsValid)
            {
                var Project = new Project
                {
                    Title = project.Title,
                    Description = project.Description,

                };

                foreach (var manager in project.Managers)
                {
                    if (manager.IsSelected)
                    {
                        Project.ManagerId = manager.Id;
                    }
                }
                AddOrUpdateDevelopers(Project, project.SelectedDevelopers);

                db.Projects.Add(Project);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(project);
        }



        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var data = db.Projects
                .Where(p => p.Id == id)
                .Select(p => new
                {
                    ViewModel = new ProjectViewModel
                    {
                        Id = p.Id,
                        Title = p.Title,
                        Description = p.Description,
                    },
                    DeveloperIds = p.Developers.Select(d => d.Id)
                })
                .SingleOrDefault();

            if (data == null)
                return HttpNotFound();

            //load all companies from db
            data.ViewModel.Developers = db.Developers
                .Select(d => new DeveloperViewModel
                {
                    Id = d.Id,
                    FirstName = d.FirstName
                })
                .ToList();

            //SET IS SELECTED TRUE IF DEVELOPER IS ALREADY SELECTED

            foreach (var developer in data.ViewModel.Developers)
            {
                developer.IsSelected = data.DeveloperIds.Contains(developer.Id);
            }


            return View(data.ViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ProjectViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var project = db.Projects.Include(p => p.Developers)
                            .SingleOrDefault(p => p.Id == viewModel.Id);

                if (project != null)
                {
                    project.Title = viewModel.Title;
                    project.Description = viewModel.Description;

                    foreach (var developer in viewModel.Developers)
                    {
                        if (developer.IsSelected)
                        {
                            if (!project.Developers.Any(
                               d => d.Id == developer.Id))
                            {
                                var selectedDeveloper = new Developer
                                {
                                    Id = developer.Id
                                };

                                db.Developers.Attach(selectedDeveloper);
                                project.Developers.Add(selectedDeveloper);

                            }
                        }

                        else
                        {
                            var removedDeveloper = project.Developers
                                                 .SingleOrDefault(d => d.Id == developer.Id);

                            if (removedDeveloper != null)
                            {
                                project.Developers.Remove(removedDeveloper);
                            }
                        }
                    }

                    db.SaveChanges();
                }
                return RedirectToAction("Index");
            }
            return View(viewModel);
        }

        public ActionResult Delete(int id)
        {
            var viewModel = db.Projects.Find(id);

            return View(viewModel);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirm(int id)
        {
            Project project = db.Projects.Find(id);
            db.Projects.Remove(project);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        private void AddOrUpdateDevelopers(Project project, IEnumerable<SelectedDevelopers> selectedDevelopers)
        {
            if (selectedDevelopers != null)
            {
                foreach (var selectedDeveloper in selectedDevelopers)
                {
                    if (selectedDeveloper.IsSelected)
                    {
                        var developer = new Developer { Id = selectedDeveloper.Id };
                        db.Developers.Attach(developer);
                        project.Developers.Add(developer);
                    }
                }
            }
        }

        //private void AddOrUpdateManager(Project project,IEnumerable<ManagerViewModel> selectedManagers)
        //{
        //    if (selectedManagers != null)
        //    {
        //        foreach (var manager in selectedManagers)
        //        {
        //            project.
        //        }
        //    }
        //}
    }
}
