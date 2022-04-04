using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectManager.Models;

namespace ProjectManager.ViewModels
{
    class ProjectMenuViewModel : ViewModel
    {
        private int selectedId = 0;
        public ProjectMenuViewModel(List<Projects> projectList)
        {
            this.projectList = projectList;
        }

        private List<Projects> projectList;
        public List<Projects> ProjectList
        {
            get => projectList;
            set => projectList = value;
        }

        public int SelectedProject
        {
            get => selectedId;
            set => selectedId = value;
        }

        public string SelectedProjectTitle
        {
            get => projectList[selectedId].Name;
        }

        public string SelectedProjectDate
        {
            get => projectList[selectedId].CreatingDate.ToString();
        }

        public string SelectedProjectUsers
        {
            get
            {
                string userList = "Список пользователей: ";
                foreach(Users user in projectList[selectedId].Users)
                {
                    userList += "\n" + user.Username;
                }
                return userList;
            }
        }

    }
}
