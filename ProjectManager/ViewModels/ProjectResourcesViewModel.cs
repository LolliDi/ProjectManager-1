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
        private List<Resources> resources = new List<Resources>();
        Repository<Resources> resourcesRepository = new Repository<Resources>();
        NavigationService navigationService = new NavigationService();
        public ProjectResourcesViewModel(NavigationService navigationService, Projects project)
        {
            resources = resourcesRepository.Items.ToList();
            ToProjectResourcesCreateViewModel = new LambdaCommand(ToProjectResourcesCreateViewModelCommandExecute);
            this.navigationService = navigationService;
        }
        public List<Resources> Resources
        {
            get { return resources; }
        }
        public ICommand ToProjectResourcesCreateViewModel { get; set; }        
        private void ToProjectResourcesCreateViewModelCommandExecute(object parameter)
        {
            navigationService.CurrentViewModel = new ProjectResourcesCreateViewModel(navigationService);
        }
    }
}
