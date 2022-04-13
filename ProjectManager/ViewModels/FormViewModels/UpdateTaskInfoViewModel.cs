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
    class UpdateTaskInfoViewModel : TaskInfoViewModel
    {
        public UpdateTaskInfoViewModel(
            IRepository<Tasks> tasksRepository,
            FormNavigationService formNavigationService,
            Projects currentProject,
            Tasks currentTask) : base(formNavigationService, currentProject, currentTask)
        {
            this.tasksRepository = tasksRepository;
        }

        private readonly IRepository<Tasks> tasksRepository;

        protected override void OnSubmitCommandExecute(object parameter)
        {
            tasksRepository.Update(CurrentTask);

            base.OnSubmitCommandExecute(parameter);
        }
        protected override bool CanSubmitCommandExecuted(object parameter)
        {
            return !HasErrors;
        }
    }
}
