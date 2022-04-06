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
            projectTaskResourcesRepository = new Repository<ProjectTaskResources>(new ProjectManagerContext());
            projectTasksRepository = new Repository<ProjectTasks>(new ProjectManagerContext());

            InitializeTasks();
        }

        private readonly IRepository<ProjectTasks> projectTasksRepository;
        private readonly IRepository<ProjectTaskResources> projectTaskResourcesRepository;

        public NavigationService NavigationService { get; set; }
        public ICollection<Tasks> Tasks { get; set; }

        private void InitializeTasks()
        {
            Tasks = new List<Tasks>()
            {
                new Tasks()
                {
                    Name = "Task1",
                    Duration = 10,
                    TaskConnections1 = new List<TaskConnections>()
                    {
                        new TaskConnections()
                        {
                            Tasks1 = new Tasks()
                            {
                                Name = "SubTask11",
                                Duration = 11,
                                TaskConnections1 = new List<TaskConnections>()
                                {
                                    new TaskConnections()
                                    {
                                        Tasks1 = new Tasks() { Name = "SubTask12", Duration = 64, }
                                    },
                                    new TaskConnections()
                                    {
                                        Tasks1 = new Tasks() { Name = "SubTask22", Duration = 20, }
                                    },
                                }

                            }

                        },
                        new TaskConnections()
                        {
                            Tasks1 = new Tasks() { Name = "SubTask21", Duration = 50, }
                        },
                        new TaskConnections()
                        {
                            Tasks1 = new Tasks() { Name = "SubTask31", Duration = 94, }
                        },
                    }
                },
                new Tasks()
                {
                    Name = "Task2",
                    TaskConnections1 = new List<TaskConnections>()
                    {
                        new TaskConnections()
                        {
                            Tasks1 = new Tasks() { Name = "SubTask12" }
                        },
                        new TaskConnections()
                        {
                            Tasks1 = new Tasks() { Name = "SubTask22" }
                        },
                    }
                },
            };
        }
    }
}
