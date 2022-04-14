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

            SelectedParentTask = CurrentTask.TaskGroups1.Select(item => item.Tasks).FirstOrDefault();

            var currentProjectTasksList = CurrentProject.ProjectTasks.Where(item => item.Projects == CurrentProject).Select(item => item.Tasks).Where(item => item != CurrentTask).ToList();
            var currentTaskConnectionsList = CurrentTask.TaskConnections.ToList();

            CurrentProjectTasks = currentProjectTasksList == null ? new ObservableCollection<Tasks>() : new ObservableCollection<Tasks>(currentProjectTasksList);
            CurrentTaskConnections = currentTaskConnectionsList == null ? new ObservableCollection<TaskConnections>() : new ObservableCollection<TaskConnections>(currentTaskConnectionsList);
            ConnectionTypes = new ObservableCollection<TaskTypes>(new Repository<TaskTypes>().Items.ToList());
            CurrentTaskConnectionViewSource = new CollectionViewSource() { Source = CurrentTaskConnections };

            AddTaskConnectionCommand = new LambdaCommand(OnAddTaskConnectionCommandExecute, CanAddTaskConnectionCommandExecuted);
            ChangeTaskConnectionCommand = new LambdaCommand(OnChangeTaskConnectionCommandExecute, CanChangeTaskConnectionCommandExecuted);
            RemoveTaskConnectionCommand = new LambdaCommand(OnRemoveTaskConnectionCommandExecute, CanRemoveTaskConnectionCommandExecuted);
            SaveChangesCommand = new LambdaCommand(OnSaveChangesCommandExecute);
            ParentTaskChangedCommand = new LambdaCommand(OnParentTaskChangedCommandExecute);
            CancelEditModeCommand = new LambdaCommand(OnCancelEditModeCommandExecute);
        }

        protected readonly IRepository<TaskConnections> taskConnectionsRepository;
        private readonly IRepository<TaskGroups> taskGroupsRepository;
        private bool isEditMode;
        private Tasks selectedTaskConnection;
        private Tasks selectedParentTask;
        private TaskTypes selectedConnectionType;
        private TaskConnections selectedListTask;

        #region Properties

        public bool IsEditMode
        {
            get => isEditMode;
            set => Set(ref isEditMode, ref value);
        }
        public Tasks CurrentTask { get; set; }
        public Projects CurrentProject { get; set; }
        public Tasks SelectedParentTask
        {
            get => selectedParentTask;
            set => Set(ref selectedParentTask, ref value);
        }
        public Tasks SelectedTaskConnection
        {
            get => selectedTaskConnection;
            set => Set(ref selectedTaskConnection, ref value);
        }
        public TaskConnections SelectedListTaskConnection
        {
            get => selectedListTask;
            set => Set(ref selectedListTask, ref value);
        }
        public TaskTypes SelectedConnectionType
        {
            get => selectedConnectionType;
            set => Set(ref selectedConnectionType, ref value);
        }
        public ObservableCollection<TaskTypes> ConnectionTypes { get; }
        public ObservableCollection<Tasks> CurrentProjectTasks { get; }
        public ObservableCollection<TaskConnections> CurrentTaskConnections { get; }
        public CollectionViewSource CurrentTaskConnectionViewSource { get; }

        #endregion

        public ICommand AddTaskConnectionCommand { get; set; }
        public ICommand ChangeTaskConnectionCommand { get; set; }
        public ICommand RemoveTaskConnectionCommand { get; set; }
        public ICommand SaveChangesCommand { get; set; }
        public ICommand ParentTaskChangedCommand { get; set; }
        public ICommand CancelEditModeCommand { get; set; }

        #region Methods

        #region Command Methods

        private void OnAddTaskConnectionCommandExecute(object parameter)
        {
            var taskConnection = new TaskConnections()
            {
                Parent = CurrentTask.Id,
                Child = SelectedTaskConnection.Id,
                Tasks = CurrentTask,
                Tasks1 = SelectedTaskConnection,
                TaskType = SelectedConnectionType.Id,
                TaskTypes = SelectedConnectionType
            };
            taskConnectionsRepository.Add(taskConnection);
            CurrentTaskConnections.Add(taskConnection);
        }
        private void OnChangeTaskConnectionCommandExecute(object parameter)
        {
            SelectedTaskConnection = SelectedListTaskConnection.Tasks1;
            SelectedConnectionType = SelectedListTaskConnection.TaskTypes;

            IsEditMode = true;
        }
        private void OnSaveChangesCommandExecute(object parameter)
        {
            SelectedListTaskConnection.Child = SelectedTaskConnection.Id;
            SelectedListTaskConnection.Tasks1 = SelectedTaskConnection;
            SelectedListTaskConnection.TaskType = SelectedConnectionType.Id;
            SelectedListTaskConnection.TaskTypes = SelectedConnectionType;
            taskConnectionsRepository.Update(SelectedListTaskConnection);

            CurrentTaskConnectionViewSource.View.Refresh();

            SelectedConnectionType = null;
            SelectedTaskConnection = null;

            IsEditMode = false;
        }
        private void OnRemoveTaskConnectionCommandExecute(object parameter)
        {
            if (MessageBoxResult.Yes == MessageBox.Show("Вы точно хотите удалить данную связь?", "Предупреждение", MessageBoxButton.YesNo, MessageBoxImage.Question))
            {
                taskConnectionsRepository.Remove(SelectedListTaskConnection.Id);
                CurrentTaskConnections.Remove(SelectedListTaskConnection);
            }
        }
        private void OnParentTaskChangedCommandExecute(object patameter)
        {
            if(MessageBoxResult.Yes == MessageBox
                .Show("Родительская задача станет недоступной для редактирования и удаления. " +
                "Ее данные будут автоматически высчитываться исходя из дочерних задач. Продолжить?", "Предупреждение", 
                MessageBoxButton.YesNo, MessageBoxImage.Warning))
            {
                TaskGroups taskGroup = CurrentTask.TaskGroups1.FirstOrDefault() ?? new TaskGroups();

                taskGroup.Parent = SelectedParentTask.Id;
                taskGroup.Child = CurrentTask.Id;
                taskGroup.Tasks = SelectedParentTask;
                taskGroup.Tasks1 = CurrentTask;

                if (taskGroup.Id == 0)
                {
                    taskGroupsRepository.Add(taskGroup);
                }
                else
                {
                    taskGroupsRepository.Update(taskGroup);
                }
            }
        }
        private bool CanAddTaskConnectionCommandExecuted(object parameter)
        {
            return SelectedTaskConnection != null
                && SelectedConnectionType != null
                && !CurrentTask.TaskConnections1.Any(item => item.Tasks1 == SelectedTaskConnection && item.TaskTypes == SelectedConnectionType);
        }
        private bool CanChangeTaskConnectionCommandExecuted(object parameter)
        {
            return SelectedListTaskConnection != null;
        }
        private bool CanRemoveTaskConnectionCommandExecuted(object parameter)
        {
            return SelectedListTaskConnection != null;
        }

        private void OnCancelEditModeCommandExecute(object parameter)
        {
            SelectedConnectionType = null;
            SelectedTaskConnection = null;

            IsEditMode = false;
        }
        #endregion

        #endregion
    }
}
