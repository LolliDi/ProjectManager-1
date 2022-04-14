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
using System.Windows;

namespace ProjectManager.ViewModels
{
    class ProjectResourcesViewModel : ViewModel
    {
        private NavigationService navigationService = new NavigationService();
        private Projects currentProject;

        private List<Resources> resources = new List<Resources>();
        private Repository<Resources> resourcesRepository = new Repository<Resources>();
        private Repository<ProjectResources> projectResourcesRepository = new Repository<ProjectResources>();
        public ProjectResourcesViewModel(NavigationService navigationService, Projects currentProject)
        {
            this.navigationService = navigationService;
            this.currentProject = currentProject;
            foreach (var item in projectResourcesRepository.Items.Where(x => x.Project == currentProject.Id))
                resources.Add(item.Resources);
            ToProjectResourcesCreateViewModel = new LambdaCommand(ToProjectResourcesCreateViewModelCommandExecute);
            ToProjectResourcesEditViewModel = new LambdaCommand(ToProjectResourcesEditViewModelCommandExecute);
            ToProjectResourcesDeleteViewModel = new LambdaCommand(ToProjectResourcesDeleteViewModelCommandExecute);
        }
        public List<Resources> Resources
        {
            get { return resources; }
        }
        public ICommand ToProjectResourcesCreateViewModel { get; set; }
        public ICommand ToProjectResourcesEditViewModel { get; set; }
        public ICommand ToProjectResourcesDeleteViewModel { get; set; }

        private void ToProjectResourcesCreateViewModelCommandExecute(object parameter)
        {
            navigationService.CurrentViewModel = new ProjectResourcesCreateViewModel(navigationService, currentProject);
        }

        private void ToProjectResourcesEditViewModelCommandExecute(object parameter)
        {
            MessageBox.Show("В разработке", "Инфомация", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void ToProjectResourcesDeleteViewModelCommandExecute(object parameter)
        {
            MessageBox.Show("В разработке", "Инфомация", MessageBoxButton.OK, MessageBoxImage.Information);
        }
    }
}
