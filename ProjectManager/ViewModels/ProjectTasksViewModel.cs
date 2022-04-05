using ProjectManager.Models;
using ProjectManager.Models.Repositories;
using ProjectManager.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManager.ViewModels
{
    class ProjectTasksViewModel : ViewModel
    {
        public class Node
        {
            public Tasks task { get; set; }
            public ObservableCollection<Node> nodes { get; set; }
        }
        public ProjectTasksViewModel(NavigationService navigationService, Projects project)
        {

            Tasks = new ObservableCollection<Node>() 
            {
                new Node() { task = new Tasks() { Name = "Task1" }, nodes = new ObservableCollection<Node>() { new Node() { task = new Tasks() { Name = "Task11" } } } },
                new Node() { task = new Tasks() { Name = "Task2" }, nodes = new ObservableCollection<Node>() { new Node() { task = new Tasks() { Name = "Task21" } } } },
            };

            projectTaskResourcesRepository = new Repository<ProjectTaskResources>(new ProjectManagerContext());
            projectTasksRepository = new Repository<ProjectTasks>(new ProjectManagerContext());

            ToResourcePageCommand = new LambdaCommand(parameter => NavigationService.CurrentViewModel = new ProjectResourcesViewModel(NavigationService, project));
            ToReportsPageCommand = new LambdaCommand(parameter => NavigationService.CurrentViewModel = new ReportsViewModel(NavigationService));
            ToProjectPassportPageCommand = new LambdaCommand(parameter => NavigationService.CurrentViewModel = new ProjectPassportViewModel(NavigationService));
        }

        private readonly IRepository<ProjectTasks> projectTasksRepository;
        private readonly IRepository<ProjectTaskResources> projectTaskResourcesRepository;

        public NavigationService NavigationService { get; set; }
        public ObservableCollection<Node> Tasks { get; set; }
        public ICommand ToResourcePageCommand { get; set; }
        public ICommand ToReportsPageCommand { get; set; }
        public ICommand ToProjectPassportPageCommand { get; set; }
    }
}
