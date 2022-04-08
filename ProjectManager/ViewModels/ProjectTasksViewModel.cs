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
using System.Windows.Input;

namespace ProjectManager.ViewModels
{
    class ProjectTasksViewModel : ViewModel
    {
        public ProjectTasksViewModel(NavigationService navigationService, Projects project)
        {
            taskGroupsRepository = new Repository<TaskGroups>();
            projectTaskResourcesRepository = new Repository<ProjectTaskResources>();
            projectTasksRepository = new Repository<ProjectTasks>();

            CurrentProject = project;
            Tasks = CurrentProject.ProjectRootTasks;

            AddTaskCommand = new LambdaCommand(OnAddTaskCommandExecute);
            UpdateTaskCommand = new LambdaCommand(OnUpdateTaskCommandExecute, CanUpdateTaskCommandExecuted);
            RemoveTaskCommand = new LambdaCommand(OnRemoveTaskCommandExecute, CanRemoveTaskCommandExecuted);
            ChangeTasksResourcesCommand = new LambdaCommand(OnChangeTasksResourcesCommandExecute, CanChangeTasksResourcesCommandExecuted);
        }

        #region Fields

        private readonly IRepository<TaskGroups> taskGroupsRepository;
        private readonly IRepository<ProjectTasks> projectTasksRepository;
        private readonly IRepository<ProjectTaskResources> projectTaskResourcesRepository;

        #endregion

        #region Properties

        public NavigationService NavigationService { get; set; }
        public ICollection<Tasks> Tasks { get; set; }
        public Projects CurrentProject { get; set; }

        #endregion

        #region Commands

        public ICommand AddTaskCommand { get; set; }
        public ICommand UpdateTaskCommand { get; set; }
        public ICommand RemoveTaskCommand { get; set; }
        public ICommand ChangeTasksResourcesCommand { get; set; }
        public ICommand ChangeSubtaskCommand { get; set; }
        public ICommand CloseFormCommand { get; set; }
        public ICommand SubmitFormCommand { get; set; }
        public ICommand AddTaskConnectionCommand { get; set; }
        public ICommand ChangeTaskConnectionCommand { get; set; }
        public ICommand RemoveTaskConnectionCommand { get; set; }

        #endregion

        #region Methods
        private void OnAddTaskCommandExecute(object parameter)
        {

        }
        private void OnUpdateTaskCommandExecute(object parameter)
        {

        }
        private bool CanUpdateTaskCommandExecuted(object parameter)
        {
            return true;
        }
        private void OnRemoveTaskCommandExecute(object parameter)
        {

        }
        private bool CanRemoveTaskCommandExecuted(object parameter)
        {
            return true;
        }
        private void OnChangeTasksResourcesCommandExecute(object parameter)
        {

        }
        private bool CanChangeTasksResourcesCommandExecuted(object parameter)
        {
            return true;
        }
        #endregion
    }
}
