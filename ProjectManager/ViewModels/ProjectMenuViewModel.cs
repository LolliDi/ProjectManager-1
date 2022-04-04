using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectManager.Models;
using ProjectManager.Models.Repositories;

namespace ProjectManager.ViewModels
{
    class ProjectMenuViewModel : ViewModel
    {
        private int selectedId = 0;
        private readonly IRepository<Projects> projectsRepository;
        private List<Projects> projectList;
        private Users currentUser;
        public ProjectMenuViewModel(Users currentUser, IRepository<Projects> projectsRepository)
        {
            this.currentUser = currentUser;
            this.projectsRepository = projectsRepository;
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
                    projectList = projectsRepository.Items.Where(x => x.Users.Contains(currentUser)).ToList();
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
