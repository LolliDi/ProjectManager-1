using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using ProjectManager.Commands;
using ProjectManager.Models;
using ProjectManager.Models.Repositories;
using ProjectManager.Services;

namespace ProjectManager.ViewModels
{
    class ProjectListViewModel : ViewModel
    {
        private string selectedProjectName, selectedProjectDate, selectedProjectUsers;
        private Visibility projectButtonVisibility;
        private int selectedId = -1;

        private readonly IRepository<Projects> projectsRepository;
        private List<Projects> projectList;
        private Users currentUser;

        private NavigationService navigationService;

        public ICommand ToBack { get; set; }
        public ICommand ToCreateProject { get; set; }
        public ICommand ToProjectPage { get; set; }

        public ProjectListViewModel(NavigationService navigationService, Users currentUser)
        {
            this.currentUser = currentUser;
            this.navigationService = navigationService;
            selectedProjectName = "Выберите проект";
            selectedProjectDate = "";
            selectedProjectUsers = "";
            projectsRepository = new Repository<Projects>(new ProjectManagerContext());
            ProjectList = projectsRepository.Items.ToList();

            ToBack = new LambdaCommand(GoBack);
            ToBack = new LambdaCommand(GoCreateProject);
            ToBack = new LambdaCommand(GoProjectPage);
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
            set
            {
                selectedId = value;
                SelectedProjectTitle = projectList[selectedId].Name.ToString();
                SelectedProjectDate = projectList[selectedId].CreatingDate.ToString();
                SelectedProjectUsers = CreateUserList();
                if (selectedId == -1)
                    ProjectButtonVisibility = Visibility.Hidden;
                else
                    ProjectButtonVisibility = Visibility.Visible;
            }
        }

        public string SelectedProjectTitle
        {
            get => selectedProjectName;
            set => Set(ref selectedProjectName, ref value);
        }

        public string SelectedProjectDate
        {
            get => selectedProjectDate;
            set => Set(ref selectedProjectDate, ref value);
        }

        public string SelectedProjectUsers
        {
            get => selectedProjectUsers;
            set => Set(ref selectedProjectUsers, ref value);
        }

        public Visibility ProjectButtonVisibility
        {
            get => projectButtonVisibility;
            set => Set(ref projectButtonVisibility, ref value);
        }

        private string CreateUserList()
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

        private void GoBack(object obj)
        {
            navigationService.CurrentViewModel = new AuthorizationViewModel(navigationService);
        }

        private void GoCreateProject(object obj)
        {

        }

        private void GoProjectPage(object obj)
        {

        }

    }
}
