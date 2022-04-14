using ProjectManager.Models;
using ProjectManager.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectManager.Models.Repositories;
using System.Windows.Input;
using ProjectManager.Commands;

namespace ProjectManager.ViewModels
{
    class ProjectResourcesViewModel : ViewModel
    {
        private NavigationService navigationService = new NavigationService();
        private Projects currentProject;

        private List<Resources> resources = new List<Resources>();
        private Repository<Resources> resourcesRepository = new Repository<Resources>();
        private Repository<ProjectResources> projectResourcesRepository = new Repository<ProjectResources>();

        public ICommand ToProjectResourcesCreateViewModel { get; set; }

        public FormNavigationService ProjectResourcesForm { get; set; }

        public ProjectResourcesViewModel(NavigationService navigationService, Projects currentProject)
        {
            this.navigationService = navigationService;
            this.currentProject = currentProject;
            Resources = new List<Resources>();
            ToProjectResourcesCreateViewModel = new LambdaCommand(ToProjectResourcesCreateViewModelCommandExecute);
            ProjectResourcesForm = new FormNavigationService() { OnClosingFormCallback = OnTaskFormClosing };
        }

        public List<Resources> Resources
        {
            get => resources;
            set 
            {
                var newResourcesList = value;
                foreach (var item in projectResourcesRepository.Items.Where(x => x.Project == currentProject.Id))
                    newResourcesList.Add(item.Resources);
                Set(ref resources, ref newResourcesList);
            }
        }

        private void ToProjectResourcesCreateViewModelCommandExecute(object parameter)
        {
            ProjectResourcesForm.IsOpen = true;
            ProjectResourcesForm.CurrentViewModel = new ProjectResourcesCreateViewModel(ProjectResourcesForm, currentProject)
            {
                Header = "Добавление ресурса"
            };
        }

        private void OnTaskFormClosing()
        {
            Resources = new List<Resources>();
        }
    }
}
