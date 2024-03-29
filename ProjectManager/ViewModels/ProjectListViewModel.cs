﻿using System;
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

        private readonly IRepository<Projects> projectsRepository = new Repository<Projects>();
        private List<Projects> projectList;
        private Users currentUser;

        private NavigationService navigationService;

        public FormNavigationService ProjectCreateForm { get; set; }
        public FormNavigationService UserAccountForm { get; set; }

        public ICommand ToBack { get; set; }
        public ICommand ToCreateProject { get; set; }
        public ICommand ToUserAccount { get; set; }
        public ICommand ToProjectMenu { get; set; }      

        public ProjectListViewModel(NavigationService navigationService, Users currentUser)
        {
            this.currentUser = currentUser;
            this.navigationService = navigationService;

            selectedProjectName = "Выберите проект";
            selectedProjectDate = "";
            selectedProjectUsers = "";

            ProjectList = new List<Projects>();

            ToBack = new LambdaCommand(GoBack);

            ToCreateProject = new LambdaCommand(GoCreateProject);
            ProjectCreateForm = new FormNavigationService() { OnClosingFormCallback = OnTaskFormClosing };

            ToUserAccount = new LambdaCommand(GoUserAccount);
            UserAccountForm = new FormNavigationService() { OnClosingFormCallback = OnTaskFormClosing };

            ToProjectMenu = new LambdaCommand(GoProjectMenu);
            ProjectButtonVisibility = Visibility.Hidden;
        }


        public List<Projects> ProjectList
        {
            get => projectList;
            set 
            {
                if (currentUser.Roles.Permissions.Where(x => x.Name == "permission_viewallprojects" || x.Name == "permission_editallprojects").Any())
                    projectList = projectsRepository.Items.ToList();
                else if (currentUser.Roles.Permissions.Where(x => x.Name == "permission_privateprojects").Any())
                {
                    foreach(Projects pr in projectsRepository.Items) // Единственный способ, которым заработало
                    {
                        projectList = value;
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
            ProjectCreateForm.IsOpen = true;
            ProjectCreateForm.CurrentViewModel = new ProjectCreateViewModel(ProjectCreateForm, currentUser)
            {
                Header = "Создание проекта"
            };
        }

        private void GoUserAccount(object obj)
        {
            UserAccountForm.IsOpen = true;
            UserAccountForm.CurrentViewModel = new UserAccountViewModel(currentUser, UserAccountForm)
            {
                Header = "Данные текущего пользователя"
            };
        }

        private void GoProjectMenu(object obj)
        {
            navigationService.CurrentViewModel = new ProjectMenuViewModel(navigationService, projectList[selectedId], currentUser);
        }

        private void OnTaskFormClosing()
        {
            ProjectList = new List<Projects>();
        }

    }
}
