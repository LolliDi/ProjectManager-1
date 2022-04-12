using ProjectManager.Commands;
using ProjectManager.Models;
using ProjectManager.Models.Repositories;
using ProjectManager.Services;
using ProjectManager.ViewModels.TaskInfo;
using ProjectManager.Views.Forms;
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
    class ProjectTasksViewModel : ViewModel
    {
        public ProjectTasksViewModel(Projects project)
        {
            taskGroupsRepository = new Repository<TaskGroups>();
            projectTaskResourcesRepository = new Repository<ProjectTaskResources>();
            projectTasksRepository = new Repository<ProjectTasks>();

            TaskInfoForm = new FormNavigationService();
            TaskConnectionsForm = new FormNavigationService();
            TaskResourcesForm = new FormNavigationService();

            CurrentProject = project;
            Tasks = new CollectionViewSource() { Source = CurrentProject.ProjectRootTasks };

            CreateTaskInfoCommand = new LambdaCommand(OnCreateTaskInfoCommandExecute);
            UpdateTaskInfoCommand = new LambdaCommand(OnUpdateTaskInfoCommandExecute, CanUpdateTaskInfoCommandExecuted);
            RemoveTaskInfoCommand = new LambdaCommand(OnRemoveTaskInfoCommandExecute, CanRemoveTaskInfoCommandExecuted);
            ChangeTaskResourcesCommand = new LambdaCommand(OnChangeTasksResourcesCommandExecute, CanChangeTasksResourcesCommandExecuted);
            ChangeTaskConnectionsCommand = new LambdaCommand(OnChangeTaskConnectionsCommandExecute, CanChangeTaskConnectionsCommandExecuted);
            SelectedItemChangedCommand = new LambdaCommand(OnSelectedItemChangedCommandExecute);
        }

        #region Fields

        private readonly IRepository<TaskGroups> taskGroupsRepository;
        private readonly IRepository<ProjectTasks> projectTasksRepository;
        private readonly IRepository<ProjectTaskResources> projectTaskResourcesRepository;

        #endregion

        #region Properties

        public CollectionViewSource Tasks { get; }
        public FormNavigationService TaskInfoForm { get; set; }
        public FormNavigationService TaskConnectionsForm { get; set; }
        public FormNavigationService TaskResourcesForm { get; set; }
        public Projects CurrentProject { get; set; }
        public Tasks SelectedTask { get; set; }

        #endregion

        #region Commands

        public ICommand CreateTaskInfoCommand { get; set; }
        public ICommand UpdateTaskInfoCommand { get; set; }
        public ICommand RemoveTaskInfoCommand { get; set; }
        public ICommand ChangeTaskResourcesCommand { get; set; }
        public ICommand ChangeTaskConnectionsCommand { get; set; }
        public ICommand SelectedItemChangedCommand { get; set; }
        #endregion

        #region Methods

        #region Menu Command Methods

        private void OnCreateTaskInfoCommandExecute(object parameter)
        {
            TaskInfoForm.IsOpen = true;
            TaskInfoForm.CurrentViewModel = new CreateTaskInfoViewModel(new Repository<Tasks>(), new Repository<ProjectTasks>(), TaskInfoForm, CurrentProject) { Header = "Добавление задачи" };
        }
        private void OnUpdateTaskInfoCommandExecute(object parameter)
        {
            TaskInfoForm.IsOpen = true;
            TaskInfoForm.CurrentViewModel = new UpdateTaskInfoViewModel(new Repository<Tasks>(), TaskInfoForm, CurrentProject, SelectedTask) { Header = "Редактирование задачи" };
        }
        private bool CanUpdateTaskInfoCommandExecuted(object parameter)
        {
            return SelectedTask != null;
        }
        private void OnRemoveTaskInfoCommandExecute(object parameter)
        {
            if (MessageBoxResult.Yes == MessageBox.Show("Вы точно хотите удалить данный элемент?", "Удаление", MessageBoxButton.YesNo, MessageBoxImage.Question))
            {
                taskGroupsRepository.Remove(SelectedTask.Id);
            }
        }
        private bool CanRemoveTaskInfoCommandExecuted(object parameter)
        {
            return SelectedTask != null;
        }
        private void OnChangeTasksResourcesCommandExecute(object parameter)
        {
            TaskResourcesForm.IsOpen = true;
            TaskResourcesForm.CurrentViewModel = new TaskResourcesViewModel(TaskResourcesForm, CurrentProject, SelectedTask)
            {
                Header = "Редактирование ресурсов задачи"
            };
        }
        private bool CanChangeTasksResourcesCommandExecuted(object parameter)
        {
            return SelectedTask != null;
        }
        #endregion

        private void OnSelectedItemChangedCommandExecute(object parameter)
        {
            SelectedTask = (Tasks)parameter;
        }
        private void OnChangeTaskConnectionsCommandExecute(object obj)
        {
            TaskConnectionsForm.IsOpen = true;
            TaskConnectionsForm.CurrentViewModel = new TaskConnectionsViewModel(
                new Repository<TaskConnections>(),
                new Repository<TaskGroups>(),
                TaskConnectionsForm,
                CurrentProject,
                SelectedTask)
            {
                Header = "Редактирование связей задачи"
            };
        }
        private bool CanChangeTaskConnectionsCommandExecuted(object arg)
        {
            return SelectedTask != null;
        }

        #endregion
    }
}
