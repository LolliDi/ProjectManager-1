using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectManager.Models;
using ProjectManager.Models.Repositories;
using ProjectManager.Services;

namespace ProjectManager.ViewModels
{
    class ProjectMenuViewModel : ViewModel
    {
        private int selectedId = 0;
        private readonly IRepository<Projects> projectsRepository;
        private List<Projects> projectList;
        private Users currentUser;
        private NavigationService navigationService;
        public ProjectMenuViewModel(NavigationService navigationService, Users currentUser)
        {
            this.currentUser = currentUser;
            this.navigationService = navigationService;
            projectsRepository = new Repository<Projects>(new ProjectManagerContext());
            ProjectList = projectsRepository.Items.ToList();
        }


        public List<Projects> ProjectList
        {
            get => projectList;
            set 
            {
                if (currentUser.Roles.Id == 1)
                    projectList = projectsRepository.Items.ToList();
                else if (currentUser.Roles.Id == 3)
                {
                    foreach(Projects pr in projectsRepository.Items) // Единственный способ, которым заработало
                    {
                        projectList = new List<Projects>();
                        if (pr.Users.Where(x => x.Id == currentUser.Id).Any())
                            projectList.Add(pr);
                    }
                }
                else
                    projectList = null;
            }
        }

        public int SelectedProject
        {
            get => selectedId;
            set => selectedId = value;
        }

        public string SelectedProjectTitle
        {
            get => projectList == null ? null : projectList[selectedId].Name;
        }

        public string SelectedProjectDate
        {
            get => projectList == null ? null : projectList[selectedId].CreatingDate.ToString();
        }

        public string SelectedProjectUsers
        {
            get
            {
                string userList = "Список пользователей: ";
                if (projectList != null)
                {
                    foreach (Users user in projectList[selectedId].Users)
                    {
                        userList += "\n" + user.Username;
                    }
                }
                return userList;
            }
        }

    }
}
