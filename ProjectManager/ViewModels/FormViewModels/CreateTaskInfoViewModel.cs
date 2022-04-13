using ProjectManager.Models;
using ProjectManager.Models.Repositories;
using ProjectManager.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManager.ViewModels.TaskInfo
{
    class CreateTaskInfoViewModel : TaskInfoViewModel
    {
        public CreateTaskInfoViewModel(
            IRepository<Tasks> tasksRepository,
            IRepository<ProjectTasks> projectTasksRepository,
            FormNavigationService formNavigationService,
            Projects currentProject) : base(formNavigationService, currentProject, new Tasks())
        {
            this.tasksRepository = tasksRepository;
            this.projectTasksRepository = projectTasksRepository;
        }

        private readonly IRepository<ProjectTasks> projectTasksRepository;
        private readonly IRepository<Tasks> tasksRepository;

        protected override void OnSubmitCommandExecute(object parameter)
        {
            var currentTask = tasksRepository.Add(CurrentTask);
            projectTasksRepository.Add(new ProjectTasks()
            {
                Project = CurrentProject.Id,
                Task = currentTask.Id,
                Projects = CurrentProject,
                Tasks = currentTask
            });

            base.OnSubmitCommandExecute(parameter);
        }
        protected override bool CanSubmitCommandExecuted(object parameter)
        {
            return !HasErrors;
        }
    }
}
