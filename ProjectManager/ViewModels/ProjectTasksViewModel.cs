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

            Tasks = projectTasksRepository.Items
                .Select(item => item.Tasks)
                .Join(taskGroupsRepository.Items, projectTask => projectTask.Id, taskGroup => taskGroup.Id, (projectTask, taskGroup) => taskGroup)
                .Where(item => item.Depth == 1)
                .Select(item => item.Tasks)
                .Distinct().ToList();
        }

        private readonly IRepository<TaskGroups> taskGroupsRepository;
        private readonly IRepository<ProjectTasks> projectTasksRepository;
        private readonly IRepository<ProjectTaskResources> projectTaskResourcesRepository;

        public NavigationService NavigationService { get; set; }
        public ICollection<Tasks> Tasks { get; set; }

    }
}
