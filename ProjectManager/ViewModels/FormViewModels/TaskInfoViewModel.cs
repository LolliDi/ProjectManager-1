using ProjectManager.Commands;
using ProjectManager.Models;
using ProjectManager.Models.Repositories;
using ProjectManager.Services;
using ProjectManager.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace ProjectManager.ViewModels.TaskInfo
{
    abstract class TaskInfoViewModel : FormViewModel
    {
        public TaskInfoViewModel(FormNavigationService formNavigationService, Projects currentProject, Tasks currentTask) : base(formNavigationService)
        {
            CurrentTask = currentTask;
            CurrentProject = currentProject;

            var currentProjectTasksList = CurrentProject.ProjectTasks.Where(item => item.Projects == CurrentProject).Select(item => item.Tasks).ToList();
            CurrentProjectTasks = currentProjectTasksList == null ? new ObservableCollection<Tasks>() : new ObservableCollection<Tasks>(currentProjectTasksList);

            TaskName = CurrentTask.Name;
            TaskStartDate = CurrentTask.StartDate.GetValueOrDefault(DateTime.Now);
            TaskEndDate = CurrentTask.EndDate.GetValueOrDefault(DateTime.Now);
            TaskDuration = CurrentTask.Duration.ToString();
            TaskCompletionPercentage = CurrentTask.CompletionPercentage.ToString();
        }

        private bool isTaskDurationEditing;

        #region Properties

        public Tasks CurrentTask { get; set; }
        public Projects CurrentProject { get; set; }
        public ObservableCollection<Tasks> CurrentProjectTasks { get; }
        public string TaskName
        {
            get => CurrentTask.Name;
            set
            {
                ClearErrors();

                if (string.IsNullOrWhiteSpace(value))
                {
                    AddError("Недопустимое наименовение задачи");
                }
                CurrentTask.Name = value;
            }
        }
        public DateTime TaskStartDate
        {
            get => CurrentTask.StartDate.GetValueOrDefault(DateTime.Now);
            set
            {
                ClearErrors();

                if (!isTaskDurationEditing)
                {
                    if (DateTime.Compare(value, TaskEndDate) > 0)
                    {
                        CurrentTask.StartDate = value;
                        TaskEndDate = value;
                    }
                    else
                    {
                        CurrentTask.StartDate = value;
                    }
                    TaskDuration = (TaskEndDate - TaskStartDate).Days.ToString();
                }

                OnPropertyChanged();
            }
        }
        public DateTime TaskEndDate
        {
            get => CurrentTask.EndDate.GetValueOrDefault(DateTime.Now);
            set
            {
                ClearErrors();

                if (DateTime.Compare(TaskStartDate, value) > 0)
                {
                    AddError("Дата окончания задачи не может раньше даты начала задачи");
                }
                if (DateTime.Compare(value, TaskStartDate) < 0)
                {
                    TaskStartDate = value;
                    CurrentTask.EndDate = value;
                }
                else
                {
                    CurrentTask.EndDate = value;
                }
                if (!isTaskDurationEditing)
                {
                    TaskDuration = (TaskEndDate - TaskStartDate).Days.ToString();
                }

                OnPropertyChanged();
            }
        }
        public string TaskDuration
        {
            get => CurrentTask.Duration.GetValueOrDefault().ToString();
            set
            {
                ClearErrors();

                isTaskDurationEditing = true;
                if (!Regex.IsMatch(value, "[0-9]*"))
                {
                    AddError("Введите число");
                }
                if(int.TryParse(value, out int parsedValue))
                {
                    TaskEndDate = TaskStartDate.AddDays(parsedValue);

                    if (parsedValue != (TaskEndDate - TaskStartDate).Days)
                    {
                        AddError("Длительность задачи не совпадает с указанными датами");
                    }
                }

                CurrentTask.Duration = parsedValue;
                isTaskDurationEditing = false;

                OnPropertyChanged();
            }
        }
        public string TaskCompletionPercentage
        {
            get => CurrentTask.CompletionPercentage.GetValueOrDefault().ToString();
            set
            {
                ClearErrors();

                if (!Regex.IsMatch(value, "^[0-9]+$") && value.Length > 1)
                {
                    AddError("Введите число");
                }
                if (int.TryParse(value, out int parsedValue))
                {
                    if (parsedValue < 0)
                    {
                        AddError("Значение не может быть меньше 0");
                    }
                    if (parsedValue > 100)
                    {
                        AddError("Значение не может быть больше 100");
                    }
                }
                CurrentTask.CompletionPercentage = parsedValue;
            }
        }
        #endregion
    }
}
