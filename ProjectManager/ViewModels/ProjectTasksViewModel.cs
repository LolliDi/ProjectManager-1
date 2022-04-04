using ProjectManager.Models;
using ProjectManager.Models.Repositories;
using ProjectManager.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManager.ViewModels
{
    class ProjectTasksViewModel : ViewModel
    {
        public ProjectTasksViewModel(Projects project)
        {

            projectTaskResourcesRepository = new Repository<ProjectTaskResources>(new ProjectManagerContext());
            projectTasksRepository = new Repository<ProjectTasks>(new ProjectManagerContext());

            ToResourcePageCommand = new LambdaCommand(parameter => NavigationService.CurrentViewModel = new ProjectResourcesViewModel(NavigationService, project));
            ToReportsPageCommand = new LambdaCommand(parameter => NavigationService.CurrentViewModel = new ReportsViewModel(NavigationService));
            ToProjectPassportPageCommand = new LambdaCommand(parameter => NavigationService.CurrentViewModel = new ProjectPassportViewModel(NavigationService));
        }

        private readonly IRepository<ProjectTasks> projectTasksRepository;
        private readonly IRepository<ProjectTaskResources> projectTaskResourcesRepository;

        public NavigationService NavigationService { get; set; }
        public ICommand ToResourcePageCommand { get; set; }
        public ICommand ToReportsPageCommand { get; set; }
        public ICommand ToProjectPassportPageCommand { get; set; }
    }
}
