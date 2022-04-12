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
using System.Windows.Input;

namespace ProjectManager.ViewModels
{
    class TaskConnectionsViewModel : FormViewModel
    {
        public TaskConnectionsViewModel(
            IRepository<TaskConnections> taskConnectionsRepository,
            IRepository<TaskGroups> taskGroupsRepository,
            FormNavigationService formNavigationService, 
            Projects currentProject, 
            Tasks currentTask) : base(formNavigationService)
        {
            this.taskConnectionsRepository = taskConnectionsRepository;
            this.taskGroupsRepository = taskGroupsRepository;

            CurrentTask = currentTask;
            CurrentProject = currentProject;

            SelectedConnectionType = CurrentTask.TaskConnections.Select(item => item.TaskTypes).FirstOrDefault();
            SelectedConnectionTask = CurrentTask.TaskConnections.Select(item => item.Tasks).FirstOrDefault();
            SelectedParentTask = CurrentTask.TaskGroups1.Select(item => item.Tasks).FirstOrDefault();

            var currentProjectTasksList = CurrentProject.ProjectTasks.Where(item => item.Projects == CurrentProject).Select(item => item.Tasks).ToList();
            var currentTaskConnectionsList = CurrentTask.TaskConnections1.Select(item => item.Tasks1).ToList();

            CurrentProjectTasks = currentProjectTasksList == null ? new ObservableCollection<Tasks>() : new ObservableCollection<Tasks>(currentProjectTasksList);
            CurrentTaskConnections = currentTaskConnectionsList == null ? new ObservableCollection<Tasks>() : new ObservableCollection<Tasks>(currentTaskConnectionsList);
            ConnectionTypes = new ObservableCollection<TaskTypes>(new Repository<TaskTypes>().Items.ToList());

            AddTaskConnectionCommand = new LambdaCommand(OnAddTaskConnectionCommandExecute, CanAddTaskConnectionCommandExecuted);
            ChangeTaskConnectionCommand = new LambdaCommand(OnChangeTaskConnectionCommandExecute, CanChangeTaskConnectionCommandExecuted);
            RemoveTaskConnectionCommand = new LambdaCommand(OnRemoveTaskConnectionCommandExecute, CanRemoveTaskConnectionCommandExecuted);
            SaveChangesCommand = new LambdaCommand(OnSaveChangesCommandExecute);
            ParentTaskChangedCommand = new LambdaCommand(OnParentTaskChangedCommandExecute);
        }

        protected readonly IRepository<TaskConnections> taskConnectionsRepository;
        private readonly IRepository<TaskGroups> taskGroupsRepository;
        private bool isEditMode;

        #region Properties

        public bool IsEditMode
        {
            get => isEditMode;
            set => Set(ref isEditMode, ref value);
        }
        public Tasks CurrentTask { get; set; }
        public Projects CurrentProject { get; set; }
        public Tasks SelectedParentTask { get; set; }
        public Tasks SelectedConnectionTask { get; set; }
        public Tasks SelectedListTask { get; set; }
        public TaskTypes SelectedConnectionType { get; set; }
        public ObservableCollection<TaskTypes> ConnectionTypes { get; }
        public ObservableCollection<Tasks> CurrentProjectTasks { get; }
        public ObservableCollection<Tasks> CurrentTaskConnections { get; }

        #endregion

        public ICommand AddTaskConnectionCommand { get; set; }
        public ICommand ChangeTaskConnectionCommand { get; set; }
        public ICommand RemoveTaskConnectionCommand { get; set; }
        public ICommand SaveChangesCommand { get; set; }
        public ICommand ParentTaskChangedCommand { get; set; }

        #region Methods

        #region Command Methods

        private void OnAddTaskConnectionCommandExecute(object parameter)
        {
            taskConnectionsRepository.Add(new TaskConnections()
            {
                Parent = CurrentTask.Id,
                Child = SelectedConnectionTask.Id,
                Tasks = CurrentTask,
                Tasks1 = SelectedConnectionTask,
                TaskType = SelectedConnectionType.Id,
                TaskTypes = SelectedConnectionType
            });
            CurrentTaskConnections.Add(SelectedConnectionTask);
        }
        private void OnChangeTaskConnectionCommandExecute(object parameter)
        {
            var taskConnection = SelectedListTask.TaskConnections1.FirstOrDefault(item => item.Tasks == CurrentTask);
            SelectedConnectionTask = taskConnection.Tasks;
            SelectedConnectionType = taskConnection.TaskTypes;

            IsEditMode = true;
        }
        private void OnSaveChangesCommandExecute(object parameter)
        {
            var taskConnection = SelectedListTask.TaskConnections1.FirstOrDefault(item => item.Tasks == CurrentTask);
            taskConnection.Child = SelectedConnectionTask.Id;
            taskConnection.Tasks1 = SelectedConnectionTask;
            taskConnection.TaskType = SelectedConnectionType.Id;
            taskConnection.TaskTypes = SelectedConnectionType;
            taskConnectionsRepository.Update(taskConnection);

            IsEditMode = false;
        }
        private void OnRemoveTaskConnectionCommandExecute(object parameter)
        {
            if (MessageBoxResult.Yes == MessageBox.Show("Вы точно хотите удалить данную связь?", "Предупреждение", MessageBoxButton.YesNo, MessageBoxImage.Question))
            {
                taskConnectionsRepository.Remove(SelectedListTask.TaskConnections1.FirstOrDefault(item => item.Tasks == CurrentTask).Id);
                CurrentTaskConnections.Remove(SelectedListTask);
            }
        }
        private void OnParentTaskChangedCommandExecute(object patameter)
        {
            TaskGroups taskGroup = CurrentTask.TaskGroups.FirstOrDefault() ?? new TaskGroups();

            if (CurrentTask.TaskGroups.Count == 0)
            {
                taskGroupsRepository.Add(taskGroup);
            }

            taskGroup.Parent = SelectedParentTask.Id;
            taskGroup.Child = CurrentTask.Id;
            taskGroup.Tasks = SelectedParentTask;
            taskGroup.Tasks1 = CurrentTask;
            taskGroupsRepository.Update(taskGroup);
        }
        private bool CanAddTaskConnectionCommandExecuted(object parameter)
        {
            return SelectedConnectionTask != null
                && SelectedConnectionType != null
                && !CurrentTask.TaskConnections1.Any(item => item.Tasks1 == SelectedConnectionTask && item.TaskTypes == SelectedConnectionType);
        }
        private bool CanChangeTaskConnectionCommandExecuted(object parameter)
        {
            return SelectedListTask != null;
        }
        private bool CanRemoveTaskConnectionCommandExecuted(object parameter)
        {
            return SelectedListTask != null;
        }

        #endregion

        #region Overrided Methods

        #endregion

        #endregion
    }
}
