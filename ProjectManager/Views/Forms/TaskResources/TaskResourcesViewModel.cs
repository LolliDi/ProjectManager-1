using ProjectManager.Commands;
using ProjectManager.Models;
using ProjectManager.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace ProjectManager.ViewModels
{
    class TaskResourcesViewModel : FormViewModel
    {
        public TaskResourcesViewModel(FormNavigationService formNavigationService, Projects currentProject, Tasks currentTask) : base(formNavigationService)
        {
            CurrentTask = currentTask;
            CurrentProject = currentProject;

            var currentTaskResourcesList = CurrentTask.ProjectTasks.FirstOrDefault(item => item.Tasks == CurrentTask)?.ProjectTaskResources.Select(item => item.ProjectResources.Resources).ToList();
            var currentProjectResourcesList = CurrentProject.ProjectResources.Where(item => item.Projects == CurrentProject).Select(item => item.Resources).ToList();

            CurrentTaskResources = currentTaskResourcesList == null ? new ObservableCollection<Resources>() : new ObservableCollection<Resources>(currentTaskResourcesList);
            CurrentProjectResources = currentProjectResourcesList == null ? new ObservableCollection<Resources>() : new ObservableCollection<Resources>(currentProjectResourcesList);

            DropItemCommand = new LambdaCommand(OnDropItemCommandExecute);

        }
        private ObservableCollection<Resources> currentTaskResources;


        public Tasks CurrentTask { get; set; }
        public Projects CurrentProject { get; set; }
        public Resources DraggedProjectResource { get; set; }
        public ObservableCollection<Resources> CurrentTaskResources
        {
            get => currentTaskResources;
            set => Set(ref currentTaskResources, ref value);
        }
        public ObservableCollection<Resources> CurrentProjectResources { get; }

        public ICommand DropItemCommand { get; set; }
        public ICommand AddProjectTaskResourcesCommand { get; set; }
        public ICommand ChangeProjectTaskResourcesCommand { get; set; }
        public ICommand RemoveProjectTaskResourcesCommand { get; set; }

        private void OnDropItemCommandExecute(object parameter)
        {
            DragEventArgs e = (DragEventArgs)parameter;
            DraggedProjectResource = (Resources)e.Data.GetData(DataFormats.Serializable);
            CurrentTaskResources.Add(DraggedProjectResource);
        }

    }
}
