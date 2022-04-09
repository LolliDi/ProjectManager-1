﻿using ProjectManager.Models;
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
        public ProjectResourcesViewModel(NavigationService navigationService, Projects currentProject)
        {
            this.navigationService = navigationService;
            this.currentProject = currentProject;
            resources = resourcesRepository.Items.ToList();
            ToProjectResourcesCreateViewModel = new LambdaCommand(ToProjectResourcesCreateViewModelCommandExecute);
        }
        public List<Resources> Resources
        {
            get { return resources; }
        }
        public ICommand ToProjectResourcesCreateViewModel { get; set; }        
        private void ToProjectResourcesCreateViewModelCommandExecute(object parameter)
        {
            navigationService.CurrentViewModel = new ProjectResourcesCreateViewModel(navigationService, currentProject);
        }
    }
}
