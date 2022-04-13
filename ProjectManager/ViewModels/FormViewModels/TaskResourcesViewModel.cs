using ProjectManager.Commands;
using ProjectManager.Models;
using ProjectManager.Models.Repositories;
using ProjectManager.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;

namespace ProjectManager.ViewModels
{
    class TaskResourcesViewModel : FormViewModel
    {
        public TaskResourcesViewModel(
            IRepository<ProjectTaskResources> projectTaskReurcesRepository,
            FormNavigationService formNavigationService,
            Projects currentProject,
            Tasks currentTask) : base(formNavigationService)
        {
            this.projectTaskReurcesRepository = projectTaskReurcesRepository;

            CurrentTask = currentTask;
            CurrentProject = currentProject;

            var currentTaskResourcesList = CurrentTask.ProjectTasks.FirstOrDefault(item => item.Tasks == CurrentTask)?.ProjectTaskResources;
            var currentProjectResourcesList = CurrentProject.ProjectResources.Where(item => item.Projects == CurrentProject).Select(item => item.Resources);

            CurrentTaskResources = currentTaskResourcesList == null ? new ObservableCollection<ProjectTaskResources>() : new ObservableCollection<ProjectTaskResources>(currentTaskResourcesList);
            CurrentProjectResources = currentProjectResourcesList == null ? new ObservableCollection<Resources>() : new ObservableCollection<Resources>(currentProjectResourcesList);
            CurrentTaskResourcesViewSource = new CollectionViewSource() { Source = currentTaskResourcesList.Select(item => item.ProjectResources.Resources) };

            DropItemCommand = new LambdaCommand(OnDropItemCommandExecute);
            RemoveTaskResourceCommand = new LambdaCommand(OnRemoveTaskResourceCommandExecute);
        }

        private readonly IRepository<ProjectTaskResources> projectTaskReurcesRepository;

        public Tasks CurrentTask { get; set; }
        public Projects CurrentProject { get; set; }
        public Resources SelectedCurrentTaskResource { get; set; }
        public Resources DraggedProjectResource { get; set; }
        public CollectionViewSource CurrentTaskResourcesViewSource { get; }
        public ObservableCollection<ProjectTaskResources> CurrentTaskResources { get; }
        public ObservableCollection<Resources> CurrentProjectResources { get; }

        public ICommand DropItemCommand { get; set; }
        public ICommand RemoveTaskResourceCommand { get; set; }

        private void OnDropItemCommandExecute(object parameter)
        {
            DragEventArgs e = (DragEventArgs)parameter;
            DraggedProjectResource = (Resources)e.Data.GetData(DataFormats.Serializable);

            var projectTaskResource = new ProjectTaskResources()
            {
                ProjectResource = DraggedProjectResource.Id,
                ProjectResources = DraggedProjectResource.ProjectResources.FirstOrDefault(item => item.Projects == CurrentProject),
                ProjectTask = CurrentTask.Id,
                ProjectTasks = CurrentTask.ProjectTasks.FirstOrDefault(item => item.Projects == CurrentProject)
            };

            projectTaskReurcesRepository.Add(projectTaskResource);
            CurrentTaskResources.Add(projectTaskResource);
            CurrentTaskResourcesViewSource.View.Refresh();
        }
        private void OnRemoveTaskResourceCommandExecute(object obj)
        {
            var projectTaskResource = SelectedCurrentTaskResource
                .ProjectResources
                .FirstOrDefault(item => item.Resources == SelectedCurrentTaskResource)
                ?.ProjectTaskResources
                .FirstOrDefault(item => item.ProjectTasks.Tasks == CurrentTask);
            projectTaskReurcesRepository.Remove(projectTaskResource.Id);
            CurrentTaskResources.Remove(projectTaskResource);
            CurrentTaskResourcesViewSource.View.Refresh();
        }
    }
}
